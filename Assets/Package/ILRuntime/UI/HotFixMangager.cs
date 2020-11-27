using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;
using System.IO;

public class HotFixMangager : Singleton<HotFixMangager>
{

    AppDomain mAppDomain;

    public void InitApp(byte [] dllBytes,byte [] pdbBytes)
    {
        mAppDomain = new AppDomain();
        MemoryStream dll = (dllBytes != null) ? new MemoryStream(dllBytes) : null;
        MemoryStream pdb = (pdbBytes != null) ? new MemoryStream(pdbBytes) : null;
        mAppDomain.LoadAssembly(dll, pdb, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
        RegisterValueTypeBinder();
        RegisterCrossBindingAdaptor();
        RegisterCLRMethodRedirection();
        RegisterDelegates();

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
    private void RegisterDelegates()
    {
        mAppDomain.DelegateManager.RegisterMethodDelegate<IViewBaseAdaptor.Adaptor>();
    }
    /// <summary>
    /// 注册所有热更DLL中用到的跨域继承Adapter，否则无法正确抓取引用
    /// </summary>
    private void RegisterCrossBindingAdaptor()
    {
        //这里需要注册所有热更DLL中用到的跨域继承Adapter，否则无法正确抓取引用
        mAppDomain.RegisterCrossBindingAdaptor(new MonoBehaviourAdapter());
        mAppDomain.RegisterCrossBindingAdaptor(new CoroutineAdapter());

        mAppDomain.RegisterValueTypeBinder(typeof(Vector3), new Vector3Binder());
        mAppDomain.RegisterCrossBindingAdaptor(new IViewBaseAdaptor());
    }


}
