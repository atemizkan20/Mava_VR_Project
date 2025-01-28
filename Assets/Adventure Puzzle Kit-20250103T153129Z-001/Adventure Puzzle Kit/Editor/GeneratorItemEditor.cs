using UnityEditor;
using UnityEngine;

namespace AdventurePuzzleKit.GeneratorSystem
{
    [CustomEditor(typeof(GeneratorItem))]
    public class GeneratorItemEditor : Editor
    {
        SerializedProperty itemType;

        SerializedProperty _canBurnFuel;
        SerializedProperty burnRate;

        SerializedProperty _canRumble;
        SerializedProperty rumbleSpeed;
        SerializedProperty rumbleIntensity;

        SerializedProperty _itemFuelAmount;
        SerializedProperty _itemMaxFuelAmount;

        SerializedProperty _showUI;
        SerializedProperty _popoutUI;

        SerializedProperty fuelSwishSound;
        SerializedProperty waterPourSound;

        SerializedProperty activateGenerator;
        SerializedProperty deactivateGenerator;

        bool generatorSelection, fuelParameters, soundGroup, generatorEvents;

        void OnEnable()
        {
            itemType = serializedObject.FindProperty(nameof(itemType));

            _canBurnFuel = serializedObject.FindProperty(nameof(_canBurnFuel));
            burnRate = serializedObject.FindProperty(nameof(burnRate));

            _canRumble = serializedObject.FindProperty(nameof(_canRumble));
            rumbleSpeed = serializedObject.FindProperty(nameof(rumbleSpeed));
            rumbleIntensity = serializedObject.FindProperty(nameof(rumbleIntensity));

            _itemFuelAmount = serializedObject.FindProperty(nameof(_itemFuelAmount));
            _itemMaxFuelAmount = serializedObject.FindProperty(nameof(_itemMaxFuelAmount));

            _showUI = serializedObject.FindProperty(nameof(_showUI));
            _popoutUI = serializedObject.FindProperty(nameof(_popoutUI));

            fuelSwishSound = serializedObject.FindProperty(nameof(fuelSwishSound));
            waterPourSound = serializedObject.FindProperty(nameof(waterPourSound));

            activateGenerator = serializedObject.FindProperty(nameof(activateGenerator));
            deactivateGenerator = serializedObject.FindProperty(nameof(deactivateGenerator));

        }

        public override void OnInspectorGUI()
        {
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script:", MonoScript.FromMonoBehaviour((GeneratorItem)target), typeof(GeneratorItem), false);
            GUI.enabled = true;

            GeneratorItem _generatorItem = (GeneratorItem)target;

            EditorGUILayout.Space(5);

            ItemTypeSelection();

            EditorGUILayout.Space(5);

            EditorGUILayout.LabelField("Fuel Parameters", EditorStyles.toolbarTextField);
            EditorGUILayout.Space(5);

            EditorGUILayout.PropertyField(_itemFuelAmount);
            EditorGUILayout.PropertyField(_itemMaxFuelAmount);

            if (_generatorItem.itemType == GeneratorItem.GeneratorItemType.Generator)
            {
                EditorGUILayout.Space(5);
                EditorGUILayout.LabelField("Generator ONLY Parameters", EditorStyles.toolbarTextField);
                EditorGUILayout.Space(5);

                generatorSelection = EditorGUILayout.BeginFoldoutHeaderGroup(generatorSelection, "Generator Parameters");
                if (generatorSelection)
                {
                    EditorGUILayout.PropertyField(_canBurnFuel);
                    if (_generatorItem.canBurnFuel)
                    {
                        EditorGUILayout.PropertyField(burnRate);
                    }

                    EditorGUILayout.Space(5);

                    EditorGUILayout.PropertyField(_canRumble);
                    if (_generatorItem.canRumble)
                    {
                        EditorGUILayout.PropertyField(rumbleSpeed);
                        EditorGUILayout.PropertyField(rumbleIntensity);
                    }
                }
                EditorGUILayout.EndFoldoutHeaderGroup();
            }

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Object Canvas Settings", EditorStyles.toolbarTextField);
            EditorGUILayout.Space(5);

            EditorGUILayout.PropertyField(_showUI);

            if (_generatorItem.showUI)
            {
                EditorGUILayout.PropertyField(_popoutUI);
            }

            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Sound Effects", EditorStyles.toolbarTextField);

            EditorGUILayout.Space(5);
            EditorGUILayout.PropertyField(fuelSwishSound);
            EditorGUILayout.PropertyField(waterPourSound);
            EditorGUILayout.Space(5);

            if (_generatorItem.itemType == GeneratorItem.GeneratorItemType.Generator)
            {
                EditorGUILayout.Space(5);
                EditorGUILayout.LabelField("Generator Activate / Deactivate ", EditorStyles.toolbarTextField);

                generatorEvents = EditorGUILayout.BeginFoldoutHeaderGroup(generatorEvents, "Generator Events");
                if (generatorEvents)
                {
                    EditorGUILayout.PropertyField(activateGenerator);
                    EditorGUILayout.PropertyField(deactivateGenerator);
                }
                EditorGUILayout.EndFoldoutHeaderGroup();
            }

            EditorGUILayout.Space(5);

            OpenEditorScript();

            serializedObject.ApplyModifiedProperties();
        }

        void ItemTypeSelection()
        {
            EditorGUILayout.LabelField("Item Type", EditorStyles.toolbarTextField);
            EditorGUILayout.Space(5);

            EditorGUILayout.PropertyField(itemType);
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
