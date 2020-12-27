using UnityEngine;
using UnityEngine.Sprites;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class BatchesSprite : MonoBehaviour
{
    public enum DrawMode : byte
    {
        Simple=1,//普通
        Cutting,// 左右裁切，一半 拼成一个设置为该模式
    }


    [SerializeField]
    private DrawMode m_DrawMode = DrawMode.Simple;
    

    public DrawMode drawMode
    {
        get
        {
            return m_DrawMode;
        }
        set
        {
            SetProperty(ref m_DrawMode,value);
        }
    }

    [SerializeField]
    private Sprite m_Sprite;

    public Sprite sprite
    {
        get
        {
            return m_Sprite;
        }
        set
        {
            SetProperty(ref m_Sprite, value);
        }
    }

    [SerializeField]
    private Color m_Color=Color.white;
    public Color color
    {
        get
        {
            return m_Color;
        }
        set
        {
            SetProperty(ref m_Color, value);
        }
    }

    [SerializeField]
    private bool m_FilpX;
    public bool filpX
    {
        get
        {
            return m_FilpX;
        }
        set
        {
            SetProperty(ref m_FilpX, value);
        }
    }

    [SerializeField]
    private bool m_FilpY;
    public bool filpY
    {
        get
        {
            return m_FilpY;
        }
        set
        {
            SetProperty(ref m_FilpY, value);
        }
    }

    [SerializeField]
    private int m_SortingOrder;
    public int sortingOrder
    {
        get
        {
            return m_SortingOrder;
        }
        set
        {
            SetProperty(ref m_SortingOrder, value);
        }
    }



    [SerializeField]
    private Material m_Material;
    public Material material
    {
        get
        {
#if  UNITY_EDITOR
            if (!Application.isPlaying)
            {
                if (m_Material != null)
                {
                    return new Material(m_Material);
                }
                else
                {
                    return BatchesUtil.GetStoredSharedMaterial(m_Material);
                }
            }
#endif
            return m_Material;
        }
        set
        {
            SetProperty(ref m_Material, value);
        }
    }

    Vector4 getTextSize;
    float[] verticesX;
    float[] verticesY;
    public Vector4 GetTextSize
    {
        get
        {
            if (getTextSize == null)
            {
                getTextSize = new Vector4();
            }
            if (s_vertices != null)
            {
                if (verticesX == null || verticesX.Length != s_vertices.Length ||
                    verticesY == null || verticesY.Length != s_vertices.Length)
                {
                    verticesX = new float[s_vertices.Length];
                    verticesY = new float[s_vertices.Length];
                }
                float minx = s_vertices[0].x;
                float maxx = s_vertices[0].x;
                float miny = s_vertices[0].y;
                float maxy = s_vertices[0].y;

                for (int i = 0; i < s_vertices.Length; i++)
                {
                    minx = minx < s_vertices[i].x ? minx : s_vertices[i].x;
                    maxx = maxx < s_vertices[i].x ? maxx : s_vertices[i].x;
                    miny = miny < s_vertices[i].y ? miny : s_vertices[i].y;
                    maxy = maxy < s_vertices[i].y ? maxy : s_vertices[i].y;
                }
                getTextSize.x = minx;
                getTextSize.y = miny;
                getTextSize.z = maxx-minx;
                getTextSize.w = maxy-miny;

                return getTextSize;
            }
            return getTextSize;
        }
    }

    private Transform m_BatchesTransform;
    private Transform batchesTransform
    {
        get
        {
            if (m_BatchesTransform == null)
            {
                m_BatchesTransform = transform;
            }
            return m_BatchesTransform;
        }
    }
    private Vector3 m_CutScale;
    public Vector3 cutScale
    {
        get
        {
            return m_CutScale;
        }
        set
        {
            if (m_DrawMode == DrawMode.Cutting)
            {
                if (m_CutScale != value) {
                    //enabled=true;
                    m_CutScale = value;
                    batchesTransform.localScale = value;
                    m_CutDirty = true;
                }
            }
        }
    }
    private Mesh m_Mesh;
    private bool m_MeshDirty;
    private bool m_CutDirty;
    private MeshFilter m_MeshFilter;
    private MeshRenderer m_MeshRenderer;
    private Vector4 m_OuterUV = Vector4.zero;
    private static int[] s_triangles = { 0, 1, 2, 0, 2, 3 };
    private static Vector3[] s_vertices = new Vector3[4];
    private static Vector2[] s_uv = new Vector2[4];
    private static Color[] s_colors = new Color[4];
    private static Vector2[] s_uv2 = { Vector2.up, Vector2.up, Vector2.up, Vector2.up,};
    private void Awake()
    {
#if UNITY_EDITOR
        Reset();
#endif
        m_MeshDirty = true;
        m_MeshFilter = GetComponent<MeshFilter>();
        m_MeshRenderer = GetComponent<MeshRenderer>();
#if UNITY_5_5_OR_NEWER
        m_MeshRenderer.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
#else
        m_MeshRenderer.motionVectors = false;
#endif
        m_MeshRenderer.receiveShadows = false;
        m_MeshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        m_MeshFilter.hideFlags = m_MeshRenderer.hideFlags = HideFlags.HideInInspector;

        if (m_DrawMode == DrawMode.Cutting)
        {
            m_CutScale = batchesTransform.localScale;
        }
    }
    private void Update()
    {
        if (m_MeshDirty)
        {
            m_MeshDirty = false;
            UpdateMesh();
        }

        if (m_CutDirty)
        {
            m_CutDirty = false;
            UpdateCutting();
        }
    }

    private void UpdateMesh()
    {
        if (m_Mesh == null)
        {
            m_Mesh = new Mesh();
        }
        else
        {
            m_Mesh.Clear();
        }

        var material = this.material;
        if (material == null)
            return;

        var sprite = this.sprite;
        if (sprite == null)
            return;

        Vector2 pivot = sprite.pivot;
        Vector2 size = sprite.rect.size;
        Vector2 filp = new Vector2(m_FilpX ? -1 : 1, m_FilpY ? -1 : 1);
        var r = DataUtility.GetPadding(sprite);
        r.z = (size.x - r.z - pivot.x) * filp.x;
        r.w = (size.y - r.w - pivot.y) * filp.y;
        r.x = (r.x - pivot.x) * filp.x;
        r.y = (r.y - pivot.y) * filp.y;
        r /= sprite.pixelsPerUnit;

        Vector4 uv = DataUtility.GetOuterUV(sprite);

        if (m_DrawMode == DrawMode.Cutting)
        {
            m_OuterUV = uv;
            uv.z = uv.x + ((uv.z - uv.x) * Mathf.Min(batchesTransform.localScale.x, 1f));
            uv.w = uv.y + ((uv.w - uv.y) * Mathf.Min(batchesTransform.localScale.y, 1f));
        }

        //
        //
        //
        //
        //

        s_vertices[0] = new Vector3(r.x, r.y);
        s_vertices[1] = new Vector3(r.x, r.w);
        s_vertices[2] = new Vector3(r.z, r.w);
        s_vertices[3] = new Vector3(r.z, r.y);
        m_Mesh.vertices = s_vertices;

        s_uv[0] = new Vector2(uv.x, uv.y);
        s_uv[1] = new Vector2(uv.x, uv.w);
        s_uv[2] = new Vector2(uv.z, uv.w);
        s_uv[3] = new Vector2(uv.z, uv.y);

        m_Mesh.uv = s_uv;
        m_Mesh.uv2 = s_uv2;
        s_colors[0] = s_colors[1] = s_colors[2] = s_colors[3] = m_Color;
        m_Mesh.colors = s_colors;
        m_Mesh.triangles = s_triangles;


        m_MeshFilter.mesh=m_Mesh;
        material.mainTexture = sprite.texture;
        m_MeshRenderer.sharedMaterial = material;
        m_MeshRenderer.sortingOrder = m_SortingOrder;
        //
    }

    private void UpdateCutting()
    {
        if (m_DrawMode == DrawMode.Cutting)
        {
            Vector4 uv = m_OuterUV;
            uv.z = uv.x + ((uv.z - uv.x) * Mathf.Min(batchesTransform.localScale.x, 1f));
            uv.w = uv.y + ((uv.w - uv.y) * Mathf.Min(batchesTransform.localScale.y, 1f));
            var mesh = m_MeshFilter.sharedMesh;
            if (mesh == null) return;

            s_uv[0] = new Vector2(uv.x, uv.y);
            s_uv[1] = new Vector2(uv.x, uv.w);
            s_uv[2] = new Vector2(uv.z, uv.w);
            s_uv[3] = new Vector2(uv.z, uv.y);

            //
            mesh.uv = s_uv;
        }
    }

    private bool SetProperty<T>(ref T property, T value)
    {
        if (!Equals(property, value))
        {
            //
            property = value;
            m_MeshDirty = true;
            return true;
        }
        return false;
    }


#if UNITY_EDITOR

    private void Reset()
    {
        if (m_Material == null)
        {
            material = BatchesUtil.GetDefaultMaterial();
        }
    }
    private void OnValidate()
    {
        m_MeshDirty = true;
    }

    [CanEditMultipleObjects, CustomEditor(typeof(BatchesSprite), true)]
    class BatchesSpriteRendererEditor:Editor
    {
        private SerializedProperty m_Sprite;
        private SerializedProperty m_Color;
        private SerializedProperty m_FilpX;
        private SerializedProperty m_FilpY;
        private SerializedProperty m_Material;
        private SerializedProperty m_DrawMode;
        private SerializedProperty m_SortingOrder;
        private SerializedObject m_RendererObject;

        private void OnEnable()
        {

            m_Sprite = serializedObject.FindProperty("m_Sprite");
            m_Color = serializedObject.FindProperty("m_Color");
            m_FilpX = serializedObject.FindProperty("m_FilpX");
            m_FilpY = serializedObject.FindProperty("m_FilpY");
            m_Material = serializedObject.FindProperty("m_Material");
            m_DrawMode = serializedObject.FindProperty("m_DrawMode");
            m_SortingOrder = serializedObject.FindProperty("m_SortingOrder");
            var renderer = ((BatchesSprite)target).GetComponent<MeshRenderer>();
            m_RendererObject = new SerializedObject(renderer);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();


            EditorGUILayout.PropertyField(m_Sprite,new GUILayoutOption[0]);

            EditorGUILayout.PropertyField(m_Color, new GUILayoutOption[0]);

            FilpToggles();

            EditorGUILayout.PropertyField(m_Material, new GUILayoutOption[0]);
            EditorGUILayout.PropertyField(m_DrawMode, new GUILayoutOption[0]);
            EditorGUILayout.PropertyField(m_SortingOrder, new GUILayoutOption[0]);

            EditorGUILayout.Space();
            m_RendererObject.Update();
            m_RendererObject.ApplyModifiedProperties();
            serializedObject.ApplyModifiedProperties();
        }

        private void FilpToggle(Rect r,string label, SerializedProperty property)
        {
            bool boolValue = property.boolValue;
            EditorGUI.showMixedValue = property.hasMultipleDifferentValues;
            EditorGUI.BeginChangeCheck();
            int indentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            boolValue = EditorGUI.ToggleLeft(r,label,boolValue);
            EditorGUI.indentLevel = indentLevel;
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObjects(targets,"Edit Constraints");
                property.boolValue = boolValue;
            }
            EditorGUI.showMixedValue = false;
        }

        private void FilpToggles()
        {
            GUILayout.BeginHorizontal(new GUILayoutOption[0]);
            Rect position = GUILayoutUtility.GetRect(EditorGUIUtility.fieldWidth,18f,16f,16f,EditorStyles.numberField);
            int id = GUIUtility.GetControlID(0x211c,FocusType.Keyboard,position);
            position = EditorGUI.PrefixLabel(position,id,new GUIContent("Flip"));
            position.width = 30f;
            FilpToggle(position,"X",m_FilpX);
            position.x += 30f;
            FilpToggle(position, "Y", m_FilpY);
            GUILayout.EndHorizontal();
        }

    }
#endif
}
