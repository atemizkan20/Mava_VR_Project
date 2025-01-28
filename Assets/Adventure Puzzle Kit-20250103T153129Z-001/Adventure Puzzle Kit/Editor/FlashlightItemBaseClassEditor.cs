using UnityEngine;
using UnityEditor;

namespace AdventurePuzzleKit.FlashlightSystem
{
    [CustomEditor(typeof(FlashlightItemBaseClass), true)] // Add "true" to make the custom editor apply to derived classes.
    public class FlashlightItemBaseClassEditor : Editor
    {
        SerializedProperty objectTypeProp;
        SerializedProperty batteryNumberProp;

        private void OnEnable()
        {
            objectTypeProp = serializedObject.FindProperty("_objectType");
            batteryNumberProp = serializedObject.FindProperty("batteryNumber");
        }

        public override void OnInspectorGUI()
        {
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script:", MonoScript.FromMonoBehaviour((FlashlightItemBaseClass)target), typeof(FlashlightItemBaseClass), false);
            GUI.enabled = true;

            EditorGUILayout.PropertyField(objectTypeProp);

            FlashlightItemBaseClass.ObjectType selectedType = (FlashlightItemBaseClass.ObjectType)objectTypeProp.enumValueIndex;

            if (selectedType == FlashlightItemBaseClass.ObjectType.Battery)
            {
                EditorGUILayout.Space(5);
                EditorGUILayout.PropertyField(batteryNumberProp);
            }

            EditorGUILayout.Space(5);

            OpenEditorScript();

            serializedObject.ApplyModifiedProperties();
        }

        void OpenEditorScript()
        {
            if (GUILayout.Button("Open Editor Script"))
            {
                string scriptFilePath = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this));
                AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<MonoScript>(scriptFilePath));
            }
        }
    }
}

