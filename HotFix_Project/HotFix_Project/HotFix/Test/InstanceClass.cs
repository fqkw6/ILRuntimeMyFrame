using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using tnt_deploy;
using UnityEngine;
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

            GOODS_INFO_ARRAY goods_infos = ReadOneDataConfig<GOODS_INFO_ARRAY>("goods_info");
            Debug.Log("goods_id==================" + goods_infos.items[0].goods_id);
        }
        public static T ReadOneDataConfig<T>(string FileName)
        {
            FileStream fileStream;
            fileStream = Test. GetDataFileStream(FileName);
            if (null != fileStream)
            {
                Debug.LogError("0000===" + typeof(T));
                Type type = typeof(T);
                T t = Serializer.Deserialize<T>(fileStream);
                fileStream.Close();
                return t;
            }

            return default(T);
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
    }


}
