/*
 * Description:             ���ñ���ظ���������
 * Author:                  tanghuan
 * Create Date:             2018/09/05
 */

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
        //var textasset = Resources.Load(ExcelDataFolderPath + bytefilename) as TextAsset;
        TextAsset textasset = AssetDatabase.LoadAssetAtPath<TextAsset>(ExcelDataFolderPath+ bytefilename+ ".bytes");
        var memorystream = new MemoryStream(textasset.bytes);
        return memorystream;
    }
}
