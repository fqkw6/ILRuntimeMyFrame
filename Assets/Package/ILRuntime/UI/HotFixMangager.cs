using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;
using System.IO;
using System.Reflection;
using System;
using ILRuntime.Runtime.Enviorment;
using AssetBundles;
using UnityEngine.Networking;

public class HotFixMangager : Singleton<HotFixMangager>
{

    AppDomain mAppDomain;

    public void InitApp(byte [] dllBytes,byte [] pdbBytes=null)
    {
        mAppDomain = new AppDomain();
        MemoryStream dll = (dllBytes != null) ? new MemoryStream(dllBytes) : null;
        MemoryStream pdb = (pdbBytes != null) ? new MemoryStream(pdbBytes) : null;
        mAppDomain.LoadAssembly(dll, pdb, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
        RegisterValueTypeBinder();
        RegisterCrossBindingAdaptor();
        RegisterCLRMethodRedirection();
        RegisterDelegates();
        Debug.LogError("启动热更");
    }

    public AppDomain GetAppDomain()
    {
        return mAppDomain;
    }
    public override void Dispose()
    {
        
    }
    private void RegisterValueTypeBinder()
    {

    }
    private void RegisterCLRMethodRedirection()
    {
        ILRuntime.Runtime.Generated.CLRBindings.Initialize(mAppDomain);
        //重新定向

    }
    /// <summary>
    /// 注册委托
    /// </summary>
    private void RegisterDelegates()
    {
        mAppDomain.DelegateManager.RegisterMethodDelegate<IViewBaseAdaptor.Adaptor>();

        mAppDomain.DelegateManager.RegisterFunctionDelegate<Adapt_IMessage.Adaptor>();
        mAppDomain.DelegateManager.RegisterMethodDelegate<Adapt_IMessage.Adaptor>();

        mAppDomain.DelegateManager.RegisterFunctionDelegate<System.Int32, System.Int32>();

        mAppDomain.DelegateManager.RegisterDelegateConvertor<EventCode.EventCallBack>((act) =>
        {
            return new EventCode.EventCallBack(() =>
            {
                ((Action)act)();
            });
        });
        mAppDomain.DelegateManager.RegisterDelegateConvertor<EventCode.EventCallBack>((act) =>
        {
            return new EventCode.EventCallBack<object>((obj) =>
            {
                ((Action<object>)act)(obj);
            });
        });

    }
    /// <summary>
    /// 注册所有热更DLL中用到的跨域继承Adapter，否则无法正确抓取引用
    /// </summary>
    private void RegisterCrossBindingAdaptor()
    {
        //这里需要注册所有热更DLL中用到的跨域继承Adapter，否则无法正确抓取引用
       // mAppDomain.RegisterCrossBindingAdaptor(new MonoBehaviourAdapter());
        //mAppDomain.RegisterCrossBindingAdaptor(new CoroutineAdapter());

        mAppDomain.RegisterValueTypeBinder(typeof(Vector3), new Vector3Binder());
       // mAppDomain.RegisterCrossBindingAdaptor(new IViewBaseAdaptor());

        // 注册适配器
        Assembly assembly = typeof(GameLaunch).Assembly;
        foreach (Type type in assembly.GetTypes())
        {
            object[] attrs = type.GetCustomAttributes(typeof(ILAdapterAttribute), false);
            if (attrs.Length == 0)
            {
                continue;
            }

            object obj = Activator.CreateInstance(type);
            CrossBindingAdaptor adaptor = obj as CrossBindingAdaptor;
            if (adaptor == null)
            {
                continue;
            }

            mAppDomain.RegisterCrossBindingAdaptor(adaptor);
        }

    }

    #region 启动热更新

    public IEnumerator LoadHotFixAssembly()
    {
        bool isEditor = false;

#if UNITY_EDITOR
        isEditor = AssetBundleConfig.IsEditorMode;
#endif
        if (isEditor)
        {
            WWW www = new WWW("file:///" + Application.streamingAssetsPath + "/HotFix_Project.awb");//.awb
            while (!www.isDone)
                yield return null;
            if (!string.IsNullOrEmpty(www.error))
                UnityEngine.Debug.LogError(www.error);
            byte[] dll = www.bytes;
            www.Dispose();

            //PDB文件是调试数据库，如需要在日志中显示报错的行号，则必须提供PDB文件，不过由于会额外耗用内存，正式发布时请将PDB去掉，下面LoadAssembly的时候pdb传null即可
            www = new WWW("file:///" + Application.streamingAssetsPath + "/HotFix_Project_PDB.awb");
            while (!www.isDone)
                yield return null;
            if (!string.IsNullOrEmpty(www.error))
                UnityEngine.Debug.LogError(www.error);
            byte[] pdb = www.bytes;

            try
            {
               InitApp(dll, pdb);
            }
            catch
            {
                Debug.LogError("加载热更DLL失败，请确保已经通过VS打开Assets/Samples/ILRuntime/1.6/Demo/HotFix_Project/HotFix_Project.sln编译过热更DLL");
            }
            InitializeILRuntime();
            OnHotFixLoaded();
        }
        else//todo
        {

            string path = AssetBundleUtility.PackagePathToAssetsPath("HfModule");
            string hotFixAssetbundleName = AssetBundleUtility.AssetBundlePathToAssetBundleName(path);
            AssetBundleManager.Instance.SetAssetBundleResident(hotFixAssetbundleName, true);
            var abloader = AssetBundleManager.Instance.LoadAssetBundleAsync(hotFixAssetbundleName);
            yield return abloader;
            
            byte[] dll_P = Load("HotFix_Project.bytes");
            HotFixMangager.instance.InitApp(dll_P, null);
            InitializeILRuntime();
            OnHotFixLoaded();
        }



    }

    public byte[] Load(string filepath)
    {
        string dataPath = string.Format("{0}/{1}", "HfModule", filepath);
        string assetbundleName = null;
        string assetName = null;
        bool status = AssetBundleManager.Instance.MapAssetPath(dataPath, out assetbundleName, out assetName);
        if (!status)
        {
            Logger.LogError("MapAssetPath failed : " + dataPath);
            return null;
        }
        var asset = AssetBundleManager.Instance.GetAssetCache(assetName) as TextAsset;
        if (asset != null)
        {
            //Logger.Log("Load lua script : " + dataPath);
            return asset.bytes;
        }

        Logger.LogError("Load data failed : " + dataPath + ", You should preload data assetbundle first!!!");
        return null;
    }

    void InitializeILRuntime()
    {
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
        //由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
        HotFixMangager.instance.GetAppDomain().UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
        //打开调试
        HotFixMangager.instance.GetAppDomain().DebugService.StartDebugService(56000);
        //这里做一些ILRuntime的注册，HelloWorld示例暂时没有需要注册的
    }

    void OnHotFixLoaded()
    {
        //启动热更代码的接口，静态方法调用
        HotFixMangager.instance.GetAppDomain().Invoke("HotFix_Project.HotManager", "HotInitialize", null, null);

    }
    #endregion

}
