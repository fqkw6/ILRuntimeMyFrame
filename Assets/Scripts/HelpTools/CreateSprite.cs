using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
public class CreateSprite
{
    //当前操作的对象
    private static GameObject CurGo;
    //后缀对应的组件类型
    public static Dictionary<string, string> typeMap = new Dictionary<string, string>()
    {
        { "im", typeof(Image).Name },
        { "txt", typeof(Text).Name },
        { "btn", typeof(Button).Name },
        { "go", typeof(GameObject).Name},
        { "tr", typeof(Transform).Name},
    };
    //脚本模版
    private static CreateSpriteUnit info;

    //在Project窗口下，选中要导出的界面，然后点击GameObject/导出脚本
    [MenuItem("Assets/Create/UISctipt", false, 70)]
    public static void CreateSpriteAction()
    {
        GameObject[] gameObjects = Selection.gameObjects;
        //保证只有一个对象
        if (gameObjects.Length == 1)
        {
            info = new CreateSpriteUnit();
            CurGo = gameObjects[0];
            ReadChild(CurGo.transform);
            info.classname = CurGo.name + "UIPanel";
            info.WtiteClass();
            info = null;
            CurGo = null;
        }
        else
        {
            EditorUtility.DisplayDialog("警告", "你只能选择一个GameObject", "确定");
        }
    }

    //遍历所有子对象，GetChild方法只能获取第一层子对象。
    public static void ReadChild(Transform tf)
    {
        foreach (Transform child in tf)
        {
            string[] typeArr = child.name.Split('_');
            if (typeArr.Length > 1)
            {
                string typeKey = typeArr[typeArr.Length - 1];
                if (typeMap.ContainsKey(typeKey))
                {
                    info.evenlist.Add(new UIInfo(child.name, typeKey, buildGameObjectPath(child).Replace(CurGo.name + "/", "")));
                }

            }
            if (child.childCount > 0)
            {
                ReadChild(child);
            }
        }
    }
    //获取路径，这个路径是带当前对象名的，需要用Replace替换掉头部
    private static string buildGameObjectPath(Transform obj)
    {
        var buffer = new StringBuilder();

        while (obj != null)
        {
            if (buffer.Length > 0)
                buffer.Insert(0, "/");
            buffer.Insert(0, obj.name);
            obj = obj.parent;
        }
        return buffer.ToString();
    }
}
#endif