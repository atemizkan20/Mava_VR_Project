using UnityEditor;
using UnityEngine;

namespace AdventurePuzzleKit.NoteSystem
{
    [CustomEditor(typeof(CustomNoteController))]
    public class CustomNoteEditor : Editor
    {
        SerializedProperty _isReadable;
        SerializedProperty pageScale;
        SerializedProperty pageImage;

        SerializedProperty hasMultPages;
        SerializedProperty noteText;

        SerializedProperty noteTextAreaScale;
        SerializedProperty textSize;
        SerializedProperty fontType;
        SerializedProperty fontStyle;
        SerializedProperty fontColor;

        SerializedProperty _allowAudioPlayback;
        SerializedProperty playOnOpen;
        SerializedProperty noteReadAudio;
        SerializedProperty noteFlipAudio;

        SerializedProperty _isNoteTrigger;
        SerializedProperty triggerObject;

        bool textCustomisationGroup, textAreaSettingsGroup;

        void OnEnable()
        {
            _isReadable = serializedObject.FindProperty(nameof(_isReadable));

            pageScale = serializedObject.FindProperty(nameof(pageScale));
            hasMultPages = serializedObject.FindProperty(nameof(hasMultPages));
            pageImage = serializedObject.FindProperty(nameof(pageImage));

            noteText = serializedObject.FindProperty(nameof(noteText));
            noteTextAreaScale = serializedObject.FindProperty(nameof(noteTextAreaScale));

            textSize = serializedObject.FindProperty(nameof(textSize));
            fontType = serializedObject.FindProperty(nameof(fontType));
            fontStyle = serializedObject.FindProperty(nameof(fontStyle));
            fontColor = serializedObject.FindProperty(nameof(fontColor));

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
            EditorGUILayout.ObjectField("Script:", MonoScript.FromMonoBehaviour((CustomNoteController)target), typeof(CustomNoteController), false);
            GUI.enabled = true;

            EditorGUILayout.Space(5);
            CustomNoteController _customNoteScript = (CustomNoteController)target;
            #endregion

            #region isReadable Section
            EditorGUILayout.Space(2);
            EditorGUILayout.PropertyField(_isReadable);
            #endregion

            #region Basic Page Settings
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Basic Page Settings", EditorStyles.toolbarTextField);
            EditorGUILayout.Space(2);
            EditorGUILayout.PropertyField(pageScale);
            EditorGUILayout.Space(2);
            EditorGUILayout.PropertyField(pageImage);
            EditorGUILayout.Space(2);
            EditorGUILayout.PropertyField(hasMultPages);
            EditorGUILayout.PropertyField(noteText);
            EditorGUILayout.Space(5);
            #endregion

            #region Text Customisation
            EditorGUILayout.LabelField("Text Customisation", EditorStyles.toolbarTextField);
            EditorGUILayout.Space(2);
            EditorGUILayout.PropertyField(noteTextAreaScale);
            EditorGUILayout.Space(2);

            textCustomisationGroup = EditorGUILayout.Foldout(textCustomisationGroup, "Font Settings");
            if (textCustomisationGroup)
            {
                EditorGUILayout.PropertyField(textSize);
                EditorGUILayout.PropertyField(fontType);
                EditorGUILayout.PropertyField(fontStyle);
                EditorGUILayout.PropertyField(fontColor);
            }
            EditorGUILayout.Space(5);
            #endregion

            #region Audio Settings
            EditorGUILayout.LabelField("Basic Audio Settings", EditorStyles.toolbarTextField);
            EditorGUILayout.Space(2);

            EditorGUILayout.PropertyField(_allowAudioPlayback);
            if (_customNoteScript.allowAudioPlayback)
            {
                EditorGUILayout.Space(2);
                EditorGUILayout.LabelField("Note Playback Settings", EditorStyles.boldLabel);
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
            if (_customNoteScript.isNoteTrigger)
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
