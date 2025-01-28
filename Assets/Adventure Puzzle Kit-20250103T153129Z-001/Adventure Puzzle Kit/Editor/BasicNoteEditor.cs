using UnityEditor;
using UnityEngine;

namespace AdventurePuzzleKit.NoteSystem
{
    [CustomEditor(typeof(BasicNoteController))]
    public class BasicNoteEditor : Editor
    {
        SerializedProperty _isReadable;
        SerializedProperty noteScale;
        SerializedProperty hasMultPages;
        SerializedProperty pageImages;
        SerializedProperty _allowAudioPlayback;
        SerializedProperty playOnOpen;
        SerializedProperty noteReadAudio;
        SerializedProperty noteFlipAudio;
        SerializedProperty _isNoteTrigger;
        SerializedProperty triggerObject;

        bool allowAudioGroup;

        void OnEnable()
        {
            _isReadable = serializedObject.FindProperty(nameof(_isReadable));

            noteScale = serializedObject.FindProperty(nameof(noteScale));
            hasMultPages = serializedObject.FindProperty(nameof(hasMultPages));
            pageImages = serializedObject.FindProperty(nameof(pageImages));

            _allowAudioPlayback = serializedObject.FindProperty(nameof(_allowAudioPlayback));
            playOnOpen = serializedObject.FindProperty(nameof(playOnOpen));

            noteReadAudio = serializedObject.FindProperty(nameof(noteReadAudio));
            noteFlipAudio = serializedObject.FindProperty(nameof(noteFlipAudio));

            _isNoteTrigger = serializedObject.FindProperty(nameof(_isNoteTrigger));
            triggerObject = serializedObject.FindProperty(nameof(triggerObject));
        }

        public override void OnInspectorGUI()
        {
            #region Visual Script Reference
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script:", MonoScript.FromMonoBehaviour((BasicNoteController)target), typeof(BasicNoteController), false);
            GUI.enabled = true;

            EditorGUILayout.Space(5);
            BasicNoteController _basicNoteScript = (BasicNoteController)target;
            #endregion

            #region isReadable Section
            EditorGUILayout.Space(2);
            EditorGUILayout.PropertyField(_isReadable);
            #endregion

            #region Basic Page Settings
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Basic Page Settings", EditorStyles.toolbarTextField);

            EditorGUILayout.PropertyField(noteScale);
            EditorGUILayout.PropertyField(hasMultPages);
            EditorGUILayout.PropertyField(pageImages);
            EditorGUILayout.Space(5);
            #endregion

            #region Basic Audio Settings
            EditorGUILayout.LabelField("Basic Audio Settings", EditorStyles.toolbarTextField);
            EditorGUILayout.Space(2);

            EditorGUILayout.PropertyField(_allowAudioPlayback);
            if (_basicNoteScript.allowAudioPlayback)
            {
                EditorGUILayout.Space(2);
                EditorGUILayout.LabelField("Note Playback Settings", EditorStyles.toolbarTextField);
                EditorGUILayout.PropertyField(playOnOpen);
                EditorGUILayout.PropertyField(noteReadAudio);
            }

            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Note Default Audio", EditorStyles.toolbarTextField);

            EditorGUILayout.Space(2);
            EditorGUILayout.PropertyField(noteFlipAudio);
            #endregion

            #region Trigger Settings
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Basic Trigger Settings", EditorStyles.toolbarTextField);

            EditorGUILayout.Space(2);
            EditorGUILayout.PropertyField(_isNoteTrigger);
            if (_basicNoteScript.isNoteTrigger)
            {
                EditorGUILayout.PropertyField(triggerObject);
            }
            #endregion

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
