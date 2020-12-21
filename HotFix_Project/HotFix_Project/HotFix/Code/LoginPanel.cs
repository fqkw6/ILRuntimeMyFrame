using UnityEngine;
using UnityEngine.UI;

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

}
public class Yu
{
    public string name;
}