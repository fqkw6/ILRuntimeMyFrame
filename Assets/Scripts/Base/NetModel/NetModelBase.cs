using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Google.Protobuf;
using Message;

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


    public void AddListening(uint netEnum, Action<NetMessageBase> action)
    {
        NetManager.Instance.AddListening(netEnum, action);
    }

    public void RemoveListening(uint netEnum, Action<NetMessageBase> action) 
    {
        NetManager.Instance.RemoveListening(netEnum, action);
    }
}
