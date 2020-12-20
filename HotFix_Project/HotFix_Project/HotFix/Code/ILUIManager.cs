using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 面板类型枚举，不同类型面板，会放置到不同的画板上
/// </summary>
public enum PanelType
{
    /// <summary>
    /// 无效面板
    /// </summary>
    None,
    /// <summary>
    /// HUD面板
    /// </summary>
    Hud,
    /// <summary>
    /// 普通面板
    /// </summary>
    Normal,
    /// <summary>
    /// 通知类面板
    /// </summary>
    Notice,
    /// <summary>
    /// 对话框面板
    /// </summary>
    Dialugue
}

/// <summary>
/// UI管理类
/// 面板打开、关闭
/// 后面功能陆续添加
/// </summary>
public class ILUIManager 
{

    private static ILUIManager instance;

    private ILUIManager()
    {

    }
    public static ILUIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance =new ILUIManager();
            }
            return instance;
        }
    }
    /// <summary>
    /// HUD面板层
    /// </summary>
    private UICanvas m_HUDCanvas;

    /// <summary>
    /// Normal面板层
    /// </summary>
    private UICanvas m_PanelCanvas;

    /// <summary>
    /// Notice面板层
    /// </summary>
    private UICanvas m_NoticeCanvas;

    /// <summary>
    /// UI关闭回收到的节点
    /// </summary>
    private Transform m_UICacheRoot;

    /// <summary>
    /// UI摄像机
    /// </summary>
    private Camera m_UICamera;

    /// <summary>
    /// 面板列表
    /// </summary>
    private UIPanelBase[] m_Panels = new UIPanelBase[(int)PanelName.Total];

    /// <summary>
    /// 面板对应的Prefab
    /// </summary>
    /// <typeparam name="string">资源地址</typeparam>
    /// <typeparam name="GameObject">Prefab实例</typeparam>
    private Dictionary<string, GameObject> m_Prefabs = new Dictionary<string, GameObject>();

    /// <summary>
    /// HUD面板栈
    /// </summary>
    private List<UIPanelBase> m_HudPanels = new List<UIPanelBase>();

    /// <summary>
    /// Normal面板栈
    /// </summary>
    private List<UIPanelBase> m_NormalPanels = new List<UIPanelBase>();

    /// <summary>
    /// Notice面板栈
    /// </summary>
    private List<UIPanelBase> m_NoticePanels = new List<UIPanelBase>();

    /// <summary>
    /// UI管理器初始化
    /// 创建 UICamera 摄像机
    /// 创建 HUDCanvas 节点
    /// 创建 PanelCanvas 节点
    /// 创建 NoticeCanvas 节点
    /// 创建 UICacheRoot 节点
    /// </summary>
    public void Initialize()
    {
        CreateUICamera();

        m_HUDCanvas = CreateCanvas("HUDCanvas", 0);
        m_PanelCanvas = CreateCanvas("PanelCanvas", 1);
        m_NoticeCanvas = CreateCanvas("NoticeCanvas", 2);

        GameObject uiCacheRoot = new GameObject("UICacheRoot");
        m_UICacheRoot = uiCacheRoot.transform;
       // m_UICacheRoot.SetParent(transform);

    }

    /// <summary>
    /// 创建 UICamera 摄像机
    /// </summary>
    private void CreateUICamera()
    {
        GameObject camera = new GameObject("UICamera");
        m_UICamera = camera.AddComponent<Camera>();
       // camera.transform.SetParent(transform);

        camera.layer = LayerMask.NameToLayer("UI");
        m_UICamera.clearFlags = CameraClearFlags.Depth;
        m_UICamera.orthographic = false;
        m_UICamera.cullingMask = 1 << camera.layer;
    }

    /// <summary>
    /// 创建Canvas
    /// </summary>
    /// <param name="name">Canvas名称</param>
    /// <param name="order">Canvas排序层级</param>
    /// <returns>Canvas</returns>
    private UICanvas CreateCanvas(string name, int order)
    {
        GameObject go = new GameObject(name);
        UICanvas canvas = go.AddComponent<UICanvas>();
        // go.transform.SetParent(transform);
        GameObject.DontDestroyOnLoad(go);
        canvas.SetCanvas(RenderMode.ScreenSpaceCamera, m_UICamera, order);
        canvas.SetCanvasScaler(CanvasScaler.ScaleMode.ScaleWithScreenSize, CanvasScaler.ScreenMatchMode.MatchWidthOrHeight, 0);
        canvas.SetLayer("UI");

        return canvas;
    }

   
    /// <summary>
    /// 打开面板
    /// </summary>
    /// <param name="panelName">面板名称（枚举）</param>
    /// <param name="msg">传递消息</param>
    public void OpenPanel<T>(PanelName panelName, Action<UIPanelBase> callBack = null) where T : UIPanelBase,IViewBase
    {
        int panelIndex = (int)panelName;
        T panel = m_Panels[panelIndex] as T;
        if (panel == null)
        {
            Type type = Type.GetType(panelName.ToString());
            panel =  Activator.CreateInstance(type) as T;
          
        }

        UICanvas canvas = GetCanvasFromPanelType(panel.GetPanelType());
        List<UIPanelBase> list = GetListFromPanelType(panel.GetPanelType());
      
        if (!list.Contains(panel))
        {
         UIMangager.Instance.OpenUI<T>(panelIndex, panel.GetAssetAddress(), panelName.ToString(), canvas.transform, callBack);
        }
        else
        {
            Debug.LogError("已经加载过该面板="+ panelName);
        }
        m_Panels[panelIndex] = panel;
    }

    public T GetPanel<T>(PanelName panelName) where T : UIPanelBase, IViewBase
    {
        return UIMangager.Instance.GetUIPanel<T>((int)panelName);
    }

    /// <summary>
    /// 通过面板类型查找对应的Canvas层根结点
    /// </summary>
    /// <param name="panelType">面板类型</param>
    /// <returns>对应Canvas</returns>
    private UICanvas GetCanvasFromPanelType(PanelType panelType)
    {
        switch (panelType)
        {
            case PanelType.Hud:
                return m_HUDCanvas;
            case PanelType.Normal:
                return m_PanelCanvas;
            case PanelType.Notice:
                return m_NoticeCanvas;
            default:
                Debug.LogErrorFormat("GetCanvasFromPanelType => {0}!", panelType);
                return null;
        }
    }

    /// <summary>
    /// 通过面板类型查找对应的List
    /// </summary>
    /// <param name="panelType">面板类型</param>
    /// <returns>对应List</returns>
    private List<UIPanelBase> GetListFromPanelType(PanelType panelType)
    {
        switch (panelType)
        {
            case PanelType.Hud:
                return m_HudPanels;
            case PanelType.Normal:
                return m_NormalPanels;
            case PanelType.Notice:
                return m_NoticePanels;
            default:
                Debug.LogErrorFormat("GetListFromPanelType => {0}!", panelType);
                return null;
        }
    }

    public void ClosePanel<T>(PanelName panelName) where T : UIPanelBase, IViewBase
    {
        int panelIndex = (int)panelName;
        T panel = m_Panels[panelIndex] as T;
      
        List<UIPanelBase> list = GetListFromPanelType(panel.GetPanelType());
        int count = list.Count;
        UIMangager.Instance.CloseUI(panelIndex);
        for (int i = 0; i < count; i++)
        {
            if (list[i].GetPanelName() == panelName)
            {
                list.Remove(list[i]);
              
            }
        }
    }

    /// <summary>
    /// 关闭面板
    /// </summary>
    /// <param name="panelType">面板类型</param>
    /// <param name="msg">传递消息</param>
    public void CloseAllPanel(PanelType panelType, object msg = null)
    {
        List<UIPanelBase> List = GetListFromPanelType(panelType);
        int count = List.Count - 1;
        for (int i = count; i >=0; i--)
        {
            List.RemoveAt(i);
        }
    }

   
    /// <summary>
    /// 关闭所有HUD面板
    /// </summary>
    public void CloseAllHUDPanel()
    {
        CloseAllPanel(PanelType.Hud);
    }

    /// <summary>
    /// 关闭所有Normal面板
    /// </summary>
    public void CloseAllNormalPanel()
    {
        CloseAllPanel(PanelType.Normal);
    }

    /// <summary>
    /// 关闭所有Notice面板
    /// </summary>
    public void CloseAllNoticePanel()
    {
        CloseAllPanel(PanelType.Notice);
    }

    /// <summary>
    /// 关闭所有面板
    /// </summary>
    public void CloseAllPanel()
    {
        CloseAllNoticePanel();
        CloseAllNormalPanel();
        CloseAllHUDPanel();
    }
   /// <summary>
   /// 加载面板调用主工程
   /// </summary>
   /// <typeparam name="T"></typeparam>
   /// <param name="realPath"></param>
   /// <param name="parent"></param>
   /// <param name="callBack"></param>
    public static void LoadUI<T>(string realPath,  Transform parent, Action<T> callBack = null) where T : IViewBase
    {
        string className = typeof(T).ToString();
        LoadUI(realPath, className, parent, callBack);
    }
    /// <summary>
    /// 加载面板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="realPath"></param>
    /// <param name="className"></param>
    /// <param name="parent"></param>
    /// <param name="callBack"></param>
    public static void LoadUI<T>(string realPath, string className, Transform parent, Action<T> callBack = null) where T : IViewBase
    {
        UIMangager.Instance.LoadUI<T>(realPath,className,parent,callBack);
    }
}