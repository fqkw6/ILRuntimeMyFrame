using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using Data;
namespace HotFix_Project
{
    class HotManager
    {
        public static void HotInitialize()
        {
            ILUIManager.Instance.Initialize();

            // onPrintAuthorInfoExcelData();
        }
        public static void StaticFunTest()
        {
            UnityEngine.Debug.Log("!!! InstanceClass.Stat55555555icFunTest()");

            //ILUIManager.LoadUI<LoginPanel>("UI/Prefabs/View/UILaunch.prefab", "LoginPanel", null,null);
            ILUIManager.Instance.OpenPanel<LoginPanel>(PanelName.LoginPanel);
            TestPB.Start();
           // onLoadAllExcelData();
        }

        public static void StaticFunTestClose()
        {
            UnityEngine.Debug.Log("!!! InstanceClass.Stat55555555icFunTest()");
            LoginPanel loginPanel = ILUIManager.Instance.GetPanel<LoginPanel>(PanelName.LoginPanel);
            Debug.LogError(loginPanel.GetGameObject()+"-===-=-=");
            ILUIManager.Instance.ClosePanel<LoginPanel>(PanelName.LoginPanel);
         
            // onLoadAllExcelData();
        }
        /// <summary>
        /// 加载所有Excel数据
        /// </summary>
        public static void onLoadAllExcelData()
        {
            Debug.Log("onLoadAllExcelData()");
            TimeCounter.Singleton.Restart("LoadAllExcelData()");
            MonoMemoryProfiler.Singleton.beginMemorySample("LoadAllExcelData()");
            GameDataManager.Instance.loadAll();
            MonoMemoryProfiler.Singleton.endMemorySample();
            TimeCounter.Singleton.End();
        }

        public static void onPrintAuthorInfoExcelData()
        {
            Debug.Log("onPrintAuthorInfoExcelData()");
            foreach (var authorinfo in GameDataManager.Instance.t_AuthorInfocontainer.getList())
            {
                Debug.Log(string.Format("authorinfo.id : {0}", authorinfo.id));
                Debug.Log(string.Format("authorinfo.author : {0}", authorinfo.author));
                Debug.Log(string.Format("authorinfo.age : {0}", authorinfo.age));
                Debug.Log(string.Format("authorinfo.hashouse : {0}", authorinfo.hashouse));
                Debug.Log(string.Format("authorinfo.money : {0}", authorinfo.money));
                Debug.Log(string.Format("authorinfo.pbutctime : {0}", authorinfo.pbutctime));
                foreach (var lucknumber in authorinfo.luckynumber)
                {
                    Debug.Log(string.Format("authorinfo.lucknumber : {0}", lucknumber));
                }
            }
        }
    }
}