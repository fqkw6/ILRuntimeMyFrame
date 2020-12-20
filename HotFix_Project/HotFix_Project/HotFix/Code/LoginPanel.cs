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
        Debug.LogError(rawImage+"ceshi====");
        Debug.LogError(GetGameObject());
    }

}