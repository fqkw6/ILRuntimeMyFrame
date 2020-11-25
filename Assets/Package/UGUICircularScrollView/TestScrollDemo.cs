using System.Collections;
using System.Collections.Generic;
using CircularScrollView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestScrollDemo : MonoBehaviour
{

    public UICircularScrollView VerticalScroll_1;
    public UICircularScrollView VerticalScroll_2;
    public UICircularScrollView HorizontalScroll_1;
    public UICircularScrollView HorizontalScroll_2;

    public Button btn;
    public Button Addbtn;
    public Button SetIndexbtn;
    public Button Removebtn;

    // Use this for initialization
    void Start()
    {
        btn.onClick.AddListener(OnClickSeeOtherScroll);
        Addbtn.onClick.AddListener(OnClickAdd);
        SetIndexbtn.onClick.AddListener(OnClickSet);
        Removebtn.onClick.AddListener(OnClickRemove);
        StartScrollView();
    }

    void OnClickSeeOtherScroll()
    {
        SceneManager.LoadScene("OtherDemo");
    }
    void OnClickRemove()
    {
        VerticalScroll_2.Init(NormalCallBack);
        VerticalScroll_2.ShowList(20);
    }
    void OnClickAdd()
    {
        VerticalScroll_2.Init(NormalCallBack);
        VerticalScroll_2.ShowList(60);
    }
    int t = 22;
    void OnClickSet()
    {

        VerticalScroll_2.SetToPageIndex(t++);
    }


    public void StartScrollView()
    {
        VerticalScroll_1.Init(NormalCallBack);
        VerticalScroll_1.ShowList(50);

        VerticalScroll_2.Init(NormalCallBack);
        VerticalScroll_2.ShowList(50);

        HorizontalScroll_1.Init(NormalCallBack);
        HorizontalScroll_1.ShowList(50);

        HorizontalScroll_2.Init(NormalCallBack);
        HorizontalScroll_2.ShowList(50);
    }

    private void NormalCallBack(GameObject cell, int index)
    {
        cell.transform.Find("Text1").GetComponent<Text>().text = index.ToString();
        cell.transform.Find("Text2").GetComponent<Text>().text = index.ToString();
    }


}
