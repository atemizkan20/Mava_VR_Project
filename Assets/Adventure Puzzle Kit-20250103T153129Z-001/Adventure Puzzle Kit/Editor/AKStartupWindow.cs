using UnityEditor;
using UnityEngine;

namespace AdventurePuzzleKit
{
    public class AKStartupWindow : EditorWindow
    {
        private bool postProcessingFoldout;
        private bool renderPipelineFoldout;
        private bool itemTextFoldout;
        private bool layersTextFoldout;
        private bool quickSetupTextFoldout;

        private Texture banner;

        [MenuItem("Window/Adventure Kit Support")]
        private static void Open()
        {
            EditorApplication.delayCall += () =>
            {
                GetWindow(typeof(AKStartupWindow));
            };
        }

        [InitializeOnLoadMethod]
        private static void OpenOnStart()
        {
            if (!SessionState.GetBool("AdventureKitSupportOpened", false))
            {
                Open();
                SessionState.SetBool("AdventureKitSupportOpened", true);
            }
        }

        private void OnEnable()
        {
            banner = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Adventure Puzzle Kit/Additional Packages/AKBanner.png", typeof(Texture));
        }

        private void OnGUI()
        {
            EditorGUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Box(banner, GUILayout.Width(480), GUILayout.Height(100));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            StarterMessage();

            EditorGUILayout.Space(5);

            EditorGUILayout.LabelField("Common Questions", EditorStyles.toolbarTextField);

            EditorGUILayout.Space(5);

            QuickSetupMessage();

            EditorGUILayout.Space(5);

            LayersMessage();

            EditorGUILayout.Space(5);

            PostProcessingMessage();

            EditorGUILayout.Space(5);

            ItemNameMessage();

            EditorGUILayout.Space(5);

            PipelineMessage();

            EditorGUILayout.Space(5);

            EditorGUILayout.LabelField("Useful Links", EditorStyles.toolbarTextField);

            UsefulLinks();
        }

        void StarterMessage()
        {
            string starterText = "Hey there! Welcome to the Adventure Puzzle Kit. Thanks for checking it out. If you do have any issues, suggestions or find any bugs - Send me an email and I'll be " +
                "happy to give you any information you need. That's best best way to get hold of me.";
            EditorStyles.textField.wordWrap = true;
            EditorGUILayout.TextArea(starterText);
        }

        void QuickSetupMessage()
        {
            quickSetupTextFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(quickSetupTextFoldout, "Fastest way to get started?");
            EditorGUILayout.Space(5);
            if (quickSetupTextFoldout)
            {
                string quickSetupText = "Drag the 'APK_EntireDemoScene_Prefab' into your scene and you'll have everything to go without any setup at all, then you can remove puzzles or assets" +
                    "as required!";

                EditorStyles.textField.wordWrap = true;
                EditorGUILayout.TextArea(quickSetupText);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        void LayersMessage()
        {
            layersTextFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(layersTextFoldout, "What Tags & Layers do I need?");
            EditorGUILayout.Space(5);
            if (layersTextFoldout)
            {
                string layersText = "Add Layers: 'ExamineLayer', 'InspectPointLayer', 'PadlockSpinner' & 'PostProcess'";
                string tagText = "Add Tags: 'InteractiveObject', 'ExaminePoint', 'InspectPoint'";

                EditorGUILayout.TextArea(tagText);
                EditorGUILayout.TextArea(layersText);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        void PostProcessingMessage()
        {
            postProcessingFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(postProcessingFoldout, "Post Processing Error");
            EditorGUILayout.Space(5);
            if (postProcessingFoldout)
            {
                string postProcessingText = "Make sure to import post processing if you're using the gas mask system, this can be found in 'Window' > 'Package Manager' > 'Unity Registry " +
                    "Dropdown' (Top left) > Type in 'Post Processing' > Install";

                EditorStyles.textField.wordWrap = true;
                EditorGUILayout.TextArea(postProcessingText);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        void ItemNameMessage()
        {
            itemTextFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(itemTextFoldout, "Item Text Missing?");
            EditorGUILayout.Space(5);
            if (itemTextFoldout)
            {
                string itemTextExamine = "When highlighting an item, if text isn't visible make sure the item in question has an 'Item Name' and 'Font' added to the inspector of the " +
                    "examine script";

                EditorStyles.textField.wordWrap = true;
                EditorGUILayout.TextArea(itemTextExamine);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        void PipelineMessage()
        {
            renderPipelineFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(renderPipelineFoldout, "URP / HDRP Setup");
            EditorGUILayout.Space(5);
            if (renderPipelineFoldout)
            {
                string renderTextHelp = "Check the online documenetation for pages on URP & HDRP setups but most of them just require upgrading of materials";

                EditorStyles.textField.wordWrap = true;
                EditorGUILayout.TextArea(renderTextHelp);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        void UsefulLinks()
        {
            EditorGUILayout.Space(5);

            if (GUILayout.Button("Online Documentation"))
            {
                Application.OpenURL("https://speedtutoruk.gitbook.io/apk-documentation/");
            }

            EditorGUILayout.Space(2);
            if (GUILayout.Button("Asset Store"))
            {
                Application.OpenURL("https://assetstore.unity.com/lists/speedtutor-puzzle-assets-5773131546630?aid=1101l9Bhe&utm_campaign=unity_affiliate&utm_medium=affiliate&utm_source=partnerize-linkmaker");
            }

            EditorGUILayout.Space(2);
            if (GUILayout.Button("YouTube"))
            {
                Application.OpenURL("https://www.youtube.com/user/speedtutor");
            }

            EditorGUILayout.Space(2);
            if (GUILayout.Button("Discord"))
            {
                Application.OpenURL("https://discord.com/invite/Dh3Kb7Z");
            }

            EditorGUILayout.Space(2);
            if (GUILayout.Button("Contact Me"))
            {
                Application.OpenURL("https://www.speed-tutor.com/pages/contact");
            }
        }
    }
}

