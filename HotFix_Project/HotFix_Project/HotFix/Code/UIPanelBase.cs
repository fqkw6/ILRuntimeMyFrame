using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 面板基类
/// </summary>
public abstract class UIPanelBase : IViewBase
{
    /// <summary>
    /// <see cref="GetAssetAddress()"/>
    /// </summary>
    protected string m_AssetAddress;

    /// <summary>
    /// <see cref="GetPanelType()"/>
    /// </summary>
    private PanelType m_PanelType = PanelType.None;

    /// <summary>
    /// <see cref="GetGameObject()"/>
    /// </summary>
    private GameObject gameObject;

    /// <summary>
    /// <see cref="GetTransform()"/>
    /// </summary>
    private Transform transform;

    private PanelName m_PanelName ;

    /// <summary>
    /// 关闭时
    /// </summary>
    public UnityAction OnClosed;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="panelName">面板名称（枚举）</param>
    /// <param name="assetAddress">面板资源地址</param>
    public  UIPanelBase(PanelName panelName, string assetAddress, PanelType panelType) 
    {
        m_AssetAddress = assetAddress;
        m_PanelType = panelType;
        m_PanelName = panelName;
    }

    /// <summary>
    /// 是否为常驻窗口
    /// </summary>
    public bool IsPermanent;

    /// <summary>
    /// 获取面板资源地址
    /// </summary>
    public string GetAssetAddress()
    {
        return m_AssetAddress;
    }

    /// <summary>
    /// 获取面板类型
    /// </summary>
    public PanelType GetPanelType()
    {
        return m_PanelType;
    }
    /// <summary>
    /// 获取面板名字
    /// </summary>
    /// <returns></returns>
    public PanelName GetPanelName()
    {
        return m_PanelName;
    }
    /// <summary>
    /// 获取面板GameObject
    /// </summary>
    public GameObject GetGameObject()
    {
        Debug.LogError("ceshi======");
        return gameObject;
    }

    /// <summary>
    /// 设置面板GameObject，同时设置Transform
    /// </summary>
    /// <param name="go">GameObject</param>
    public void SetGameObjectAndTransform(GameObject go)
    {
        gameObject = go;
        transform = gameObject ? gameObject.transform : null;
    }

    /// <summary>
    /// 获取面板Transform
    /// </summary>
    public Transform GetTransform()
    {
        return transform;
    }

    /// <summary>
    /// 查找面板内组件
    /// </summary>
    /// <param name="path">面板内相对路径</param>
    /// <returns>对应组件</returns>
    protected T FindComponent<T>(string path) where T : Component
    {
        Transform result = transform.Find(path);
        if (result == null)
        {
            return null;
        }
        return result.GetComponent<T>();
    }

    /// <summary>
    /// 查找指定节点下的组件
    /// </summary>
    /// <param name="node">节点</param>
    /// <param name="path">相对节点的相对路径</param>
    /// <returns>对应组件</returns>
    protected T FindComponent<T>(Transform node, string path)
    {
        Transform result = node.Find(path);
        if (result)
        {
            return result.GetComponent<T>();
        }
        return default(T);
    }

   

   


    #region 协程处理

    /// <summary>
    /// 开始更新
    /// </summary>
    protected void StartUpdate()
    {
        StopUpdate();

      //  UIManager.Instance.OnUpdate += Update;
    }

    /// <summary>
    /// 停止更新
    /// </summary>
    protected void StopUpdate()
    {
      //  UIManager.Instance.OnUpdate -= Update;
    }


    public virtual void Awake()
    {
        SetGameObjectAndTransform(gameObject);
    }

    public virtual void OnEnable()
    {
        gameObject.SetActive(true);
    }

    public virtual void Start()
    {
        
    }

    public virtual void OnDisable()
    {
        StopUpdate();

        gameObject.SetActive(false);
    }


    public void SetGameObject(GameObject go)
    {
        SetGameObjectAndTransform(go);
    }

    public virtual void OnDestroy()
    {
        
    }

    public virtual void Update()
    {
       
    }

    #endregion


}