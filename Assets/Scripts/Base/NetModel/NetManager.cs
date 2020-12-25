using Google.Protobuf;
using Message;
using Networks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NetManager 
{
    HjTcpNetwork hjTcpNetwork = new HjTcpNetwork();
    public void Init()
    {
       
    }
    //注册
    public void RegisterNetModel()
    {
        //myperson myperson = new myperson();
        //myperson.Projects.Add("ss",90);
        //myperson.ArrayValue.Add(new Google.Protobuf.ByteString); 
    }

    /// <summary>
    /// 处理 接受的消息
    /// </summary>
    public void ProcessMessage(byte[] bytes)
    {
        ByteBuffer recive = new ByteBuffer(bytes);
        byte[] data = recive.ReadBytes();
      //  ProtoBufferTool.Deserialize();
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

    }
    public void Update()
    {
        hjTcpNetwork.UpdateNetwork();

    }
    public void Connect(string ip,int port)
    {
        hjTcpNetwork.ReceivePkgHandle = ProcessMessage;
        RegisterNetModel();
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

    public void Dispose()
    {
        hjTcpNetwork.Dispose();
    }
}
