using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventCode;
using System;
/// 例子 注册委托
public delegate bool Predicete<T>(T arg);
public delegate void Predicete<T,U>(T arg1,U arg2);


public class EventManager 
{
    static public void AddListener(int eventType, Action handler)
    {
        EventCenter.AddListener(eventType,handler);
    }

    //Single parameter
    static public void AddListener<T>(int eventType, Action<T> handler)
    {
        EventCenter.AddListener(eventType, handler);
    }

    //Two parameters
    static public void AddListener<T, U>(int eventType, Action<T, U> handler)
    {
        EventCenter.AddListener(eventType, handler);
    }

    //Three parameters
    static public void AddListener<T, U, V>(int eventType, Action<T, U, V> handler)
    {
        EventCenter.AddListener(eventType, handler);
    }


  
    //No parameters
    static public void RemoveListener(int eventType, Action handler)
    {
        //OnListenerRemoving(eventType, handler);
        EventCenter.RemoveListener(eventType,handler);
    }

    //Single parameter
    static public void RemoveListener<T>(int eventType, Action<T> handler)
    {
        //OnListenerRemoving(eventType, handler);
        EventCenter.RemoveListener(eventType, handler);
    }

    //Two parameters
    static public void RemoveListener<T, U>(int eventType, Action<T, U> handler)
    {
        //OnListenerRemoving(eventType, handler);
        EventCenter.RemoveListener(eventType, handler);
    }

    //Three parameters
    static public void RemoveListener<T, U, V>(int eventType, Action<T, U, V> handler)
    {
        //OnListenerRemoving(eventType, handler);
        EventCenter.RemoveListener(eventType, handler);
    }
    #region SendMessage
    //No parameters
    static public void SendMessage(int eventType)
    {
        EventCenter.Broadcast(eventType);
    }

    //Single parameter
    static public void SendMessage<T>(int eventType, T arg1)
    {
        EventCenter.Broadcast(eventType, arg1);
    }

    //Two parameters
    static public void SendMessage<T, U>(int eventType, T arg1, U arg2)
    {
        EventCenter.Broadcast(eventType, arg1, arg2);
    }

    //Three parameters
    static public void SendMessage<T, U, V>(int eventType, T arg1, U arg2, V arg3)
    {
        EventCenter.Broadcast(eventType, arg1, arg2, arg3);
    }
    #endregion

}
