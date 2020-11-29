/*
 * Description:             ���ñ���ظ���������
 * Author:                  tanghuan
 * Create Date:             2018/09/05
 */

using AssetBundles;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// ���ñ���ظ���������
/// </summary>
public class ConfLoader : SingletonTemplate<ConfLoader> {

    /// <summary>
    /// Excel������ݴ洢Ŀ¼
    /// </summary>
    public const string ExcelDataFolderPath = "Assets/AssetsPackage/DataBytes/";

    public ConfLoader()
    {

    }

    /// <summary>
    /// ��ȡ����������ݵĶ�����������
    /// </summary>
    /// <param name="bytefilename"></param>
    /// <returns></returns>
    public Stream getStreamByteName(string bytefilename)
    {
#if UNITY_EDITOR
        //var textasset = Resources.Load(ExcelDataFolderPath + bytefilename) as TextAsset;
        MemoryStream memorystream =null;
        if (AssetBundleConfig.IsEditorMode)
        {
            TextAsset textasset = AssetDatabase.LoadAssetAtPath<TextAsset>(ExcelDataFolderPath + bytefilename + ".bytes");
            memorystream = new MemoryStream(textasset.bytes);
        }
        else
        {
            byte[] data = Load(bytefilename);
            memorystream = new MemoryStream(data);

        }


        return memorystream;
#else
        MemoryStream memorystream = null;
        byte[] data = Load(bytefilename);
        memorystream = new MemoryStream(data);
        return memorystream;
#endif
    }
    public byte[] Load(string filepath)
    {
        string dataPath = string.Format("{0}/{1}.bytes", GameLaunch.DataPath, filepath);
        string assetbundleName = null;
        string assetName = null;
        bool status = AssetBundleManager.Instance.MapAssetPath(dataPath, out assetbundleName, out assetName);
        if (!status)
        {
            Logger.LogError("MapAssetPath failed : " + dataPath);
            return null;
        }
        var asset = AssetBundleManager.Instance.GetAssetCache(assetName) as TextAsset;
        if (asset != null)
        {
            //Logger.Log("Load lua script : " + dataPath);
            return asset.bytes;
        }

        Logger.LogError("Load data failed : " + dataPath + ", You should preload data assetbundle first!!!");
        return null;
    }
}
