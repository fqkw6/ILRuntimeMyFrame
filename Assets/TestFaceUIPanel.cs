using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class TestFaceUIPanel 
{   
	public GameObject Show_go;
	public Button Hit_btn;
	public Text Info_txt;


    public void OnAwake(GameObject viewGO)
    {
		Show_go = viewGO.transform.Find("Show_go").gameObject;
		Hit_btn = viewGO.transform.Find("Hit_btn").GetComponent<Button>();
		Info_txt = viewGO.transform.Find("Hit_btn/Info_txt").GetComponent<Text>();

    }

    public void OnDestroy()
    {
		Show_go = null;
		Hit_btn = null;
		Info_txt = null;

    }
}