using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssetBundles;
using Google.Protobuf;
using System.IO;

public class UIMangager 
{
    public static void LoadUI<T>(string realPath, string className, Transform parent,Action<T> callBack) where T : IViewBase
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
    public static void LoadUI<T>(string realPath, int num, string className, Action<List<T>> callBack) where T : IViewBase
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

    public static void OpenPanel<T>(string realPath, string className,Transform parent, Action<T> callBack = null) where T : IViewBase
    {
        Debug.LogError(typeof(T)+"===fjsjdkhfdkjshfkj");
        LoadUI<T>(realPath,className, parent,callBack);
    }

    public static IMessage Deserialize(MessageParser _type, byte[] byteData)
    {
        Stream stream = new MemoryStream(byteData);
        if (stream != null)
        {
            IMessage t = _type.ParseFrom(stream);
            stream.Close();
            return t;
        }
        stream.Close();
        return default(IMessage);
    }
    public static byte[] Serialize(IMessage _data)
    {
        MemoryStream stream = new MemoryStream();
        if (stream != null)
        {
            _data.WriteTo(stream);
            byte[] bytes = stream.ToArray();
            stream.Close();
            return bytes;
        }
        stream.Close();
        return null;
    }

}
