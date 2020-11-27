﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class ClassInfo
{
    public string Name;
    public UnityEngine.Object Object;
    public string Type;
}

public class MonoView : MonoBehaviour
{
    public List<ClassInfo> infos = new List<ClassInfo>();
    private IViewBase mView;

    public IViewBase View
    {
        get
        {
            return mView;
        }
    }

    public void AddView(string className)
    {
        if (mView != null) return;
        IViewBase view = HotFixMangager.instance.GetAppDomain().Instantiate<IViewBase>(className);

        if (view == null)
        {
            Debug.LogError("is Not=="+className);
            return;
        }
#if UNITY_EDITOR
        ClassInfo info = new ClassInfo();
        info.Name = className;
        info.Object = gameObject;
        info.Type = typeof(MonoView).ToString();
        infos.Add(info);
        Debug.LogError(info+"jiajiajdijaisdjai");
#endif
        AddView(view);
    }

    public void AddView(IViewBase view)
    {
        try
        {
            mView = view;
            if (mView != null)
            {
                mView.SetGameObject(gameObject);
                if (gameObject.activeInHierarchy)
                {
                    mView.Awake();
                    mView.OnEnable();
                }
            }
        }
        catch (System.Exception e)
        {

            Debug.LogError(e);
        }
    }

    private void Awake()
    {
        if (mView != null)
        {
            mView.Awake();
        }
    }
    private void OnEnable()
    {
        if (mView != null)
        {
            mView.OnEnable();
        }
    }
    private void Start()
    {
        if (mView != null)
        {
            mView.Start();
        }
    }
    private void Update()
    {
        if (mView != null)
        {
            mView.Update();
        }
    }
    private void OnDisable()
    {
        if (mView != null)
        {
            mView.OnDisable();
        }
    }
    private void OnDestroy()
    {
        if (mView != null)
        {
            mView.OnDestroy();
        }
    }
   
}
