using Google.Protobuf;
using Message;
using Networks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NetManager :MonoSingleton<NetManager>
{
    HjTcpNetwork hjTcpNetwork = new HjTcpNetwork();

    public Dictionary<uint, Action<NetMessageBase>> mMessageAllDic = new Dictionary<uint, Action<NetMessageBase>>();
    public NetModelBase mNetModelBase;
    //注册
    public void RegisterNetModel()
    {
        mNetModelBase = new NetModelBase();
    }
    public void AddListening(uint netEnum, Action<NetMessageBase> action) 
    {
        if (!mMessageAllDic.ContainsKey(netEnum))
        {
            mMessageAllDic.Add(netEnum,action);
        }
    }

    public void RemoveListening(uint netEnum, Action<NetMessageBase> action) 
    {
        if (!mMessageAllDic.ContainsKey(netEnum))
        {
            mMessageAllDic.Add(netEnum, action);
        }
    }
    /// <summary>
    /// 处理 接受的消息
    /// </summary>
    public void ProcessMessage(byte[] bytes)
    {
        ByteBuffer recive = new ByteBuffer(bytes);
        byte[] data = recive.ReadBytes();
        NetMessageBase msg = ProtoBufferTool.Deserialize(NetMessageBase.Parser, data) as NetMessageBase;
        if (mMessageAllDic.TryGetValue(msg.MessageId, out Action<NetMessageBase> messageBoby))
        {
            messageBoby(msg);
        }
    }

    public void SendMessage(uint id, byte [] msg)
    {
        ByteBuffer send = new ByteBuffer();
        NetMessageBase netMessageBase = new NetMessageBase();
        netMessageBase.MessageId = id;
        netMessageBase.MessageBoby = ByteString.AttachBytes(msg);
        byte[] by = ProtoBufferTool.Serialize(netMessageBase);
       
        send.WriteBytes(by);

        hjTcpNetwork.SendMessage(send.ToBytes());
        Debug.LogError("fasongchangdu==="+send.ToBytes().Length);
    }
    public void Update()
    {
        hjTcpNetwork.UpdateNetwork();

    }
    public void Connect(string ip,int port)
    {
        RegisterNetModel();
        hjTcpNetwork.ReceivePkgHandle = ProcessMessage;
        hjTcpNetwork.OnConnect = onConnect;
        hjTcpNetwork.OnClosed = onClose;
        hjTcpNetwork.SetHostPort(ip,port);
        hjTcpNetwork.Connect();
        Debug.LogError("Connect to " + ip + ", port :" + port);
    }

    private void onClose(object arg1, int arg2, string arg3)
    {
        if (arg2 < 0)
        {
            Debug.LogError("Close err : " + arg3);
        }
    }

    private void onConnect(object arg1, int arg2, string arg3)
    {
        if (arg2<0)
        {
            Debug.LogError("Connect err : " + arg3);
        }
    }

    public override void Dispose()
    {
        hjTcpNetwork.Dispose();
    }
}
