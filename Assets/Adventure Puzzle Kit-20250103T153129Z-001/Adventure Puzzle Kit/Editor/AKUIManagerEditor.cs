using UnityEditor;
using UnityEngine;

namespace AdventurePuzzleKit
{
    [CustomEditor(typeof(AKUIManager))]
    public class AKUIManagerEditor : Editor
    {
        #region Inventory UI SerializedProperties
        SerializedProperty adventureKitCanvas;
        
        SerializedProperty flashlightUI;
        SerializedProperty flashlightBatteryUI;

        SerializedProperty gasmaskUI;
        SerializedProperty gasMaskFilterUI;

        SerializedProperty generatorUI;
        SerializedProperty themedKeyUI;
        SerializedProperty valveUI;
        SerializedProperty chessPuzzleUI;

        SerializedProperty themedKeyContainerUI;
        SerializedProperty valveContainerUI;
        SerializedProperty chessPuzzleContainerUI;

        SerializedProperty _radialIndicator;
        SerializedProperty triggerInteractPrompt;

        SerializedProperty crosshair;

        SerializedProperty nameHighlightCanvas;
        #endregion

        #region Flashlight SeralizedProperties
        SerializedProperty flashlightIndicatorUI;
        SerializedProperty batteryLevelUI;
        SerializedProperty batteryIconUI;
        SerializedProperty batteryCountUI;
        #endregion

        #region Fuse SeralizedProperties
        SerializedProperty fuseboxUI;
        SerializedProperty fuseIcon;
        SerializedProperty fuseCountUI;
        #endregion

        #region Gas Mask SeralizedProperties
        SerializedProperty maskEquippedColor;
        SerializedProperty maskNormalColor;

        SerializedProperty filterAlarmColor;
        SerializedProperty filterNormalColor;

        SerializedProperty healthTextUI;
        SerializedProperty healthSliderUI;

        SerializedProperty _maskIconUI;
        SerializedProperty _filterIconUI;
        SerializedProperty _filterCountUI;
        SerializedProperty _filterSliderUI;

        SerializedProperty visorImageOverlay;

        SerializedProperty _postProcessingVolume;
        SerializedProperty _originalProfile;
        SerializedProperty _gasMaskProfile;
        #endregion

        #region Generator SeralizedProperties
        SerializedProperty fuelFillUI;
        SerializedProperty currentFuelText;
        SerializedProperty maximumFuelText;
        #endregion

        #region Examine SerializedProperties
        SerializedProperty noUICloseButton;

        SerializedProperty basicItemNameUI;
        SerializedProperty basicItemDescUI;
        SerializedProperty basicExamineUI;

        SerializedProperty rightItemNameUI;
        SerializedProperty rightItemDescUI;
        SerializedProperty rightExamineUI;

        SerializedProperty interactionItemNameUI;
        SerializedProperty interactionNameHelpPrompt;
        SerializedProperty interactionNameMainUI;

        SerializedProperty interestPointText;
        SerializedProperty interestPointParentUI;

        SerializedProperty showHelp;
        SerializedProperty examineHelpUI;
        #endregion

        #region ThemedKey SerializedProperties
        SerializedProperty tkInventorySlots;
        SerializedProperty tkInventoryBGSlots;
        #endregion

        #region Valve SerializedProperties
        SerializedProperty valveInventorySlots;
        SerializedProperty valveInventoryBGSlots;
        SerializedProperty valveProgressUI;
        SerializedProperty sliderParentUI;
        #endregion

        #region Chess SerializedProperties
        SerializedProperty _slotWidgets;
        #endregion

        #region Safe SerializedProperties
        SerializedProperty safeCanvasUI;
        SerializedProperty acceptBtn;
        SerializedProperty numberUI;
        SerializedProperty selectionBtn;
        #endregion

        #region Phone SerializedProperties
        SerializedProperty payPhoneCodeText;
        SerializedProperty officePhoneCodeText;
        SerializedProperty mobilePhoneCodeText;

        SerializedProperty payPhoneCanvas;
        SerializedProperty officePhoneCanvas;
        SerializedProperty mobilePhoneCanvas;
        #endregion

        #region Keypad SerliazedProperties
        SerializedProperty modernCodeText;
        SerializedProperty scifiCodeText;
        SerializedProperty keyboardCodeText;

