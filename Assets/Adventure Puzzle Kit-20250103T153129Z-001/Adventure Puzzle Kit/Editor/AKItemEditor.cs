using UnityEngine;
using UnityEditor;

namespace AdventurePuzzleKit
{
    [CustomEditor(typeof(AKItem))]
    public class AKItemEditor : Editor
    {
        SerializedProperty _systemType;
        SerializedProperty _secondarySystemType;

        SerializedProperty _showNameHighlight;
        SerializedProperty _showNameHelpPrompt;
        SerializedProperty itemName;

        private void OnEnable()
        {
            _systemType = serializedObject.FindProperty(nameof(_systemType));
            _secondarySystemType = serializedObject.FindProperty(nameof(_secondarySystemType));

            _showNameHighlight = serializedObject.FindProperty(nameof(_showNameHighlight));
            _showNameHelpPrompt = serializedObject.FindProperty(nameof(_showNameHelpPrompt));
            itemName = serializedObject.FindProperty(nameof(itemName));
        }

        public override void OnInspectorGUI()
        {
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script:", MonoScript.FromMonoBehaviour((AKItem)target), typeof(AKItem), false);
            GUI.enabled = true;

            AKItem _AKitem = (AKItem)target;

            #region System Type
            EditorGUILayout.LabelField("System Type", EditorStyles.toolbarTextField);

            EditorGUILayout.Space(5);

            EditorGUILayout.PropertyField(_systemType);
            if (_AKitem._systemType == AKItem.SystemType.ExamineSys)
            {
                EditorGUILayout.PropertyField(_secondarySystemType);
            }

            EditorGUILayout.Space(5);
            #endregion

            #region Name Highlight Section
            EditorGUILayout.LabelField("Name Highlight (Tooltip For More Information)", EditorStyles.toolbarTextField);

            EditorGUILayout.PropertyField(_showNameHighlight);
            if (_AKitem.showNameHighlight)
            {
                EditorGUILayout.PropertyField(itemName);
                EditorGUILayout.PropertyField(_showNameHelpPrompt);
            }

            EditorGUILayout.Space(5);
            #endregion  

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

