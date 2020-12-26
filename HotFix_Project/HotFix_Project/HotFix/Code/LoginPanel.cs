using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Message;
using Google.Protobuf;

class LoginPanel : UIPanelBase
{
    private const string assetAddress= "UI/Prefabs/View/UILaunch.prefab";
    public LoginPanel() : base(PanelName.LoginPanel, assetAddress, PanelType.Normal)
    {

    }
    public override void OnEnable()
    {
        base.OnEnable();
    }
    public override void OnDisable()
    {
        base.OnDisable();
    }

    public override void Start()
    {
        RawImage rawImage = FindComponent<RawImage>("BgRoot/BG");
        UIMangager.Instance.PrediceteEventTest1 += TestKLKL;
        UIMangager.Instance.PrediceteEventTest2 += tdddj;
        Debug.LogError(rawImage + "ceshi====");
        Debug.LogError(GetGameObject());

        Role.ReqGet role_Info2 = new Role.ReqGet();

        role_Info2.Name = "woshilll尽快";
      

        byte[] msg1 = ProtoBufferTool.Serialize(role_Info2);
        NetMessageBase netMessageBase1 = new NetMessageBase();
        netMessageBase1.MessageId = 300;
        netMessageBase1.MessageBoby = ByteString.CopyFrom(msg1);
        byte[] by1 = ProtoBufferTool.Serialize(netMessageBase1);

        NetMessageBase newooo = ProtoBufferTool.Deserialize(NetMessageBase.Parser, by1) as NetMessageBase;
        Debug.LogError(newooo.MessageId + "==newooo.MessageId==");
        Role.ReqGet info2 = ProtoBufferTool.DeserializeNew<Role.ReqGet>(Role.ReqGet.Parser, newooo.MessageBoby.ToByteArray());
        Debug.LogError(info2.Name + "==info2.Name ==");
        EventManager.SendMessage((int)10000);

        EventManager.SendMessage<Yu>((int)20000, new Yu() {name="WangBadao" });
        EventManager.SendMessage<Mu>((int)30000, new Mu() { name = "还记得" });
        Debug.LogError(Parent + "==Parent=");
        Debug.LogError(PanelId+ "==PanelId=");
    }

    public override void AddListener()
    {
        EventManager.AddListener((int)10000, TestCCallBack);
        EventManager.AddListener<Yu>((int)20000, TestCCallBack);
      
        EventManager.AddListener<Mu>((int)30000, TestCCallBack);
     
    }

    private void tdddj(int arg1, int arg2)
    {
        Debug.LogError("ecec=="+arg1+arg2);
    }

    private bool TestKLKL(int arg)
    {
        Debug.LogError(arg);
        return arg > 10;
    }

    public override void RemoveListener()
    {
        base.RemoveListener();
        EventManager.RemoveListener((int)10000, TestCCallBack);
        EventManager.RemoveListener<Yu>((int)20000, TestCCallBack);
        EventManager.RemoveListener<Mu>((int)30000, TestCCallBack);
    }
    private void TestCCallBack(Yu arg1)
    {
        Debug.LogError("收到热更" + arg1.name);
    }
    private void TestCCallBack(Mu arg1)
    {
        Debug.LogError("收到主工程的类" + arg1.name);
        
    }
  
    private void TestCCallBack()
    {
        Debug.LogError("收到主工程发来的消息");
    }
    private Dictionary<int, int> dic = new Dictionary<int, int>();

    public void PianLi()
    {
        Dictionary<int, int>.Enumerator enumerator = dic.GetEnumerator();
        while (enumerator.MoveNext())
        {
            KeyValuePair<int, int> current = enumerator.Current;
            int key = current.Key;
            current = enumerator.Current;
            int num3 = current.Value;
        }


        Dictionary<int, int> dicTemp = dic;
        var k= dic.Keys.GetEnumerator();

        while (k.MoveNext())
        {
            int current = k.Current;
            int value2 = dicTemp[current];
        }
    }
}
public class Yu
{
    public string name;
}