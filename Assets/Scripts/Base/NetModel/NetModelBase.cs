using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NetModelBase
{
    public NetModelBase()
    {
        Register();
    }
    public virtual void Register()
    {
        Debug.LogError("注册消息");
    }


    public void AddListening<T>(NetEnum netEnum, Action<T> action)
    {

    }

    public void RemoveListening<T>(NetEnum netEnum, Action<T> action)
    {

    }
}
