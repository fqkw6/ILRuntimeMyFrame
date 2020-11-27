using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(MonoView), true)]
public class MonoViewInspector : UnityEditor.Editor
{
    public class ObjecctType
    {
        public SerializedProperty Object;
        public SerializedProperty Type;
        public SerializedProperty Name;
    }

    private SerializedProperty mObjects;
    private List<ObjecctType> mObjecctTypes;


    private void OnEnable()
    {
        this.mObjects = this.serializedObject.FindProperty("infos");
        Debug.LogError(mObjects + "jiajiajd11122ijaisdjai");
        this.mObjecctTypes = this.ToObjecctTypes(this.mObjects);

    }

    private void OnDestroy()
    {

    }
    protected void DrawBaseInspectorGUI()
    {
        base.OnInspectorGUI();
    }

    public override void OnInspectorGUI()
    {
        using (var space = new EditorGUILayout.VerticalScope())
        {
            this.DrawUnityEnginrObjects();
        }
        this.serializedObject.ApplyModifiedProperties();
    }

    private void DrawUnityEnginrObjects()
    {
        this.mObjecctTypes = this.ToObjecctTypes(this.mObjects);

        EditorGUILayout.LabelField("Objects: ");

        for (int i = 0; i < this.mObjecctTypes.Count; i++)
        {
            using (var space1 = new EditorGUILayout.HorizontalScope("TextField"))
            {
                var rElementNamePro = this.mObjecctTypes[i].Name;
                var rElementObjPro = this.mObjecctTypes[i].Object;
                var rElementTypePro = this.mObjecctTypes[i].Type;

                EditorGUILayout.PropertyField(rElementNamePro,new GUIContent(""));
                EditorGUILayout.PropertyField(rElementObjPro, new GUIContent(""));
            }
        }
    }

    private List<ObjecctType> ToObjecctTypes(SerializedProperty rObjects)
    {
        var rObjectTypes = new List<ObjecctType>();

        if (rObjects == null) return rObjectTypes;
        for (int i = 0; i < rObjects.arraySize; i++)
        {
            SerializedProperty rElementPro = rObjects.GetArrayElementAtIndex(i);

            var rObjectType = new ObjecctType()
            {
                Object = rElementPro.FindPropertyRelative("Object"),
                Type = rElementPro.FindPropertyRelative("Type"),
                Name = rElementPro.FindPropertyRelative("Name"),
            };
            if (rObjectType.Object != null && rObjectType.Object.objectReferenceValue != null && string.IsNullOrEmpty(rObjectType.Name.stringValue))
            {
                rObjectType.Name.stringValue = rObjectType.Object.objectReferenceValue.name;
            }
            rObjectTypes.Add(rObjectType);
        }

        return rObjectTypes;
    }
}

