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
        Debug.LogError(rawImage + "ceshi====");
        Debug.LogError(GetGameObject());

       

        Role.ReqGet role_Info2 = new Role.ReqGet();

        role_Info2.Name = "woshi";
      

        byte[] msg1 = ProtoBufferTool.Serialize(role_Info2);
        NetMessageBase netMessageBase1 = new NetMessageBase();
        netMessageBase1.MessageId = 300;
        netMessageBase1.MessageBoby = ByteString.CopyFrom(msg1);
        byte[] by1 = ProtoBufferTool.Serialize(netMessageBase1);

        NetMessageBase newooo = ProtoBufferTool.Deserialize(NetMessageBase.Parser, by1) as NetMessageBase;
        Debug.LogError(newooo.MessageId + "==newooo.MessageId==");
        Role.ReqGet info2 = ProtoBufferTool.Deserialize(Role.ReqGet.Parser, newooo.MessageBoby.ToByteArray()) as Role.ReqGet;
        Debug.LogError(info2.Name + "==info2.Name ==");



        EventManager.SendMessage((int)ILEventName.LoginTest);
        EventManager.SendMessage<Yu>((int)ILEventName.LoginTest2, new Yu() {name="WangBadao" });
    }

    public override void AddListener()
    {
        EventManager.AddListener((int)ILEventName.LoginTest, TestCCallBack);
        EventManager.AddListener<Yu>((int)ILEventName.LoginTest2, TestCCallBack);
    }

    private void TestCCallBack(Yu arg1)
    {
        Debug.LogError("收到" + arg1.name);
    }

    private void TestCCallBack()
    {
        Debug.LogError("收到");
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