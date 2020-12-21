using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventCode;
public class EventManager 
{
    static public void AddListener(int eventType, EventCallBack handler)
    {
        EventCenter.AddListener(eventType,handler);
    }

    //Single parameter
    static public void AddListener<T>(int eventType, EventCallBack<T> handler)
    {
        EventCenter.AddListener(eventType, handler);
    }

    //Two parameters
    static public void AddListener<T, U>(int eventType, EventCallBack<T, U> handler)
    {
        EventCenter.AddListener(eventType, handler);
    }

    //Three parameters
    static public void AddListener<T, U, V>(int eventType, EventCallBack<T, U, V> handler)
    {
        EventCenter.AddListener(eventType, handler);
    }


  
    //No parameters
    static public void RemoveListener(int eventType, EventCallBack handler)
    {
        //OnListenerRemoving(eventType, handler);
        EventCenter.RemoveListener(eventType,handler);
    }

    //Single parameter
    static public void RemoveListener<T>(int eventType, EventCallBack<T> handler)
    {
        //OnListenerRemoving(eventType, handler);
        EventCenter.RemoveListener(eventType, handler);
    }

    //Two parameters
    static public void RemoveListener<T, U>(int eventType, EventCallBack<T, U> handler)
    {
        //OnListenerRemoving(eventType, handler);
        EventCenter.RemoveListener(eventType, handler);
    }

    //Three parameters
    static public void RemoveListener<T, U, V>(int eventType, EventCallBack<T, U, V> handler)
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
