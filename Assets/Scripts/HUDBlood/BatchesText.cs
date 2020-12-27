using System.Collections.Generic;
using UnityEngine;using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class BatchesText : MonoBehaviour
{
    [TextArea(3, 10), SerializeField]
    private string m_Text = string.Empty;
    public string text
    {
        get
        {
            return m_Text;
        }
        set
        {
            SetProperty(ref m_Text,value??string.Empty);
        }
    }

    [SerializeField]
    private TextAnchor m_Anchor;
    public TextAnchor anchor
    {
        get
        {
            return m_Anchor;
        }
        set
        {
            SetProperty(ref m_Anchor, value);
        }
    }

    [SerializeField]
    private TextAlignment m_Alignment;
    public TextAlignment alignment
    {
        get
        {
            return m_Alignment;
        }
        set
        {
            SetProperty(ref m_Alignment, value);
        }
    }

    [SerializeField]
    private Font m_Font;
    public Font font
    {
        get
        {
            return m_Font;
        }
        set
        {
            if (m_Font == value)
                return;
            UntrackText(this);
            m_Font = value;
            TrackText(this);
            m_MeshDirty = true;
        }
    }

    [SerializeField]
    private int m_FontSize = 14;
    public int fontSize
    {
        get
        {
            return m_FontSize;
        }
        set
        {
            SetProperty(ref m_FontSize, value);
        }
    }

    [SerializeField]
    private FontStyle m_FontStyle;
    public FontStyle fontStyle
    {
        get
        {
            return m_FontStyle;
        }
        set
        {
            SetProperty(ref m_FontStyle, value);
        }
    }

    [SerializeField]
    private float m_LineSpacing = 1.0f;
    public float lineSpacing
    {
        get
        {
            return m_LineSpacing;
        }
        set
        {
            SetProperty(ref m_LineSpacing, value);
        }
    }

    [SerializeField]
    private bool m_RichText;
    public bool richText
    {
        get
        {
            return m_RichText;
        }
        set
        {
            SetProperty(ref m_RichText, value);
        }
    }

    [SerializeField]
    private Color m_Color = Color.white;
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
    private int m_SortingOrder = 14;
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

    [SerializeField]
    private float m_PixelsPerUnit = 100;
    public float pixelsPerUnit
    {
        get
        {
            return m_PixelsPerUnit;
        }
        set
        {
            SetProperty(ref m_PixelsPerUnit, Mathf.Max(0.001f,value));
        }
    }

    [SerializeField]
    private bool m_ShadowOpen = true;
    public bool shadowShadowOpen
    {
        get
        {
            return m_ShadowOpen;
        }
        set
        {
            SetProperty(ref m_ShadowOpen, value);
        }
    }

    [SerializeField]
    private float m_ShdowOffX = 1;
    public float shdowOffX
    {
        get
        {
            return m_ShdowOffX;
        }
        set
        {
            SetProperty(ref m_ShdowOffX, value);
        }
    }

    [SerializeField]
    private float m_ShdowOffY = -1;
    public float shdowOffY
    {
        get
        {
            return m_ShdowOffY;
        }
        set
        {
            SetProperty(ref m_ShdowOffY, value);
        }
    }

    [SerializeField]
    private Color m_ShadowColor = Color.white;
    public Color shadowColor
    {
        get
        {
            return m_ShadowColor;
        }
        set
        {
            SetProperty(ref m_ShadowColor, value);
        }
    }
   
    [SerializeField]
    private Vector2 m_Size = Vector2.zero;

    private Mesh m_Mesh;
    private bool m_MeshDirty;
    private MeshFilter m_MeshFilter;
    private MeshRenderer m_MeshRenderer;
    private bool m_DisableFontTextureRebuiltCallback = false;

    static private TextGenerator m_CacherTextGenerator;
    static private TextGenerator cacherTextGenerator
    {
        get
        {
            return m_CacherTextGenerator ?? (m_CacherTextGenerator=new TextGenerator(0));
        }
    }

    Vector2 vecTextSize;
    //文字实际渲染的宽高：如果需要计算位置的话
    public Vector2 GetTextSize
    {
        get
        {
            if (cacherTextGenerator == null) return Vector2.zero;
            var textGenerator = cacherTextGenerator;
            textGenerator.Invalidate();
            textGenerator.Populate(m_Text,GetGenerationSetting(m_Size));
            vecTextSize.x = cacherTextGenerator.rectExtents.width / pixelsPerUnit;
            vecTextSize.y = cacherTextGenerator.rectExtents.height / pixelsPerUnit;
            return vecTextSize;
        }
    }

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
    }

    private void OnEnable()
    {
        m_MeshDirty = true;
        TrackText(this);
    }
    private void OnDisable()
    {
        UntrackText(this);
    }

    private void Update()
    {
        if (m_MeshDirty)
        {
            m_MeshDirty = false;
            UpdateMesh();
        }
    }

    private void FontTexttureChanged()
    {
        //
        if (!this)
        {
            //
            return;
        }

        if (m_DisableFontTextureRebuiltCallback)
            return;

        m_MeshDirty = true;
    }
    static Color32 ConstColor = new Color32(255,255,255,255);
    static List<UIVertex> verts = new List<UIVertex>();
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

        if (m_Font == null || string.IsNullOrEmpty(m_Text))
            return;

        m_DisableFontTextureRebuiltCallback = true;

        var textGenerator = cacherTextGenerator;
        textGenerator.Invalidate();
        textGenerator.Populate(m_Text,GetGenerationSetting(m_Size));

        var vertsl = textGenerator.verts;
        int orgCount = vertsl.Count;
        verts.AddRange(vertsl);
        if (m_ShadowOpen)
        {
            ApplyShadowZeroAlloc(verts,ConstColor,0,vertsl.Count,m_ShdowOffX,m_ShdowOffY);
        }

        var vertCount = verts.Count - 0;//原版2017.4.39  verts.Count -4

        Vector3[] vertices = new Vector3[vertCount];
        Vector2[] uv = new Vector2[vertCount];
        Vector2[] uv2 = new Vector2[vertCount];
        Color[] colors = new Color[vertCount];
        int[] triangles = new int[vertCount / 2 * 3];
        for (int i =0, j = 0; i < vertCount;++i)
        {
            var vert = verts[i];
            vertices[i] = vert.position / m_PixelsPerUnit;
            uv[i] = vert.uv0;
            uv2[i] = Vector2.right;
            if (i < orgCount && m_ShadowOpen)
            {
                colors[i] = m_ShadowColor;
            }
            else
            {
                colors[i] = m_Color;
            }

            if ((i & 3) == 3)
            {
                triangles[j++] = i - 3;
                triangles[j++] = i - 2;
                triangles[j++] = i - 1;
                triangles[j++] = i - 3;
                triangles[j++] = i - 1;
                triangles[j++] = i - 0;
            }
        }

        m_Mesh.vertices = vertices;
        m_Mesh.uv = uv;
        m_Mesh.uv2 = uv2;
        m_Mesh.colors = colors;
        m_Mesh.triangles = triangles;
        m_MeshFilter.mesh = m_Mesh;
        material.SetTexture("_FontTex",m_Font.material.mainTexture);
        m_MeshRenderer.sharedMaterial = material;
        m_MeshRenderer.sortingOrder = m_SortingOrder;
        m_DisableFontTextureRebuiltCallback = false;
        verts.Clear();
    }

    protected void ApplyShadowZeroAlloc(List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
    {
        UIVertex vt;

        var needCapacity = verts.Count + end - start;
        if (verts.Capacity < needCapacity)
            verts.Capacity = needCapacity;

        for (int i = start; i < end; ++i)
        {
            vt = verts[i];
            verts.Add(vt);

            Vector3 v = vt.position;
            v.x += x;
            v.y += y;
            vt.position = v;
            var newColor = color;
            newColor.a = (byte)((newColor.a * verts[i].color.a) / 255);
            vt.color = newColor;
            verts[i] = vt;
        }
    }

    private bool SetProperty<T>(ref T property, T value)
    {
        if (!Equals(property, value))
        {
            property = value;
            m_MeshDirty = true;
            return true;
        }
        return false;
    }

    private TextGenerationSettings GetGenerationSetting(Vector2 extents)
    {
        var settings = new TextGenerationSettings();

        settings.generationExtents = extents;
        if (font != null && font.dynamic)
        {
            settings.fontSize = m_FontSize;
            settings.resizeTextMinSize = 0;
            settings.resizeTextMaxSize = 0;
        }
        settings.textAnchor = (TextAnchor)(m_Alignment + 3);
        settings.alignByGeometry = false;
        settings.scaleFactor = 1;
        settings.color = m_Color;
        settings.font = font;
        settings.pivot = new Vector2(((int)m_Anchor % 3) * 0.5f, (2 - (int)m_Anchor / 3) * 0.5f);
        settings.richText = m_RichText;
        settings.lineSpacing = m_LineSpacing;
        settings.fontStyle = m_FontStyle;
        settings.resizeTextForBestFit = false;
        settings.updateBounds = true;
        settings.horizontalOverflow = extents.x == 0 ? HorizontalWrapMode.Overflow : HorizontalWrapMode.Wrap;
        settings.verticalOverflow = extents.y == 0 ? VerticalWrapMode.Overflow : VerticalWrapMode.Truncate;

        return settings;
    }
    #region FontUpdateTracker 
    static Dictionary<Font, HashSet<BatchesText>> m_Tracked = new Dictionary<Font, HashSet<BatchesText>>();

    static void TrackText(BatchesText t)
    {
        if (t.font == null)
            return;

        HashSet<BatchesText> exists;
        m_Tracked.TryGetValue(t.font, out exists);
        if (exists == null)
        {
            if (m_Tracked.Count == 0)
                Font.textureRebuilt += RebuildForFont;

            exists = new HashSet<BatchesText>();
            m_Tracked.Add(t.font,exists);
        }

        exists.Add(t);
    }

    static void UntrackText(BatchesText t)
    {
        if (t.font == null)
            return;

        HashSet<BatchesText> texts;
        m_Tracked.TryGetValue(t.font, out texts);

        if (texts == null)
            return;

        texts.Remove(t);

        if (texts.Count==0)
        {
            m_Tracked.Remove(t.font);
            if (m_Tracked.Count == 0)
                Font.textureRebuilt -= RebuildForFont;
        }
    }

    private static void RebuildForFont(Font f)
    {
        HashSet<BatchesText> texts;
        m_Tracked.TryGetValue(f,out texts);

        if (texts == null)
            return;

        foreach (var text in texts)
        {
            text.FontTexttureChanged();
        }
    }
    #endregion
#if UNITY_EDITOR
    private void Reset()
    {
        if (m_Font == null)
        {
            font = BatchesUtil.GetDefaultFont();

        }
        if (m_Material == null)
        {
            material = BatchesUtil.GetDefaultMaterial();
        }
    }

    private void OnValidate()
    {
        UntrackText(this);
        TrackText(this);
        m_MeshDirty = true;
    }


    [CanEditMultipleObjects, CustomEditor(typeof(BatchesSprite), true)]
    class BatchesTextMeshEditor : Editor
    {
        static private class Styles
        {
            public static GUIStyle alignmentButtonLeft = new GUIStyle(EditorStyles.miniButtonLeft);
            public static GUIStyle alignmentButtonMid = new GUIStyle(EditorStyles.miniButtonMid);
            public static GUIStyle alignmentButtonRight = new GUIStyle(EditorStyles.miniButtonRight);

            public static GUIContent m_EncodingContent;

            public static GUIContent m_LeftAlignText;
            public static GUIContent m_CenterAlignText;
            public static GUIContent m_RightAlignText;
            public static GUIContent m_TopAlignText;
            public static GUIContent m_MiddleAlignText;
            public static GUIContent m_BottomAlignText;

            public static GUIContent m_LeftAlignTextActive;
            public static GUIContent m_CenterAlignTextActive;
            public static GUIContent m_RightAlignTextActive;
            public static GUIContent m_TopAlignTextActive;
            public static GUIContent m_MiddleAlignTextActive;
            public static GUIContent m_BottomAlignTextActive;

            static Styles()
            {
                m_EncodingContent = new GUIContent("Rcih Text","Use enmticons and colors");

                //horizontal Alignment Icons
                m_LeftAlignText = EditorGUIUtility.IconContent(@"GUISystem/align_horizontally_left","Left Align");
                m_CenterAlignText = EditorGUIUtility.IconContent(@"GUISystem/align_horizontally_center", "Center Align");
                m_RightAlignText = EditorGUIUtility.IconContent(@"GUISystem/align_horizontally_right", "Right Align");
                m_LeftAlignTextActive = EditorGUIUtility.IconContent(@"GUISystem/align_horizontally_left_active", "Left Align");
                m_CenterAlignTextActive = EditorGUIUtility.IconContent(@"GUISystem/align_horizontally_center_active", "Center Align");
                m_RightAlignTextActive = EditorGUIUtility.IconContent(@"GUISystem/align_horizontally_right_active", "Right Align");

                //vertical Alignment Icons
                m_TopAlignText = EditorGUIUtility.IconContent(@"GUISystem/align_vertically_top", "Top Align");
                m_MiddleAlignText = EditorGUIUtility.IconContent(@"GUISystem/align_vertically_center", "Middle Align");//middle
                m_BottomAlignText = EditorGUIUtility.IconContent(@"GUISystem/align_vertically_bottom", "Bottom Align");
                m_TopAlignTextActive = EditorGUIUtility.IconContent(@"GUISystem/align_vertically_top_active", "Top Align");
                m_MiddleAlignTextActive = EditorGUIUtility.IconContent(@"GUISystem/align_vertically_center_active", "Middle Align");//middle
                m_BottomAlignTextActive = EditorGUIUtility.IconContent(@"GUISystem/align_vertically_bottom_active", "Bottom Align");

                FixAlignmentButtonStyles(alignmentButtonLeft,alignmentButtonMid,alignmentButtonRight);
            }

            static void FixAlignmentButtonStyles(params GUIStyle [] styles)
            {
                foreach (GUIStyle style in styles)
                {
                    style.padding.left = 2;
                    style.padding.right = 2;
                }
            }
        }

        private SerializedProperty m_Text;
        private SerializedProperty m_Font;
        private SerializedProperty m_FontSize;
        private SerializedProperty m_FontStyle;
        private SerializedProperty m_LineSpacing;
        private SerializedProperty m_RichText;
        private SerializedProperty m_Color;
        private SerializedProperty m_Material;
        private SerializedProperty m_PixelsperUnit;
        private SerializedObject m_RendererObject;
        private SerializedProperty m_Anchor;
        private SerializedProperty m_Alignment;
        private SerializedProperty m_SortingOrder;

        private SerializedProperty m_ShadowOpen;
        private SerializedProperty m_ShadowOffX;
        private SerializedProperty m_ShadowOffY;
        private SerializedProperty m_ShadowColor;
        private void OnEnable()
        {
            m_Text = serializedObject.FindProperty("m_Text");
            m_Font = serializedObject.FindProperty("m_Font");
            m_FontSize = serializedObject.FindProperty("m_FontSize");
            m_FontStyle = serializedObject.FindProperty("m_FontStyle");
            m_LineSpacing = serializedObject.FindProperty("m_LineSpacing");
            m_RichText = serializedObject.FindProperty("m_RichText");
            m_Color = serializedObject.FindProperty("m_Color");
            m_Material = serializedObject.FindProperty("m_Material");
            m_PixelsperUnit = serializedObject.FindProperty("m_PixelsperUnit");
            m_Anchor = serializedObject.FindProperty("m_Anchor");
            m_Alignment = serializedObject.FindProperty("m_Alignment");
            m_SortingOrder = serializedObject.FindProperty("m_SortingOrder");

            m_ShadowOpen = serializedObject.FindProperty("m_ShadowOpen");
            m_ShadowOffX = serializedObject.FindProperty("m_ShadowOffX");
            m_ShadowOffY = serializedObject.FindProperty("m_ShadowOffY");
            m_ShadowColor = serializedObject.FindProperty("m_ShadowColor");
            var renderer = ((BatchesSprite)target).GetComponent<MeshRenderer>();
            m_RendererObject = new SerializedObject(renderer);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(m_Text);
            EditorGUILayout.LabelField("Character",EditorStyles.boldLabel);
            ++EditorGUI.indentLevel;
            {
                EditorGUILayout.PropertyField(m_Font);
                EditorGUILayout.PropertyField(m_FontSize);
                EditorGUILayout.PropertyField(m_FontStyle);
                EditorGUILayout.PropertyField(m_LineSpacing);
                EditorGUILayout.PropertyField(m_RichText);
            }
            --EditorGUI.indentLevel;

            EditorGUILayout.LabelField("Rectangle", EditorStyles.boldLabel);
            ++EditorGUI.indentLevel;
            {
                EditorGUILayout.PropertyField(m_Anchor);
                EditorGUILayout.PropertyField(m_Alignment);
            }
            --EditorGUI.indentLevel;

            if (EditorGUILayout.PropertyField(m_PixelsperUnit, true, new GUILayoutOption[0]))
            {
                if (m_PixelsperUnit.floatValue < 0.001f)
                {
                    m_PixelsperUnit.floatValue = 0.001f;
                }
            }

            EditorGUILayout.PropertyField(m_Color, true, new GUILayoutOption[0]);
            EditorGUILayout.PropertyField(m_Material, true, new GUILayoutOption[0]);
            EditorGUILayout.PropertyField(m_SortingOrder, true, new GUILayoutOption[0]);
            EditorGUILayout.LabelField("阴影", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(m_ShadowOpen);
            ++EditorGUI.indentLevel;
               EditorGUILayout.PropertyField(m_ShadowOffX, true, new GUILayoutOption[0]);
               EditorGUILayout.PropertyField(m_ShadowOffY, true, new GUILayoutOption[0]);
               EditorGUILayout.PropertyField(m_ShadowColor, true, new GUILayoutOption[0]);
            --EditorGUI.indentLevel;

            m_RendererObject.Update();
            m_RendererObject.ApplyModifiedProperties();

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
