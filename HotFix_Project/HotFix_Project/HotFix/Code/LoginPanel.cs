using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
class LoginPanel : UIPanelBase
{
    public LoginPanel(PanelName panelName, string assetAddress, PanelType panelType) : base(panelName, assetAddress, panelType)
    {
    }

    public override void Start()
    {
        RawImage rawImage = FindComponent<RawImage>("BgRoot/BG");
        Debug.LogError(rawImage);
    }

}