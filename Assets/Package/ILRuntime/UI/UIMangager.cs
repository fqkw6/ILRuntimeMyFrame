using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssetBundles;
using Google.Protobuf;
using System.IO;
using UnityEngine.UI;

public class UIMangager 
{
    private static UIMangager instance;

    private UIMangager()
    {

    }
    public static UIMangager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIMangager();
            }
            return instance;
        }
    }
    public event Predicete<int> PrediceteEventTest1;
    public event Predicete<int, int> PrediceteEventTest2;
    public  void LoadUI<T>(string realPath, string className, Transform parent,Action<T> callBack) where T : IViewBase
    {
        GameObject go = null;
        AssetBundleManager.Instance.LoadAssetAsync<GameObject>(realPath, (obj) =>
        {
            go = obj as GameObject;
            GameObject gameObject = GameObject.Instantiate(go) as GameObject;
            gameObject.transform.SetParent(parent);
            UIObject uiObj = gameObject.GetComponent<UIObject>();
            if (uiObj == null)
            {
                uiObj = gameObject.AddComponent<UIObject>();
            }
            uiObj.OnInStanceWitAsset(realPath);
            uiObj.obj = gameObject;

            if (typeof(T) == typeof(IViewBaseAdaptor.Adaptor))
            {

                MonoView view = gameObject.GetComponent<MonoView>();
                view.AddView(className);
                if (callBack != null) callBack((T)view.View);

            }
            else
            {
                MonoView view = gameObject.GetComponent<MonoView>();
                if (null != view)
                {
                    T t = Activator.CreateInstance<T>();
                    view.AddView((IViewBase)t);
                    if (callBack != null) callBack(t);
                }
                else
                {
                    if (callBack != null) callBack(gameObject.GetComponent<T>());
                }
            }

        });
       
    }
    public void LoadUI<T>(string realPath, int num, string className, Action<List<T>> callBack) where T : IViewBase
    {
        GameObject go = null;
        List<T> list = new List<T>();

        try
        {
            AssetBundleManager.Instance.LoadAssetAsync<GameObject>(realPath,(obj)=> 
            {
                go = obj as GameObject;
                
                for (int i = 0; i < num; i++)
                {
                    GameObject gameObject = GameObject.Instantiate(go) as GameObject;
                    if (typeof(T) == typeof(IViewBaseAdaptor.Adaptor))
                    {

                        MonoView view = gameObject.GetComponent<MonoView>();
                        view.AddView(className);

                        list.Add((T)view.View);
                    }
                    else
                    {
                        MonoView view = gameObject.GetComponent<MonoView>();
                        if (null != view)
                        {
                            T t = Activator.CreateInstance<T>();
                            view.AddView((IViewBase)t);
                            list.Add(t);
                        }
                        else
                        {
                            list.Add(gameObject.GetComponent<T>());
                        }
                    }
                    UIObject uiObj = gameObject.GetComponent<UIObject>();
                    if (uiObj == null)
                    {
                        uiObj = gameObject.AddComponent<UIObject>();
                    }
                    uiObj.OnInStanceWitAsset(realPath);
                    uiObj.obj = gameObject;
                }
                if (callBack != null)
                {
                    callBack(list);
                }

            });
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        // 实例化
    }

    /// <summary>
    /// HUD面板栈
    /// </summary>
    private Dictionary<int, IViewBase> mDicPanels = new Dictionary<int, IViewBase>();

    public void OpenUI<T>(int panelId,string realPath, string className, Transform parent, Action<T> callBack) where T : IViewBase
    {
        GameObject go = null;
        if (mDicPanels.ContainsKey(panelId))
        {
            Debug.LogError("已经打开过该面板"+className);
            return;
        }
       AssetBundleManager.Instance.LoadAssetAsync<GameObject>(realPath, (obj) =>
        {
            T viewT = default(T);
            go = obj as GameObject;
            GameObject gameObject = GameObject.Instantiate(go) as GameObject;
            gameObject.transform.SetParent(parent);
            UIObject uiObj = gameObject.GetComponent<UIObject>();
            if (uiObj == null)
            {
                uiObj = gameObject.AddComponent<UIObject>();
            }
            uiObj.OnInStanceWitAsset(realPath);
            uiObj.obj = gameObject;

            if (typeof(T) == typeof(IViewBaseAdaptor.Adaptor))
            {

                MonoView view = gameObject.GetComponent<MonoView>();
                view.AddView(className);
                if (callBack != null) callBack((T)view.View);
                viewT = (T)view.View;
            }
            else
            {
                MonoView view = gameObject.GetComponent<MonoView>();
                if (null != view)
                {
                    T t = Activator.CreateInstance<T>();
                    view.AddView((IViewBase)t);
                    if (callBack != null) callBack(t);
                    viewT = t;
                }
                else
                {
                    if (callBack != null) callBack(gameObject.GetComponent<T>());
                    viewT = gameObject.GetComponent<T>();
                }
            }

            if (!mDicPanels.ContainsKey(panelId))
            {
                mDicPanels.Add(panelId, viewT);
            }

        });
    }

    public void CloseUI(int panelId)
    {
        IViewBase viewBase;
        if (mDicPanels.TryGetValue(panelId, out viewBase))
        {
            Debug.LogError(viewBase.GetGameObject()+"cccceded");
            if (viewBase.GetGameObject() != null)
            {
                GameObject.Destroy(viewBase.GetGameObject());
            }
            mDicPanels.Remove(panelId);
        }
    }

    public T GetUIPanel<T>(int panelId) where T : IViewBase
    {
        IViewBase viewBase;
        if (mDicPanels.TryGetValue(panelId, out viewBase))
        {
            return (T)viewBase;
        }
        return (T)viewBase;
    }


    public void RTYU()
    {
        PrediceteEventTest1(20);
    }
    public void RTYU2()
    {
        PrediceteEventTest2(20,109);
    }
}
