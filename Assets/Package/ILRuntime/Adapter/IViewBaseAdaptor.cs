using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.CLR.Method;
using System;
[ILAdapter]
public class IViewBaseAdaptor : CrossBindingAdaptor
{
    public override Type BaseCLRType
    {
        get
        {
            return typeof(IViewBase);
        }
    }

    public override Type AdaptorType
    {
        get
        {
            return typeof(Adaptor);
        }
    }

    public override object CreateCLRInstance(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
    {
        return new Adaptor(appdomain, instance);
    }
    //为了完整实现MonoBehaviour的所有特性，这个Adapter还得扩展，这里只抛砖引玉，只实现了最常用的Awake, Start和Update
    public class Adaptor : IViewBase, CrossBindingAdaptorType
    {
        ILTypeInstance instance;
        ILRuntime.Runtime.Enviorment.AppDomain appdomain;
        //缓存这个数据来避免调用时的GC Alloc
        object[] paraml = new object[1];
        public Adaptor()
        {

        }

        public Adaptor(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
        {
            this.appdomain = appdomain;
            this.instance = instance;
        }

        public ILTypeInstance ILInstance { get { return instance; } set { instance = value; } }

        public ILRuntime.Runtime.Enviorment.AppDomain AppDomain { get { return appdomain; } set { appdomain = value; } }



        public void Awake()
        {
            IMethod method = null;
            method = instance.Type.GetMethod("Awake",0);
            if (method == null)
            {
                Debug.LogError("Awake Error");
                return;
            }
            else
            {
                appdomain.Invoke(method,instance,null);
            }
        }
        public void OnEnable()
        {
            IMethod method = null;
            method = instance.Type.GetMethod("OnEnable", 0);
            if (method == null)
            {
                Debug.LogError("OnEnable Error");
                return;
            }
            else
            {
                appdomain.Invoke(method, instance, null);
            }
        }
        public void Start()
        {
            IMethod method = null;
            method = instance.Type.GetMethod("Start", 0);
            if (method == null)
            {
                Debug.LogError("Start Error");
                return;
            }
            else
            {
                appdomain.Invoke(method, instance, null);
            }
        }

        public void Update()
        {
            IMethod method = null;
            method = instance.Type.GetMethod("Update", 0);
            if (method == null)
            {
                Debug.LogError("Update Error");
                return;
            }
            else
            {
                appdomain.Invoke(method, instance, null);
            }
        }
        public void OnDisable()
        {
            IMethod method = null;
            method = instance.Type.GetMethod("OnDisable", 0);
            if (method == null)
            {
                Debug.LogError("OnDisable Error");
                return;
            }
            else
            {
                appdomain.Invoke(method, instance, null);
            }
        }
        public void OnDestroy()
        {
            IMethod method = null;
            method = instance.Type.GetMethod("OnDestroy", 0);
            if (method == null)
            {
                Debug.LogError("OnDestroy Error");
                return;
            }
            else
            {
                appdomain.Invoke(method, instance, null);
            }
        }

        public GameObject GetGameObject()
        {
            IMethod method = null;
            method = instance.Type.GetMethod("GetGameObject", 0);
            if (method == null)
            {
                Debug.LogError("GetGameObject Error");
                return null;
            }
            else
            {
                var res = appdomain.Invoke(method, instance, null);
                return (GameObject)res;
            }
        }

        public void SetGameObject(GameObject go)
        {
            IMethod method = null;
            method = instance.Type.GetMethod("SetGameObject", 1);
            if (method == null)
            {
                Debug.LogError("SetGameObject Error");
                return;
            }
            else
            {
                paraml[0] = go;
                appdomain.Invoke(method, instance, paraml);
                Debug.LogError("shezhi "+go);
            }
        }
        ///属性 特殊类
        public GameObject Parent
        {
            get
            {
                IMethod method = null;
                method = instance.Type.GetMethod("get_Parent", 0);
                if (method == null)
                {
                    Debug.LogError("get_Parent Error");
                    return null;
                }
                else
                {
                    var res = appdomain.Invoke(method, instance, null);
                    return (GameObject)res;
                }
            }
            set
            {
                IMethod method = null;
                method = instance.Type.GetMethod("set_Parent", 0);
                if (method == null)
                {
                    Debug.LogError("set_Parent Error");
                    return;
                }
                else
                {
                    paraml[0] = value;
                    appdomain.Invoke(method, instance, paraml[0]);

                }
            }
        }

        public int PanelId
        {
            get
            {
                return 0;
            }
            set
            {

            }
        }

    }
}
