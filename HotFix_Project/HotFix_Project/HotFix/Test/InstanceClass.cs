using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Data;
namespace HotFix_Project
{
    public class InstanceClass
    {
        private int id;

        public InstanceClass()
        {
            UnityEngine.Debug.Log("!!! InstanceClass::InstanceClass()");
            this.id = 0;
        }

        public InstanceClass(int id)
        {
            UnityEngine.Debug.Log("!!! InstanceClass::InstanceClass() id = " + id);
            this.id = id;
        }

        public int ID
        {
            get { return id; }
        }

        // static method
        public static void StaticFunTest()
        {
            UnityEngine.Debug.Log("!!! InstanceClass.Stat55555555icFunTest()");

            UIMangager.OpenPanel<LoginPanel>("UI/Prefabs/View/UILaunch.prefab", "LoginPanel", null,null);
            TestPB.Start();
            onLoadAllExcelData();
           // onPrintAuthorInfoExcelData();
        }
        public static void StaticFunTest2(int a)
        {
            UnityEngine.Debug.Log("!!! InstanceClass.StaticFunTest2(), a=" + a);
        }

        public static void GenericMethod<T>(T a)
        {
            UnityEngine.Debug.Log("!!! InstanceClass.GenericMethod(), a=" + a);
        }

        public void RefOutMethod(int addition, out List<int> lst, ref int val)
        {
            val = val + addition + id;
            lst = new List<int>();
            lst.Add(id);
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
