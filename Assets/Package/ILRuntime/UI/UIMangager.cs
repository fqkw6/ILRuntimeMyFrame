using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssetBundles;
public class UIMangager 
{
    public static T LoadUI<T>(string realPath, string className) where T : IViewBase
    {
        List<T> list = LoadUI<T>(realPath, 1, className);
        if (list.Count == 0)
        {
            return default(T);
        }
        else
        {
            return list[0];
        }
    }
    public static List<T> LoadUI<T>(string realPath, int num, string className) where T : IViewBase
    {
        GameObject go = null;
        try
        {
            AssetBundleManager.Instance.LoadAssetAsync<GameObject>(realPath,(obj)=> 
            {
                go = obj as GameObject;
            });
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        if (null == go)
        {
            return new List<T>();
        }

        List<T> list = new List<T>();
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
        // 实例化

        return list;
    }

    public static void OpenPanel<T>(string realPath, string className,Transform parent, Action<T> callBack = null) where T : IViewBase
    {
        T ui =  LoadUI<T>(realPath,className);
        if (ui != null)
        {
            if (callBack != null)
            {
                callBack(ui);
            } 
            ui.GetGameObject().transform.SetParent(parent);
        }
        else
        {
            Debug.LogError("加载失败");
        }
    }



}