        SerializedProperty modernCanvas;
        SerializedProperty scifiCanvas;
        SerializedProperty keyboardCanvas;
        #endregion

        #region Show / Hide Property Groups
        bool showFlashlightGroup
        {
            get { return EditorPrefs.GetBool("ShowFlashlightGroup", false); }
            set { EditorPrefs.SetBool("ShowFlashlightGroup", value); }
        }
        bool showFuseboxGroup
        {
            get { return EditorPrefs.GetBool("showFuseboxGroup", false); }
            set { EditorPrefs.SetBool("showFuseboxGroup", value); }
        }
        bool showGasmaskGroup
        {
            get { return EditorPrefs.GetBool("showGasmaskGroup", false); }
            set { EditorPrefs.SetBool("showGasmaskGroup", value); }
        }
        bool showGeneratorGroup
        {
            get { return EditorPrefs.GetBool("showGeneratorGroup", false); }
            set { EditorPrefs.SetBool("showGeneratorGroup", value); }
        }
        bool showExamineGroup
        {
            get { return EditorPrefs.GetBool("showExamineGroup", false); }
            set { EditorPrefs.SetBool("showExamineGroup", value); }
        }

        bool showThemedKeyGroup
        {
            get { return EditorPrefs.GetBool("showThemedKeyGroup", false); }
            set { EditorPrefs.SetBool("showThemedKeyGroup", value); }
        }

        bool showValveGroup
        {
            get { return EditorPrefs.GetBool("showValveGroup", false); }
            set { EditorPrefs.SetBool("showValveGroup", value); }
        }

        bool showChessGroup
        {
            get { return EditorPrefs.GetBool("showChessGroup", false); }
            set { EditorPrefs.SetBool("showChessGroup", value); }
        }

        bool showSafeGroup
        {
            get { return EditorPrefs.GetBool("showSafeGroup", false); }
            set { EditorPrefs.SetBool("showSafeGroup", value); }
        }

        bool showPhoneGroup
        {
            get { return EditorPrefs.GetBool("showPhoneGroup", false); }
            set { EditorPrefs.SetBool("showPhoneGroup", value); }
        }

        bool showKeypadGroup
        {
            get { return EditorPrefs.GetBool("showKeypadGroup", false); }
            set { EditorPrefs.SetBool("showKeypadGroup", value); }
        }

        bool showMainSystemUIGroup
        {
            get { return EditorPrefs.GetBool("showMainSystemUIGroup", false); }
            set { EditorPrefs.SetBool("showMainSystemUIGroup", value); }
        }

        bool showContainerUIGroup
        {
            get { return EditorPrefs.GetBool("showContainerUIGroup", false); }
            set { EditorPrefs.SetBool("showContainerUIGroup", value); }
        }

        bool showSharedUIGroup
        {
            get { return EditorPrefs.GetBool("showSharedUIGroup", false); }
            set { EditorPrefs.SetBool("showSharedUIGroup", value); }
        }
        #endregion

