using System;
using System.Collections.Generic;
using UnityEngine;
namespace EventCode
{
    public delegate void EventCallBack();
    public delegate void EventCallBack<T>(T arg);
    public delegate void EventCallBack<T, X>(T arg1, X arg2);
    public delegate void EventCallBack<T, X, Y>(T arg1, X arg2, Y arg3);
    public delegate void EventCallBack<T, X, Y, Z>(T arg1, X arg2, Y arg3, Z arg4);
    public delegate void EventCallBack<T, X, Y, Z, W>(T arg1, X arg2, Y arg3, Z arg4, W arg5);
    //public delegate void EventCallBack(params object[] param);
    public class EventCenter
    {

        private static Dictionary<int, Delegate> m_EventTable = new Dictionary<int, Delegate>();

        private static void OnListenerAdding(int id, Delegate callBack)
        {
            if (!m_EventTable.ContainsKey(id))
            {
                m_EventTable.Add(id, null);
            }
            Delegate d = m_EventTable[id];
            if (d != null && d.GetType() != callBack.GetType())
            {
                throw new Exception(string.Format("尝试为事件{0}添加不同类型的委托，当前事件所对应的委托是{1}，要添加的委托类型为{2}", id, d.GetType(), callBack.GetType()));
            }
        }
        private static void OnListenerRemoving(int id, Delegate callBack)
        {
            if (m_EventTable.ContainsKey(id))
            {
                Delegate d = m_EventTable[id];
                if (d == null)
                {
                    throw new Exception(string.Format("移除监听错误：事件{0}没有对应的委托", id));
                }
                else if (d.GetType() != callBack.GetType())
                {
                    throw new Exception(string.Format("移除监听错误：尝试为事件{0}移除不同类型的委托，当前委托类型为{1}，要移除的委托类型为{2}", id, d.GetType(), callBack.GetType()));
                }
            }
            else
            {
                throw new Exception(string.Format("移除监听错误：没有事件码{0}", id));
            }
        }
        private static void OnListenerRemoved(int id)
        {
            if (m_EventTable[id] == null)
            {
                m_EventTable.Remove(id);
            }
        }
        //no parameters
        public static void AddListener(int id, EventCallBack callBack)
        {
            OnListenerAdding(id, callBack);
            m_EventTable[id] = (EventCallBack)m_EventTable[id] + callBack;
        }
        //Single parameters
        public static void AddListener<T>(int id, EventCallBack<T> callBack)
        {
            OnListenerAdding(id, callBack);
            m_EventTable[id] = (EventCallBack<T>)m_EventTable[id] + callBack;
        }
        //two parameters
        public static void AddListener<T, X>(int id, EventCallBack<T, X> callBack)
        {
            OnListenerAdding(id, callBack);
            m_EventTable[id] = (EventCallBack<T, X>)m_EventTable[id] + callBack;
        }
        //three parameters
        public static void AddListener<T, X, Y>(int id, EventCallBack<T, X, Y> callBack)
        {
            OnListenerAdding(id, callBack);
            m_EventTable[id] = (EventCallBack<T, X, Y>)m_EventTable[id] + callBack;
        }
        //four parameters
        public static void AddListener<T, X, Y, Z>(int id, EventCallBack<T, X, Y, Z> callBack)
        {
            OnListenerAdding(id, callBack);
            m_EventTable[id] = (EventCallBack<T, X, Y, Z>)m_EventTable[id] + callBack;
        }
        //five parameters
        public static void AddListener<T, X, Y, Z, W>(int id, EventCallBack<T, X, Y, Z, W> callBack)
        {
            OnListenerAdding(id, callBack);
            m_EventTable[id] = (EventCallBack<T, X, Y, Z, W>)m_EventTable[id] + callBack;
        }

        //no parameters
        public static void RemoveListener(int id, EventCallBack callBack)
        {
            OnListenerRemoving(id, callBack);
            m_EventTable[id] = (EventCallBack)m_EventTable[id] - callBack;
            OnListenerRemoved(id);
        }
        //single parameters
        public static void RemoveListener<T>(int id, EventCallBack<T> callBack)
        {
            OnListenerRemoving(id, callBack);
            m_EventTable[id] = (EventCallBack<T>)m_EventTable[id] - callBack;
            OnListenerRemoved(id);
        }
        //two parameters
        public static void RemoveListener<T, X>(int id, EventCallBack<T, X> callBack)
        {
            OnListenerRemoving(id, callBack);
            m_EventTable[id] = (EventCallBack<T, X>)m_EventTable[id] - callBack;
            OnListenerRemoved(id);
        }
        //three parameters
        public static void RemoveListener<T, X, Y>(int id, EventCallBack<T, X, Y> callBack)
        {
            OnListenerRemoving(id, callBack);
            m_EventTable[id] = (EventCallBack<T, X, Y>)m_EventTable[id] - callBack;
            OnListenerRemoved(id);
        }
        //four parameters
        public static void RemoveListener<T, X, Y, Z>(int id, EventCallBack<T, X, Y, Z> callBack)
        {
            OnListenerRemoving(id, callBack);
            m_EventTable[id] = (EventCallBack<T, X, Y, Z>)m_EventTable[id] - callBack;
            OnListenerRemoved(id);
        }
        //five parameters
        public static void RemoveListener<T, X, Y, Z, W>(int id, EventCallBack<T, X, Y, Z, W> callBack)
        {
            OnListenerRemoving(id, callBack);
            m_EventTable[id] = (EventCallBack<T, X, Y, Z, W>)m_EventTable[id] - callBack;
            OnListenerRemoved(id);
        }


        //no parameters
        public static void Broadcast(int id)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(id, out d))
            {
                EventCallBack callBack = d as EventCallBack;
                if (callBack != null)
                {
                    callBack();
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", id));
                }
            }
        }
        //single parameters
        public static void Broadcast<T>(int id, T arg)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(id, out d))
            {
                EventCallBack<T> callBack = d as EventCallBack<T>;
                if (callBack != null)
                {
                    callBack(arg);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", id));
                }
            }
        }
        //two parameters
        public static void Broadcast<T, X>(int id, T arg1, X arg2)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(id, out d))
            {
                EventCallBack<T, X> callBack = d as EventCallBack<T, X>;
                if (callBack != null)
                {
                    callBack(arg1, arg2);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", id));
                }
            }
        }
        //three parameters
        public static void Broadcast<T, X, Y>(int id, T arg1, X arg2, Y arg3)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(id, out d))
            {
                EventCallBack<T, X, Y> callBack = d as EventCallBack<T, X, Y>;
                if (callBack != null)
                {
                    callBack(arg1, arg2, arg3);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", id));
                }
            }
        }
        //four parameters
        public static void Broadcast<T, X, Y, Z>(int id, T arg1, X arg2, Y arg3, Z arg4)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(id, out d))
            {
                EventCallBack<T, X, Y, Z> callBack = d as EventCallBack<T, X, Y, Z>;
                if (callBack != null)
                {
                    callBack(arg1, arg2, arg3, arg4);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", id));
                }
            }
        }
        //five parameters
        public static void Broadcast<T, X, Y, Z, W>(int id, T arg1, X arg2, Y arg3, Z arg4, W arg5)
        {
            Delegate d;
            if (m_EventTable.TryGetValue(id, out d))
            {
                EventCallBack<T, X, Y, Z, W> callBack = d as EventCallBack<T, X, Y, Z, W>;
                if (callBack != null)
                {
                    callBack(arg1, arg2, arg3, arg4, arg5);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", id));
                }
            }
        }
    }
}