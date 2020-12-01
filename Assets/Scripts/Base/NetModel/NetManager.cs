using Networks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetMessageBase
{
    public int MessageId;//消息头
    public byte[] MessageBoby;//消息体
}
public class NetManager 
{
    HjTcpNetwork hjTcpNetwork = new HjTcpNetwork();
    public void Init()
    {
        hjTcpNetwork.ReceivePkgHandle = ProcessMessage;
        RegisterNetModel();
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

    public void SendMessage(int id, byte[] bytes)
    {
        ByteBuffer send = new ByteBuffer(bytes);

    }
    public void Update()
    {
        hjTcpNetwork.UpdateNetwork();

    }
    public void Connect()
    {
      
    }
    public void Dispose()
    {
        hjTcpNetwork.Dispose();
    }
}