        void OnEnable()
        {
            #region Inventory UI SerializedObjects
            adventureKitCanvas = serializedObject.FindProperty(nameof(adventureKitCanvas));

            flashlightUI = serializedObject.FindProperty(nameof(flashlightUI));
            flashlightBatteryUI = serializedObject.FindProperty(nameof(flashlightBatteryUI));

            gasmaskUI = serializedObject.FindProperty(nameof(gasmaskUI));
            gasMaskFilterUI = serializedObject.FindProperty(nameof(gasMaskFilterUI));

            generatorUI = serializedObject.FindProperty(nameof(generatorUI));
            themedKeyUI = serializedObject.FindProperty(nameof(themedKeyUI));
            valveUI = serializedObject.FindProperty(nameof(valveUI));
            chessPuzzleUI = serializedObject.FindProperty(nameof(chessPuzzleUI));

            themedKeyContainerUI = serializedObject.FindProperty(nameof(themedKeyContainerUI));
            valveContainerUI = serializedObject.FindProperty(nameof(valveContainerUI));
            chessPuzzleContainerUI = serializedObject.FindProperty(nameof(chessPuzzleContainerUI));

            _radialIndicator = serializedObject.FindProperty(nameof(_radialIndicator));
            triggerInteractPrompt = serializedObject.FindProperty(nameof(triggerInteractPrompt));

            crosshair = serializedObject.FindProperty(nameof(crosshair));

            nameHighlightCanvas = serializedObject.FindProperty(nameof(nameHighlightCanvas));     
            #endregion

            #region Flashlight SerializedObjects 
            flashlightIndicatorUI = serializedObject.FindProperty(nameof(flashlightIndicatorUI));
            batteryLevelUI = serializedObject.FindProperty(nameof(batteryLevelUI));

            batteryIconUI = serializedObject.FindProperty(nameof(batteryIconUI));
            batteryCountUI = serializedObject.FindProperty(nameof(batteryCountUI));
            #endregion

            #region Fuse SerializedObjects 
            fuseboxUI = serializedObject.FindProperty(nameof(fuseboxUI));
            fuseIcon = serializedObject.FindProperty(nameof(fuseIcon));
            fuseCountUI = serializedObject.FindProperty(nameof(fuseCountUI));
            #endregion

            #region Gas Mask SerializedObjects 
            maskEquippedColor = serializedObject.FindProperty(nameof(maskEquippedColor));
            maskNormalColor = serializedObject.FindProperty(nameof(maskNormalColor));

            filterAlarmColor = serializedObject.FindProperty(nameof(filterAlarmColor));
            filterNormalColor = serializedObject.FindProperty(nameof(filterNormalColor));

            healthTextUI = serializedObject.FindProperty(nameof(healthTextUI));
            healthSliderUI = serializedObject.FindProperty(nameof(healthSliderUI));

            _maskIconUI = serializedObject.FindProperty(nameof(_maskIconUI));
            _filterIconUI = serializedObject.FindProperty(nameof(_filterIconUI));
            _filterCountUI = serializedObject.FindProperty(nameof(_filterCountUI));
            _filterSliderUI = serializedObject.FindProperty(nameof(_filterSliderUI));

            visorImageOverlay = serializedObject.FindProperty(nameof(visorImageOverlay));

            _postProcessingVolume = serializedObject.FindProperty(nameof(_postProcessingVolume));
            _originalProfile = serializedObject.FindProperty(nameof(_originalProfile));
            _gasMaskProfile = serializedObject.FindProperty(nameof(_gasMaskProfile));
            #endregion

            #region Generator SerializedObjects 
            fuelFillUI = serializedObject.FindProperty(nameof(fuelFillUI));
            currentFuelText = serializedObject.FindProperty(nameof(currentFuelText));
            maximumFuelText = serializedObject.FindProperty(nameof(maximumFuelText));
            #endregion

            #region Examine SerializedObjects 
            noUICloseButton = serializedObject.FindProperty(nameof(noUICloseButton));

            basicItemNameUI = serializedObject.FindProperty(nameof(basicItemNameUI));
            basicItemDescUI = serializedObject.FindProperty(nameof(basicItemDescUI));
            basicExamineUI = serializedObject.FindProperty(nameof(basicExamineUI));

            rightItemNameUI = serializedObject.FindProperty(nameof(rightItemNameUI));
            rightItemDescUI = serializedObject.FindProperty(nameof(rightItemDescUI));
            rightExamineUI = serializedObject.FindProperty(nameof(rightExamineUI));

            interactionItemNameUI = serializedObject.FindProperty(nameof(interactionItemNameUI));
            interactionNameHelpPrompt = serializedObject.FindProperty(nameof(interactionNameHelpPrompt));
            interactionNameMainUI = serializedObject.FindProperty(nameof(interactionNameMainUI));

            interestPointText = serializedObject.FindProperty(nameof(interestPointText));
            interestPointParentUI = serializedObject.FindProperty(nameof(interestPointParentUI));

            showHelp = serializedObject.FindProperty(nameof(showHelp));
            examineHelpUI = serializedObject.FindProperty(nameof(examineHelpUI));
            #endregion

            #region ThemedKey SerializedObjects
            tkInventorySlots = serializedObject.FindProperty(nameof(tkInventorySlots));
            tkInventoryBGSlots = serializedObject.FindProperty(nameof(tkInventoryBGSlots));
            #endregion

            #region Valve SerializedObjects
            valveInventorySlots = serializedObject.FindProperty(nameof(valveInventorySlots));
            valveInventoryBGSlots = serializedObject.FindProperty(nameof(valveInventoryBGSlots));
            valveProgressUI = serializedObject.FindProperty(nameof(valveProgressUI));
            sliderParentUI = serializedObject.FindProperty(nameof(sliderParentUI));
            #endregion

            #region Chess SerializedObjects
            _slotWidgets = serializedObject.FindProperty(nameof(_slotWidgets));
            #endregion

            #region Safe SerializedObjects
            safeCanvasUI = serializedObject.FindProperty(nameof(safeCanvasUI));
            acceptBtn = serializedObject.FindProperty(nameof(acceptBtn));
            numberUI = serializedObject.FindProperty(nameof(numberUI));
            selectionBtn = serializedObject.FindProperty(nameof(selectionBtn));
            #endregion

            #region Phone SerializedObjects
            payPhoneCodeText = serializedObject.FindProperty(nameof(payPhoneCodeText));
            officePhoneCodeText = serializedObject.FindProperty(nameof(officePhoneCodeText));
            mobilePhoneCodeText = serializedObject.FindProperty(nameof(mobilePhoneCodeText));

            payPhoneCanvas = serializedObject.FindProperty(nameof(payPhoneCanvas));
            officePhoneCanvas = serializedObject.FindProperty(nameof(officePhoneCanvas));
            mobilePhoneCanvas = serializedObject.FindProperty(nameof(mobilePhoneCanvas));
            #endregion

            #region Keypad SerializedObjects
            modernCodeText = serializedObject.FindProperty(nameof(modernCodeText));
            scifiCodeText = serializedObject.FindProperty(nameof(scifiCodeText));
            keyboardCodeText = serializedObject.FindProperty(nameof(keyboardCodeText));

            modernCanvas = serializedObject.FindProperty(nameof(modernCanvas));
            scifiCanvas = serializedObject.FindProperty(nameof(scifiCanvas));
            keyboardCanvas = serializedObject.FindProperty(nameof(keyboardCanvas));
            #endregion
        }

