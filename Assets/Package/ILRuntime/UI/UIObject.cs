using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObject : MonoBehaviour
{
    [HideInInspector]
    public UnityEngine.Object obj;
    private string m_assetPath = "";

    public void OnInStanceWitAsset(string assetPath)
    {
        if (!string.IsNullOrEmpty(assetPath))
        {
            m_assetPath = assetPath;
            //toto  加载预设
        }
    }
    private void OnDestroy()
    {
        if (!string.IsNullOrEmpty(m_assetPath))
        {

            //to do 释放预设
        }
    }
}
