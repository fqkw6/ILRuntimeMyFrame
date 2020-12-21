using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Mu
{
    public string name;
}
public class TetsMessage : MonoBehaviour
{
    // Start is called before the first frame update

    public event Callback x;
    public event Callback <Mu>x1;
    void Awake()
    {
        Debug.Log("fasong");
        EventDispatcher.Global.Regist((int)EventEnum.TestA, TestACallBack);
      
        EventManager.AddListener(2, TestCCallBack);
        EventManager.AddListener<Mu>(1, TestCCallBack);
    }

    private void TestCCallBack(Mu arg1)
    {
        Debug.LogError("收到"+arg1.name);
    }

    private void TestCCallBack()
    {
        Debug.LogError("收到");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TestACallBack(params object[] objs)
    {
        Debug.LogError(objs[0]);

    }

   
}