        public override void OnInspectorGUI()
        {
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script:", MonoScript.FromMonoBehaviour((AKUIManager)target), typeof(AKUIManager), false);
            GUI.enabled = true;

            EditorGUILayout.Space(5);

            EditorGUILayout.LabelField("Main Canvas", EditorStyles.toolbarTextField);
            EditorGUILayout.PropertyField(adventureKitCanvas);

            EditorGUILayout.Space(5);

            ShowIndividualUIParent();

            EditorGUILayout.Space(5);

            ShowContainerUI();

            EditorGUILayout.Space(5);

            ShowSharedUI();

            EditorGUILayout.Space(5);

            FlashlightLogic();

            EditorGUILayout.Space(5);

            FuseBoxLogic();

            EditorGUILayout.Space(5);

            GasMaskLogic();

            EditorGUILayout.Space(5);

            GeneratorLogic();

            EditorGUILayout.Space(5);

            ExamineLogic();

            EditorGUILayout.Space(5);

            ThemedKeyLogic();

            EditorGUILayout.Space(5);

            ValveLogic();

            EditorGUILayout.Space(5);

            ChessLogic();

            EditorGUILayout.Space(5);

            SafeLogic();

            EditorGUILayout.Space(5);

            PhoneLogic();

            EditorGUILayout.Space(5);

            KeypadLogic();

            EditorGUILayout.Space(5);

            OpenEditorScript();

            serializedObject.ApplyModifiedProperties();
        }

