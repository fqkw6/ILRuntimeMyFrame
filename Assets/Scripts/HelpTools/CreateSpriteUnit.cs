//导出脚本的模版
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
public class CreateSpriteUnit
{
    public string classname;
    public string template = @"
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class @ClassName 
{   
@fields

    public void OnAwake(GameObject viewGO)
    {
@body1
    }

    public void OnDestroy()
    {
@body2
    }
}
";
    //缓存的所有子对象信息
    public List<UIInfo> evenlist = new List<UIInfo>();
    /// <summary>
    /// 把拼接好的脚本写到本地。
    /// （自己可以个窗口支持改名和选择路径，真实工程里是带这些功能的）
    /// </summary>
    public void WtiteClass()
    {
        bool flag = true;
        bool throwOnInvalidBytes = false;
        UTF8Encoding encoding = new UTF8Encoding(flag, throwOnInvalidBytes);
        bool append = false;
        StreamWriter writer = new StreamWriter(Application.dataPath + "/" + classname + ".cs", append, encoding);
        writer.Write(GetClasss());
        writer.Close();
        AssetDatabase.Refresh();
    }
    //脚本拼接
    public string GetClasss()
    {
        var fields = new StringBuilder();
        var body1 = new StringBuilder();
        var body2 = new StringBuilder();
        for (int i = 0; i < evenlist.Count; i++)
        {
            fields.AppendLine("\t" + evenlist[i].field);
            body1.AppendLine("\t\t" + evenlist[i].body1);
            body2.AppendLine("\t\t" + evenlist[i].body2);
        }
        template = template.Replace("@ClassName", classname).Trim();
        template = template.Replace("@body1", body1.ToString()).Trim();
        template = template.Replace("@body2", body2.ToString()).Trim();
        template = template.Replace("@fields", fields.ToString()).Trim();
        return template;
    }
}
//子对象信息
public class UIInfo
{
    public string field;
    public string body1;
    public string body2;
    public UIInfo(string name, string typeKey, string path)
    {
        field = string.Format("public {0} {1};", CreateSprite.typeMap[typeKey], name);
        if (typeKey == "go")
        {
            body1 = string.Format("{0} = viewGO.transform.Find(\"{1}\").gameObject;", name, path, CreateSprite.typeMap[typeKey]);
        }
        else
        {
            body1 = string.Format("{0} = viewGO.transform.Find(\"{1}\").GetComponent<{2}>();", name, path, CreateSprite.typeMap[typeKey]);
        }
        body2 = string.Format("{0} = null;", name);
    }
}
#endif