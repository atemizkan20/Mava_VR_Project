using UnityEditor;
using UnityEngine;

namespace AdventurePuzzleKit.NoteSystem
{
    [CustomEditor(typeof(CustomReverseNoteController))]
    public class CustomReverseNoteCustomEditor : Editor
    {
        SerializedProperty _isReadable;
        SerializedProperty pageScale;
        SerializedProperty pageImage;

        SerializedProperty hasMultPages;
        SerializedProperty noteReverseText;

        SerializedProperty mainTextAreaScale;
        SerializedProperty mainTextSize;
        SerializedProperty mainFontType;
        SerializedProperty mainFontStyle;
        SerializedProperty mainFontColor;

        SerializedProperty flipTextBGColor;

        SerializedProperty flipTextAreaScale;
        SerializedProperty flipTextBGScale;
        SerializedProperty flipTextSize;
        SerializedProperty flipFontType;
        SerializedProperty flipFontStyle;
        SerializedProperty flipFontColor;

        SerializedProperty _allowAudioPlayback;
        SerializedProperty playOnOpen;
        SerializedProperty noteReadAudio;
        SerializedProperty noteFlipAudio;

        SerializedProperty _isNoteTrigger;
        SerializedProperty triggerObject;

        bool textCustomisationGroup, textAreaSettingsGroup, flipBGColorGroup, flipTextCustomisationGroup;

        void OnEnable()
        {
            _isReadable = serializedObject.FindProperty(nameof(_isReadable));

            pageScale = serializedObject.FindProperty(nameof(pageScale));
            pageImage = serializedObject.FindProperty(nameof(pageImage));

            hasMultPages = serializedObject.FindProperty(nameof(hasMultPages));
            noteReverseText = serializedObject.FindProperty(nameof(noteReverseText));

            mainTextAreaScale = serializedObject.FindProperty(nameof(mainTextAreaScale));
            mainTextSize = serializedObject.FindProperty(nameof(mainTextSize));
            mainFontType = serializedObject.FindProperty(nameof(mainFontType));
            mainFontStyle = serializedObject.FindProperty(nameof(mainFontStyle));
            mainFontColor = serializedObject.FindProperty(nameof(mainFontColor));

            flipTextBGColor = serializedObject.FindProperty(nameof(flipTextBGColor));
            flipTextAreaScale = serializedObject.FindProperty(nameof(flipTextAreaScale));
            flipTextBGScale = serializedObject.FindProperty(nameof(flipTextBGScale));
            flipTextSize = serializedObject.FindProperty(nameof(flipTextSize));
            flipFontType = serializedObject.FindProperty(nameof(flipFontType));
            flipFontStyle = serializedObject.FindProperty(nameof(flipFontStyle));
            flipFontColor = serializedObject.FindProperty(nameof(flipFontColor));

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
            EditorGUILayout.ObjectField("Script:", MonoScript.FromMonoBehaviour((CustomReverseNoteController)target), typeof(CustomReverseNoteController), false);
            GUI.enabled = true;
            #endregion

            EditorGUILayout.Space(5);
            CustomReverseNoteController _customNoteScript = (CustomReverseNoteController)target;

            #region isReadable Section
            EditorGUILayout.LabelField("Basic Note Settings", EditorStyles.toolbarTextField);

            EditorGUILayout.Space(2);
            EditorGUILayout.PropertyField(_isReadable);
            #endregion

            #region Basic Page Settings
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Basic Page Settings", EditorStyles.toolbarTextField);
            EditorGUILayout.Space(2);
            EditorGUILayout.PropertyField(pageScale);
            EditorGUILayout.PropertyField(pageImage);

            EditorGUILayout.Space(5);
            EditorGUILayout.PropertyField(hasMultPages);
            EditorGUILayout.PropertyField(noteReverseText);
            #endregion

            #region Main Note Text Customisation
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Main Note Text Customisation", EditorStyles.toolbarTextField);

            EditorGUILayout.Space(2);
            EditorGUILayout.PropertyField(mainTextAreaScale);

            EditorGUILayout.Space(2);

            textCustomisationGroup = EditorGUILayout.Foldout(textCustomisationGroup, "Main Note Font Settings");
            if (textCustomisationGroup)
            {
                EditorGUILayout.PropertyField(mainTextSize);
                EditorGUILayout.PropertyField(mainFontType);
                EditorGUILayout.PropertyField(mainFontStyle);
                EditorGUILayout.PropertyField(mainFontColor);
            }
            EditorGUILayout.Space(5);
            #endregion

            #region Flip / Reverse Text Customisation Settings
            EditorGUILayout.LabelField("Flip / Reverse Text Customisation Settings", EditorStyles.toolbarTextField);
            EditorGUILayout.Space(2);

            EditorGUILayout.PropertyField(flipTextAreaScale);
            EditorGUILayout.Space(2);
            EditorGUILayout.PropertyField(flipTextBGColor);

            EditorGUILayout.Space(2);
            flipTextCustomisationGroup = EditorGUILayout.Foldout(flipTextCustomisationGroup, "Flip Font Settings");
            {
                if (flipTextCustomisationGroup)
                {
                    EditorGUILayout.PropertyField(flipTextSize);
                    EditorGUILayout.PropertyField(flipFontType);
                    EditorGUILayout.PropertyField(flipFontStyle);
                    EditorGUILayout.PropertyField(flipFontColor);
                }
            }
            EditorGUILayout.Space(5);
            #endregion

            #region Basic Audio Settings
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
