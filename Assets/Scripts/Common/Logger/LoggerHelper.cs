﻿using UnityEngine;
using System.Collections.Generic;
using XLua;
using System;
using Message;
using Google.Protobuf;

[Hotfix]
public class LoggerHelper : MonoSingleton<LoggerHelper>
{
    public enum LOG_TYPE
    {
        LOG = 0,
        LOG_ERR,
    }

    struct log_info
    {
        public LOG_TYPE type;
        public string msg;

        public log_info(LOG_TYPE type, string msg)
        {
            this.type = type;
            this.msg = msg;
        }
    }

    private static LoggerHelper _instance = null;
    private List<log_info> backList = new List<log_info>(100);
    private List<log_info> frontList = new List<log_info>(100);

    protected override void Init()
    {
        if (!Application.isEditor)
        {
            Application.logMessageReceived += (LogHandler);

            //关闭error上报定时器
            //InvokeRepeating("CheckReport", 1f, 1f);
        }
    }

    private void LogHandler(string condition, string stackTrace, LogType type)
    {
        if (Application.isEditor)
        {
            return;
        }

        if (type == LogType.Error || type == LogType.Exception || type == LogType.Assert)
        {
            Logger.LogError(condition + " \n" + stackTrace);
        }
    }

    private void CheckReport()
    {
        Logger.CheckReportError();
    }
    
    private void Update()
    {
        lock (backList)
        {
            if (backList.Count > 0)
            {
                List<log_info> tmp = frontList;
                frontList = backList;
                backList = tmp;
            }
        }

        if (frontList.Count > 0)
        {
            for (int i = 0; i < frontList.Count; i++)
            {
                var logInfo = frontList[i];
                switch (logInfo.type)
                {
                    case LOG_TYPE.LOG:
                        {
                            Logger.Log(logInfo.msg, null);
                            break;
                        }
                    case LOG_TYPE.LOG_ERR:
                        {
                            Logger.LogError(logInfo.msg, null);
                            break;
                        }
                }
            }
            frontList.Clear();
        }

        if (Input.GetMouseButtonDown(1))
        {
            HotFixMangager.instance.GetAppDomain().Invoke("HotFix_Project.HotManager", "StaticFunTest", null, null);
            Debug.LogError("ceshiA");
            Role.role_info role_Info1 = new Role.role_info();
            role_Info1.Level = 15;
            role_Info1.Name = "woshi";
            role_Info1.RoomId = 100;
            role_Info1.Score = 90;

            byte[] msg = ProtoBufferTool.Serialize(role_Info1);
            NetMessageBase netMessageBase = new NetMessageBase();
            netMessageBase.MessageId = 200;
            netMessageBase.MessageBoby = ByteString.CopyFrom(msg);
            byte[] by = ProtoBufferTool.Serialize(netMessageBase);

            NetMessageBase newooo = ProtoBufferTool.Deserialize(NetMessageBase.Parser, by) as NetMessageBase;
            Debug.LogError(newooo.MessageId + "==newooo.MessageId==");
            Role.role_info info1 = ProtoBufferTool.Deserialize(Role.role_info.Parser, newooo.MessageBoby.ToByteArray()) as Role.role_info;
            Debug.LogError(info1.Name + "==info1.Name ==");


        }
        if (Input.GetMouseButtonDown(0))
        {
            HotFixMangager.instance.GetAppDomain().Invoke("HotFix_Project.HotManager", "StaticFunTestClose", null, null);

            Debug.LogError("ceshiB");

        }
    }
    public override void Dispose()
    {
        lock (backList)
        {
            backList.Clear();
        }
        frontList.Clear();
        base.Dispose();
    }

    public void LogToMainThread(LOG_TYPE type, string msg)
    {
        lock (backList)
        {
            backList.Add(new log_info(type, msg));
        }
    }
}

#if UNITY_EDITOR
public static class LoggerHelperExporter
{
    [LuaCallCSharp]
    public static List<Type> LuaCallCSharp = new List<Type>()
        {
            typeof(LoggerHelper),
        };
}
#endif