        void ShowIndividualUIParent()
        {
            EditorGUILayout.LabelField("Individual UI Elements", EditorStyles.toolbarTextField);

            showMainSystemUIGroup = EditorGUILayout.BeginFoldoutHeaderGroup(showMainSystemUIGroup, "Show Parent UI Groups");
            if (showMainSystemUIGroup)
            {
                EditorGUILayout.PropertyField(flashlightUI);
                EditorGUILayout.PropertyField(flashlightBatteryUI);

                EditorGUILayout.PropertyField(gasmaskUI);
                EditorGUILayout.PropertyField(gasMaskFilterUI);

                EditorGUILayout.PropertyField(generatorUI);
                EditorGUILayout.PropertyField(themedKeyUI);
                EditorGUILayout.PropertyField(valveUI);
                EditorGUILayout.PropertyField(chessPuzzleUI);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        void ShowContainerUI()
        {
            EditorGUILayout.LabelField("Container UI Elements", EditorStyles.toolbarTextField);

            showContainerUIGroup = EditorGUILayout.BeginFoldoutHeaderGroup(showContainerUIGroup, "Show Container UI Groups");
            if (showContainerUIGroup)
            {
                EditorGUILayout.PropertyField(themedKeyContainerUI);
                EditorGUILayout.PropertyField(valveContainerUI);
                EditorGUILayout.PropertyField(chessPuzzleContainerUI);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        void ShowSharedUI()
        {
            EditorGUILayout.LabelField("Shared UI Elements", EditorStyles.toolbarTextField);

            showSharedUIGroup = EditorGUILayout.BeginFoldoutHeaderGroup(showSharedUIGroup, "Show Shared UI Groups");
            if (showSharedUIGroup)
            {
                EditorGUILayout.PropertyField(_radialIndicator);
                EditorGUILayout.PropertyField(triggerInteractPrompt);
                EditorGUILayout.PropertyField(crosshair);
                EditorGUILayout.PropertyField(nameHighlightCanvas);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        void FlashlightLogic()
        {
            EditorGUILayout.LabelField("Flashlight UI Elements", EditorStyles.toolbarTextField);

            showFlashlightGroup = EditorGUILayout.Toggle("Show Flashlight Properties", showFlashlightGroup);
            if (showFlashlightGroup)
            {
                EditorGUILayout.PropertyField(flashlightIndicatorUI);
                EditorGUILayout.PropertyField(batteryLevelUI);

                EditorGUILayout.PropertyField(batteryIconUI);   
                EditorGUILayout.PropertyField(batteryCountUI);
            }
        }

        void FuseBoxLogic()
        {
            EditorGUILayout.LabelField("Fuse Box UI Elements", EditorStyles.toolbarTextField);

            showFuseboxGroup = EditorGUILayout.Toggle("Show Fusebox Properties", showFuseboxGroup);
            if (showFuseboxGroup)
            {
                EditorGUILayout.PropertyField(fuseboxUI);
                EditorGUILayout.PropertyField(fuseIcon);
                EditorGUILayout.PropertyField(fuseCountUI);
            }
        }

        void GasMaskLogic()
        {
            EditorGUILayout.LabelField("Gas Mask UI Elements", EditorStyles.toolbarTextField);

            showGasmaskGroup = EditorGUILayout.Toggle("Show Gas Mask Properties", showGasmaskGroup);
            if (showGasmaskGroup)
            {
                EditorGUILayout.PropertyField(maskEquippedColor);
                EditorGUILayout.PropertyField(maskNormalColor);

                EditorGUILayout.PropertyField(filterAlarmColor);
                EditorGUILayout.PropertyField(filterNormalColor);

                EditorGUILayout.PropertyField(healthTextUI);
                EditorGUILayout.PropertyField(healthSliderUI);

                EditorGUILayout.PropertyField(_maskIconUI);
                EditorGUILayout.PropertyField(_filterIconUI);
                EditorGUILayout.PropertyField(_filterCountUI);
                EditorGUILayout.PropertyField(_filterSliderUI);

                EditorGUILayout.PropertyField(visorImageOverlay);

                EditorGUILayout.PropertyField(_postProcessingVolume);
                EditorGUILayout.PropertyField(_originalProfile);
                EditorGUILayout.PropertyField(_gasMaskProfile);
            }
        }

        void GeneratorLogic()
        {
            EditorGUILayout.LabelField("Generator UI Elements", EditorStyles.toolbarTextField);

            showGeneratorGroup = EditorGUILayout.Toggle("Show Generator Properties", showGeneratorGroup);
            if (showGeneratorGroup)
            {
                EditorGUILayout.PropertyField(fuelFillUI);
                EditorGUILayout.PropertyField(currentFuelText);
                EditorGUILayout.PropertyField(maximumFuelText);
            }
        }

        void ExamineLogic()
        {
            EditorGUILayout.LabelField("Examine UI Elements", EditorStyles.toolbarTextField);

            showExamineGroup = EditorGUILayout.Toggle("Show Examine Properties", showExamineGroup);
            if (showExamineGroup)
            {
                EditorGUILayout.PropertyField(noUICloseButton);

                EditorGUILayout.PropertyField(basicItemNameUI);
                EditorGUILayout.PropertyField(basicItemDescUI);
                EditorGUILayout.PropertyField(basicExamineUI);

                EditorGUILayout.PropertyField(rightItemNameUI);
                EditorGUILayout.PropertyField(rightItemDescUI);
                EditorGUILayout.PropertyField(rightExamineUI);

                EditorGUILayout.PropertyField(interactionItemNameUI);
                EditorGUILayout.PropertyField(interactionNameHelpPrompt);
                EditorGUILayout.PropertyField(interactionNameMainUI);

                EditorGUILayout.PropertyField(interestPointText);
                EditorGUILayout.PropertyField(interestPointParentUI);

                EditorGUILayout.PropertyField(showHelp);
                EditorGUILayout.PropertyField(examineHelpUI);
            }
        }

        void ThemedKeyLogic()
        {
            EditorGUILayout.LabelField("Themed Key UI Elements", EditorStyles.toolbarTextField);

            showThemedKeyGroup = EditorGUILayout.Toggle("Show Themed Key Properties", showThemedKeyGroup);
            if (showThemedKeyGroup)
            {
                EditorGUILayout.PropertyField(tkInventorySlots);
                EditorGUILayout.PropertyField(tkInventoryBGSlots);          
            }
        }

        void ValveLogic()
        {
            EditorGUILayout.LabelField("Valve UI Elements", EditorStyles.toolbarTextField);

            showValveGroup = EditorGUILayout.Toggle("Show Valve Properties", showValveGroup);
            if (showValveGroup)
            {
                EditorGUILayout.PropertyField(valveInventorySlots);
                EditorGUILayout.PropertyField(valveInventoryBGSlots);
               
                EditorGUILayout.Space(5);

                EditorGUILayout.PropertyField(valveProgressUI);
                EditorGUILayout.PropertyField(sliderParentUI);
            }
        }

        void ChessLogic()
        {
            EditorGUILayout.LabelField("Chess UI Elements", EditorStyles.toolbarTextField);

            showChessGroup = EditorGUILayout.Toggle("Show Chess Properties", showChessGroup);
            if (showChessGroup)
            {
                EditorGUILayout.PropertyField(_slotWidgets);
            }
        }

        void SafeLogic()
        {
            EditorGUILayout.LabelField("Safe UI Elements", EditorStyles.toolbarTextField);

            showSafeGroup = EditorGUILayout.Toggle("Show Safe Properties", showSafeGroup);
            if (showSafeGroup)
            {
                EditorGUILayout.PropertyField(safeCanvasUI);
                EditorGUILayout.PropertyField(acceptBtn);
                EditorGUILayout.PropertyField(numberUI);
                EditorGUILayout.PropertyField(selectionBtn);
            }
        }

        void PhoneLogic()
        {
            EditorGUILayout.LabelField("Phone UI Elements", EditorStyles.toolbarTextField);

            showPhoneGroup = EditorGUILayout.Toggle("Show Phone Properties", showPhoneGroup);
            if (showPhoneGroup)
            {
                EditorGUILayout.PropertyField(payPhoneCodeText);
                EditorGUILayout.PropertyField(officePhoneCodeText);
                EditorGUILayout.PropertyField(mobilePhoneCodeText);

                EditorGUILayout.PropertyField(payPhoneCanvas);
                EditorGUILayout.PropertyField(officePhoneCanvas);
                EditorGUILayout.PropertyField(mobilePhoneCanvas);
            }
        }

        void KeypadLogic()
        {
            EditorGUILayout.LabelField("Keypad UI Elements", EditorStyles.toolbarTextField);

            showKeypadGroup = EditorGUILayout.Toggle("Show Keypad Properties", showKeypadGroup);
            if (showKeypadGroup)
            {
                EditorGUILayout.PropertyField(modernCodeText);
                EditorGUILayout.PropertyField(scifiCodeText);
                EditorGUILayout.PropertyField(keyboardCodeText);

                EditorGUILayout.PropertyField(modernCanvas);
                EditorGUILayout.PropertyField(scifiCanvas);
                EditorGUILayout.PropertyField(keyboardCanvas);
            }
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
