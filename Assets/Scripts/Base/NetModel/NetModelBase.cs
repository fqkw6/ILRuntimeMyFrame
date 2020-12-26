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
        AddListening(10001, JieShouInfo);
    }

    private void JieShouInfo(NetMessageBase obj)
    {
        Debug.LogError(obj.MessageId+ "obj.MessageId");
        byte[] s = obj.MessageBoby.ToByteArray();
        Role.role_info info= ProtoBufferTool.DeserializeNew<Role.role_info>(Role.role_info.Parser,s);
        Debug.LogError(info.Name);
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
