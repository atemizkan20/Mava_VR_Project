using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using AdventurePuzzleKit.ChessSystem;
using AdventurePuzzleKit.ExamineSystem;
using AdventurePuzzleKit.ThemedKey;
using AdventurePuzzleKit.ValveSystem;
using AdventurePuzzleKit.SafeSystem;
using AdventurePuzzleKit.PhoneSystem;
using AdventurePuzzleKit.KeypadSystem;
using UnityEngine.EventSystems;

namespace AdventurePuzzleKit
{
    public class AKUIManager : MonoBehaviour
    {
        #region Inventory UI Fields
        //Adventure Kit UI Canvas
        [SerializeField] private GameObject adventureKitCanvas = null;

        //V1.6 - Asset UI Elements
        [SerializeField] private GameObject flashlightUI = null;
        [SerializeField] private GameObject flashlightBatteryUI = null;
        [SerializeField] private GameObject gasmaskUI = null;
        [SerializeField] private GameObject gasMaskFilterUI = null;
        [SerializeField] private GameObject generatorUI = null;
        [SerializeField] private GameObject themedKeyUI = null;
        [SerializeField] private GameObject valveUI = null;
        [SerializeField] private GameObject chessPuzzleUI = null;

        //[Header("Asset Containers UI")]
        [SerializeField] private GameObject themedKeyContainerUI = null;
        [SerializeField] private GameObject valveContainerUI = null;
        [SerializeField] private GameObject chessPuzzleContainerUI = null;

        //[Header("Indicator Prompts")]
        [SerializeField] private Image _radialIndicator = null;
        [SerializeField] private GameObject triggerInteractPrompt = null;

        //[Header("UI / Crosshair")]
        [SerializeField] private Image crosshair = null;

        [SerializeField] private Canvas nameHighlightCanvas = null;
        #endregion

        #region Flashlight Fields
        [SerializeField] private Image flashlightIndicatorUI = null;
        [SerializeField] private Image batteryLevelUI = null;

        [SerializeField] private Image batteryIconUI = null;
        [SerializeField] private Text batteryCountUI = null;
        #endregion

        #region Fuse Fields
        [SerializeField] private GameObject fuseboxUI = null;
        [SerializeField] private Image fuseIcon = null;
        [SerializeField] private Text fuseCountUI = null;
        #endregion

        #region GasMask Fields
        public enum MaskUIState { MaskNormal, MaskEquipped }
        private MaskUIState _maskUIState;

        public enum FilterState { FilterNumber, FilterAlarm, FilterNormal, FilterValue }
        private FilterState _filterState;

        public enum PostProcessState { OriginalPostProcess, GasPostProcess }
        private PostProcessState _postProcessState;

        [Header("Gas Mask UI Colours")]
        [SerializeField] private Color maskEquippedColor = Color.green;
        [SerializeField] private Color maskNormalColor = Color.white;

        [Header("Filter UI Colours")]
        [SerializeField] private Color filterAlarmColor = Color.red;
        [SerializeField] private Color filterNormalColor = Color.white;

        [Header("Health UI")]
        [SerializeField] private Text healthTextUI = null;
        [SerializeField] private Image healthSliderUI = null;

        [Header("Gas Mask UI")]
        [SerializeField] private Image _maskIconUI = null;
        [SerializeField] private Image _filterIconUI = null;
        [SerializeField] private Text _filterCountUI = null;
        [SerializeField] private Image _filterSliderUI = null;

        [Header("Visor Overlay UI")]
        [SerializeField] private GameObject visorImageOverlay = null;

        [Header("Post Processing Effects")]
        [SerializeField] private Volume _postProcessingVolume = null;
        [SerializeField] private VolumeProfile _originalProfile = null;
        [SerializeField] private VolumeProfile _gasMaskProfile = null;
        private Vignette _vignette;
        private DepthOfField _dof;
        #endregion

        #region Generator System Fields
        [Header("UI Image Elements")]
        [SerializeField] private Image fuelFillUI = null;

        [Header("UI Text Elements")]
        [SerializeField] private Text currentFuelText = null;
        [SerializeField] private Text maximumFuelText = null;
        #endregion

        #region Examine Fields
        [Header("No UI Close Button")]
        [SerializeField] private GameObject noUICloseButton = null;

        [Header("Basic Example UI References")]
        [SerializeField] private Text basicItemNameUI = null;
        [SerializeField] private Text basicItemDescUI = null;
        [SerializeField] private GameObject basicExamineUI = null;

        [Header("Right Side Example UI References")]
        [SerializeField] private Text rightItemNameUI = null;
        [SerializeField] private Text rightItemDescUI = null;
        [SerializeField] private GameObject rightExamineUI = null;

        [Header("Interaction Name UI")]
        [SerializeField] private Text interactionItemNameUI = null;
        [SerializeField] private GameObject interactionNameHelpPrompt = null;
        [SerializeField] private GameObject interactionNameMainUI = null;

        [Header("Interest Point UI's")]
        [SerializeField] private Text interestPointText = null;
        [SerializeField] private GameObject interestPointParentUI = null;

        [HideInInspector] public ExaminableItem _examinableItem;

        [Header("Help Panel Visibility")]
        [SerializeField] private bool showHelp = false;
        [SerializeField] private GameObject examineHelpUI = null;
        #endregion

        #region ThemedKey Fields
        //Themed Key Inventory Slots
        [SerializeField] private Image[] tkInventorySlots = null;
        [SerializeField] private Image[] tkInventoryBGSlots = null;
        #endregion

        #region Valve Fields
        [SerializeField] private Image[] valveInventorySlots = null;
        [SerializeField] private Image[] valveInventoryBGSlots = null;

        //Valve Slider UI
        [SerializeField] private Image valveProgressUI = null;
        [SerializeField] private CanvasGroup sliderParentUI = null;

        public Image ValveProgressUI
        {
            get { return valveProgressUI; }
            set { valveProgressUI = value; }
        }

        public CanvasGroup SliderParentUI
        {
            get { return sliderParentUI; }
            set { sliderParentUI = value; }
        }
        #endregion

        #region Chess Fields
        [SerializeField] private ChessSlotWidget[] _slotWidgets = null;

        //private bool disableInventoryOpen = false;
        private bool disableRemoveButton;
        private CPFuseBoxInteractable _fuseBox;

        public CPFuseBoxInteractable fuseBoxInteractable
        {
            get { return _fuseBox; }
            set
            {
                _fuseBox = value;
                foreach (ChessSlotWidget slotWidget in _slotWidgets)
                {
                    slotWidget.FuseBox = _fuseBox;
                }
            }
        }
        #endregion

        #region Safe Fields
        [Tooltip("Add the main safe canvas here")]
        [SerializeField] private GameObject safeCanvasUI = null;

        [Tooltip("Add the UI numbers text UI elements here")]
        [Space(5)] [SerializeField] private Button acceptBtn = null;

        [Tooltip("Add the UI numbers text UI elements here")]
        [Space(5)] [SerializeField] private Text[] numberUI = new Text[3];

        [Tooltip("Add the UI selection buttons, there should be 3")]
        [Space(5)] [SerializeField] private Button[] selectionBtn = new Button[3];

        public string playerInputNumber { get; private set; }
        #endregion

        #region Phone Fields
        [Header("Phone Type Input Fields")]
        [SerializeField] private InputField payPhoneCodeText = null;
        [SerializeField] private InputField officePhoneCodeText = null;
        [SerializeField] private InputField mobilePhoneCodeText = null;

        [Header("Phone Type Canvas Fields")]
        [SerializeField] private GameObject payPhoneCanvas = null;
        [SerializeField] private GameObject officePhoneCanvas = null;
        [SerializeField] private GameObject mobilePhoneCanvas = null;

        private bool firstPhoneClick;
        private PhoneController _phoneController;
        private PhoneType _phoneType;
        private enum PhoneType { None, Pay, Office, Mobile };
        #endregion

        #region Keypad Fields
        [Header("Keypad Type Input Fields")]
        [SerializeField] private InputField modernCodeText = null;
        [SerializeField] private InputField scifiCodeText = null;
        [SerializeField] private InputField keyboardCodeText = null;

        [Header("Phone Type Canvas Fields")]
        [SerializeField] private GameObject modernCanvas = null;
        [SerializeField] private GameObject scifiCanvas = null;
        [SerializeField] private GameObject keyboardCanvas = null;

        private bool firstKeypadClick;
        private KeypadController _keypadController;
        private KeypadType _keypadType;
        private enum KeypadType { None, Modern, Scifi, Keyboard };
        #endregion

        #region Collectable Fields
        public Image radialIndicator
        {
            get { return _radialIndicator; }
            set { _radialIndicator = value; }
        }

        public bool hasFlashlight { get; set; }
        public bool disableFlashlightUI { get; set; }
        public bool hasJerrycan { get; set; }
        public bool disableJerrycan { get; set; }
        public bool hasGasMask { get; set; }
        public bool disableGasmaskUI { get; set; }
        public bool hasThemedKey { get; set; }
        public bool hasValve { get; set; }
        public bool hasChessPiece { get; set; }
        public bool hasFuse { get; set; }

        public bool isInteracting;
        public bool isInventoryOpen { get; set; }
        #endregion

        private bool showUI;

        public static AKUIManager instance;

        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }

            print("Please make sure Post Processing is installed, navigate to Package Manager > Unity Registery > Type 'Post Processing' - Then install");

            _gasMaskProfile.TryGet(out _vignette);
            _gasMaskProfile.TryGet(out _dof);
        }

        #region Update For Enabling / Disabling Inventory
        private void Update()
        {
            // If isInteracting is true, exit the method early
            if (isInteracting)
                return;

            if (Input.GetKeyDown(AKInputManager.instance.toggleInventoryKey))
            {
                // If showUI is true, it becomes false and vice versa
                showUI = !showUI;

                if (showUI)
                {
                    OpenInventoryUI();
                }
                else
                {
                    CloseInventoryUI();
                }
            }
            // only check for close key if the UI is already shown
            else if (showUI && Input.GetKeyDown(AKInputManager.instance.closeInventoryKey))
            {
                CloseInventoryUI();
            }
        }
        #endregion

        #region Inventory Toggle - Check this****

        void OpenInventoryUI()
        {
            adventureKitCanvas.SetActive(true);
            isInventoryOpen = true;
            AKDisableManager.instance.DisablePlayerDefault(true, false, false);
            isInteracting = false;
            showUI = true;
        }

        void CloseInventoryUI()
        {
            adventureKitCanvas.SetActive(false);
            isInventoryOpen = false;
            AKDisableManager.instance.DisablePlayerDefault(false, false, false);
            isInteracting = false;
            fuseBoxInteractable = null;
            showUI = false;
        }
        #endregion

        #region Cursor
        public void ShowCursor(bool showCursor)
        {
            if (showCursor)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        #endregion

        #region Toggle Name Highlight Visibilty
        public void ToggleNameHighlight(bool isActive)
        {
            if (nameHighlightCanvas != null)
            {
                nameHighlightCanvas.enabled = isActive;
            }
            else
            {
                print("Add the Name Interact Canvas here, from the Examine UI container if you want to disable it");
            }

        }
        #endregion

        #region Crosshair
        public void ShowCrosshair(bool on)
        {
            crosshair.enabled = on;
        }

        public void HighlightCrosshair(bool on)
        {
            if (on)
            {
                crosshair.color = Color.red;
            }
            else
            {
                crosshair.color = Color.white;
            }
        }
        #endregion

        #region Radial Indicator
        public void EnableRadialIndicatorUI(float radialTimer)
        {
            radialIndicator.enabled = true;
            radialIndicator.fillAmount = radialTimer;
        }

        public void DisableRadialIndicatorUI(float radialTimer)
        {
            radialIndicator.fillAmount = radialTimer;
            radialIndicator.enabled = false;
        }
        #endregion

        #region InteractPrompt
        public void EnableInteractPrompt(bool on)
        {
            triggerInteractPrompt.SetActive(on);
        }
        #endregion

        #region UI Containers
        public void DisableUIContainers()
        {
            themedKeyContainerUI.SetActive(false);
            valveContainerUI.SetActive(false);
            chessPuzzleContainerUI.SetActive(false);
        }
        #endregion

        #region Examine UI Logic
        public void SetInspectPointParent(bool on, Vector3 position)
        {
            interestPointParentUI.SetActive(on);
            interestPointParentUI.transform.position = position;
        }

        public void SetInspectPointText(string inspectText)
        {
            interestPointText.text = inspectText;
        }

        public void SetBasicUIText(string itemName, string itemDescription, bool on)
        {
            basicItemNameUI.text = itemName;
            basicItemDescUI.text = itemDescription;
            basicExamineUI.SetActive(on);
        }

        public void SetBasicUITextSettings(int textSize, Font fontType, FontStyle fontStyle, Color fontColor, int textSizeDesc, Font fontTypeDesc, FontStyle fontStyleDesc, Color fontColorDesc)
        {
            basicItemNameUI.fontSize = textSize;
            basicItemNameUI.font = fontType;
            basicItemNameUI.fontStyle = fontStyle;
            basicItemNameUI.color = fontColor;

            basicItemDescUI.fontSize = textSizeDesc;
            basicItemDescUI.font = fontTypeDesc;
            basicItemDescUI.fontStyle = fontStyleDesc;
            basicItemDescUI.color = fontColorDesc;
        }

        public void SetRightSideUIText(string itemName, string itemDescription, bool on)
        {
            rightItemNameUI.text = itemName;
            rightItemDescUI.text = itemDescription;
            rightExamineUI.SetActive(on);
        }

        public void SetRightUITextSettings(int textSize, Font fontType, FontStyle fontStyle, Color fontColor, int textSizeDesc, Font fontTypeDesc, FontStyle fontStyleDesc, Color fontColorDesc)
        {
            rightItemNameUI.fontSize = textSize;
            rightItemNameUI.font = fontType;
            rightItemNameUI.fontStyle = fontStyle;
            rightItemNameUI.color = fontColor;

            rightItemDescUI.fontSize = textSizeDesc;
            rightItemDescUI.font = fontTypeDesc;
            rightItemDescUI.fontStyle = fontStyleDesc;
            rightItemDescUI.color = fontColorDesc;
        }

        public void SetHighlightName(string itemName, bool on, bool showPrompt)
        {
            interactionItemNameUI.text = itemName;
            interactionNameMainUI.SetActive(on);
            interactionNameHelpPrompt.SetActive(showPrompt);
        }

        public void ShowCloseButton(bool on)
        {
            noUICloseButton.SetActive(on);
        }

        public void ShowInteractionName(bool on)
        {
            interactionNameMainUI.SetActive(on);
        }

        public void CloseButton()
        {
            _examinableItem.DropObject(true);
        }

        public void ShowHelpPrompt(bool on)
        {
            if (showHelp)
            {
                examineHelpUI.SetActive(on);
            }
        }
        #endregion

        #region Chess Puzzle
        public void ChessPieceCollected()
        {
            hasChessPiece = true;
            if (chessPuzzleUI)
            {
                chessPuzzleUI.SetActive(true);
            }
            else
            {
                Debug.Log("Add the Chess piece canvas to avoid errors!");
            }
        }

        public void EnabledChessPuzzleUIContainer()
        {
            if (chessPuzzleUI)
            {
                DisableUIContainers();
                chessPuzzleContainerUI.SetActive(true);
            }
        }

        public void FillChessInventorySlot()
        {
            int chessPieceCount = CPInventory.instance.chessPieceList.Count;

            for (int i = 0; i < _slotWidgets.Length; i++)
            {
                ChessPiece chessPiece = i < chessPieceCount ? CPInventory.instance.chessPieceList[i] : null;
                _slotWidgets[i].SetPiece(chessPiece);
            }
        }

        public void ResetChessInventorySlot()
        {
            FillChessInventorySlot();
        }

        public void RemoveFuseButton()
        {
            if (!disableRemoveButton)
            {
                fuseBoxInteractable.RemoveFuse(fuseBoxInteractable.currentFuse);
                EventSystem.current.SetSelectedGameObject(null);
            }
        }

        public void OpenInventoryFusebox(CPFuseBoxInteractable fuseBoxController)
        {
            fuseBoxInteractable = fuseBoxController;
            OpenInventoryUI();
            disableRemoveButton = false;
        }

        public void DisableInventoryFusebox()
        {
            CloseInventoryUI();
            disableRemoveButton = true;
        }
        #endregion

        #region Flashlight
        public void FlashlightCollected()
        {
            hasFlashlight = true;
            if (!disableFlashlightUI)
            {
                if (flashlightUI)
                {
                    flashlightUI.SetActive(true);
                }
                else
                {
                    Debug.Log("Add the Flashlight UI Parent to avoid errors!");
                }
            }
        }

        public void BatteryCollected()
        {
            if (!disableFlashlightUI)
            {
                if (flashlightBatteryUI)
                {
                    flashlightBatteryUI.SetActive(true);
                }
                else
                {
                    Debug.Log("Add the Flashlight Battery Parent to avoid errors!");
                }
            }
        }

        public void ToggleFlashlightUI(bool showUI)
        {
            flashlightUI.SetActive(showUI);
        }
        public void FlashlightIndicatorColor(bool on)
        {
            flashlightIndicatorUI.color = on ? Color.white : Color.black;
        }
        public void MaximumBatteryLevel(float maxIntensity)
        {
            batteryLevelUI.fillAmount = maxIntensity;
        }
        public void UpdateBatteryLevelUI(float drainAmount)
        {
            batteryLevelUI.fillAmount -= drainAmount;
        }
        public void UpdateBatteryCountUI(int batteryCount)
        {
            if (batteryCount >= 1)
            {
                batteryIconUI.color = Color.white;
            }
            else
            {
                batteryIconUI.color = Color.black;
            }
            batteryCountUI.text = batteryCount.ToString("0");
        }
        #endregion

        #region GasMask
        public void GasMaskCollected()
        {
            hasGasMask = true;
            if (!disableGasmaskUI)
            {
                if (gasmaskUI)
                {
                    gasmaskUI.SetActive(true);
                }
            }
            else
            {
                Debug.Log("Add the Gas Mask Parent to avoid errors!");
            }
        }

        public void FilterCollected()
        {
            if (gasMaskFilterUI)
            {
                gasMaskFilterUI.SetActive(true);
            }
            else
            {
                Debug.Log("Add the Gas Mask Filter Parent to avoid errors!");
            }
        }

        public void GasChokingEffect(bool? on, bool dofEnabled)
        {
            EnableDOF(dofEnabled);
            if (on.HasValue)
            {
                if (on.Value)
                {
                    SwapPostProcessingProfile(PostProcessState.GasPostProcess);
                }
                else
                {
                    SwapPostProcessingProfile(PostProcessState.OriginalPostProcess);
                }
            }
        }

        public void GasMaskVisorUI(bool on)
        {
            visorImageOverlay.SetActive(on);
            EnableVignette(on);
            if (on)
            {
                SwapPostProcessingProfile(PostProcessState.GasPostProcess);
            }
            else
            {
                SwapPostProcessingProfile(PostProcessState.OriginalPostProcess);
            }
        }

        public void SwapPostProcessingProfile(PostProcessState _postProcessState)
        {
            switch (_postProcessState)
            {
                case PostProcessState.GasPostProcess:
                    _postProcessingVolume.profile = _gasMaskProfile;
                    break;
                case PostProcessState.OriginalPostProcess:
                    _postProcessingVolume.profile = _originalProfile;
                    break;
            }
        }

        public void EnableVignette(bool on)
        {
            _vignette.active = on;
        }

        public void EnableDOF(bool on)
        {
            _dof.active = on;
        }

        public void UpdateFilterUI(FilterState _filterState, string filterAmount, float filterFillAmount)
        {
            switch (_filterState)
            {
                case FilterState.FilterNumber:
                    _filterCountUI.text = filterAmount;
                    break;
                case FilterState.FilterAlarm:
                    _filterIconUI.color = filterAlarmColor;
                    break;
                case FilterState.FilterNormal:
                    _filterIconUI.color = filterNormalColor;
                    break;
                case FilterState.FilterValue:
                    _filterSliderUI.fillAmount = filterFillAmount;
                    break;
            }
        }

        public void UpdateMaskUI(MaskUIState _maskUIState)
        {
            switch (_maskUIState)
            {
                case MaskUIState.MaskNormal: _maskIconUI.color = maskNormalColor; break;
                case MaskUIState.MaskEquipped: _maskIconUI.color = maskEquippedColor; break;
            }
        }

        public void UpdateHealthUI(float currentHealth, float maxHealth)
        {
            healthTextUI.text = currentHealth.ToString("0");
            healthSliderUI.fillAmount = (currentHealth / maxHealth);
        }

        public void OnDestroy()
        {
            EnableVignette(false);
            EnableDOF(false);
        }
        #endregion

        #region Generator
        public void JerrycanCollected()
        {
            hasJerrycan = true;
            if (!disableJerrycan)
            {
                if (generatorUI)
                {
                    generatorUI.SetActive(true);
                }
                else
                {
                    Debug.Log("Add the Generator Canvas to avoid errors!");
                }
            }
        }
        public void UpdateInventoryUI(float currentFuel, float maximumFuel)
        {
            fuelFillUI.fillAmount = (currentFuel / maximumFuel);
            currentFuelText.text = currentFuel.ToString("0");
            maximumFuelText.text = maximumFuel.ToString("0");
        }
        #endregion

        #region ThemedKey
        public void ThemedKeyCollected()
        {
            hasThemedKey = true;
            if (themedKeyUI)
            {
                themedKeyUI.SetActive(true);
            }
            else
            {
                Debug.Log("Add the Themed Key Canvas to avoid errors!");
            }
        }

        public void EnabledThemedKeyUIContainer()
        {
            if (hasThemedKey)
            {
                DisableUIContainers();
                themedKeyContainerUI.SetActive(true);
            }
        }

        public void FillTKInventorySlot()
        {
            for (int i = 0; i < tkInventorySlots.Length; i++)
            {
                if (tkInventorySlots[i].enabled == false)
                {
                    tkInventorySlots[i].sprite = TKInventory.instance._keyList[i]._KeySprite;
                    tkInventorySlots[i].enabled = true;
                    tkInventoryBGSlots[i].enabled = true;
                    break;
                }
            }
        }

        public void ResetTKInventorySlot(int keysCollected)
        {
            tkInventorySlots[keysCollected - 1].sprite = null;
            tkInventorySlots[keysCollected - 1].enabled = false;
            tkInventoryBGSlots[keysCollected - 1].enabled = false;

            for (int i = 0; i < tkInventorySlots.Length; i++)
            {
                if (tkInventorySlots[i].enabled == true)
                {
                    tkInventorySlots[i].sprite = TKInventory.instance._keyList[i]._KeySprite;
                }
            }
        }
        #endregion

        #region Valve
        public void ValveCollected()
        {
            hasValve = true;
            if (valveUI)
            {
                valveUI.SetActive(true);
            }
            else
            {
                Debug.Log("Add the Themed Key Canvas to avoid errors!");
            }
        }

        public void EnabledValveUIContainer()
        {
            if (valveUI)
            {
                DisableUIContainers();
                valveContainerUI.SetActive(true);
            }
        }

        public void ResetProgress()
        {
            ValveProgressUI.fillAmount = 0;
            SliderOpacity(false);
        }

        public void UpdateValveProgress(float valveProgress)
        {
            ValveProgressUI.fillAmount += valveProgress;
        }

        public void SliderOpacity(bool visible)
        {
            if (visible)
            {
                sliderParentUI.alpha = 1;
            }
            else
            {
                sliderParentUI.alpha = 0;
            }
        }

        public void FillValveInventorySlot()
        {
            for (int i = 0; i < valveInventorySlots.Length; i++)
            {
                if (valveInventorySlots[i].enabled == false)
                {
                    valveInventorySlots[i].sprite = ValveInventory.instance._valvesList[i].ValveSprite;
                    valveInventorySlots[i].enabled = true;
                    valveInventoryBGSlots[i].enabled = true;
                    break;
                }
            }
        }

        public void ResetValveInventorySlot(int valvesCollected)
        {
            valveInventorySlots[valvesCollected - 1].sprite = null;
            valveInventorySlots[valvesCollected - 1].enabled = false;
            valveInventoryBGSlots[valvesCollected - 1].enabled = false;

            for (int i = 0; i < valveInventorySlots.Length; i++)
            {
                if (valveInventorySlots[i].enabled == true)
                {
                    valveInventorySlots[i].sprite = ValveInventory.instance._valvesList[i].ValveSprite;
                }
            }
        }
        #endregion

        #region Fuse
        public void FuseCollected()
        {
            hasFuse = true;
            if (fuseboxUI)
            {
                fuseboxUI.SetActive(true);
            }
            else
            {
                Debug.Log("Add the Fuse Box Canvas to avoid errors!");
            }
        }
        public void UpdateFuseCountUI(int fuses)
        {
            fuseCountUI.text = fuses.ToString("0");
            fuseIcon.color = Color.white;
        }
        #endregion

        #region Safe UI Logic
        public void ShowMainSafeUI(bool active)
        {
            safeCanvasUI.SetActive(active);
            if (active)
            {
                SetInitialSafeUI();
            }
        }

        public void SetInitialSafeUI()
        {
            acceptBtn.onClick.RemoveAllListeners();
            foreach (var btn in selectionBtn) btn.onClick.RemoveAllListeners();
            ResetSafeUI();
        }

        public void ResetEventSystem()
        {
            EventSystem.current.SetSelectedGameObject(null);
        }

        public void ResetSafeUI()
        {
            foreach (var numUI in numberUI)
            {
                numUI.text = "0";
            }
            UpdateUIState(0);
        }

        public void SetUIButtons(SafeController _myController)
        {
            acceptBtn.onClick.AddListener(_myController.CheckDialNumber);
            for (int i = 0; i < selectionBtn.Length; i++)
            {
                int index = i + 1;
                selectionBtn[i].onClick.AddListener(() => _myController.MoveDialLogic(index));
            }
        }

        public void PlayerInputCode()
        {
            playerInputNumber = string.Join("", numberUI[0].text, numberUI[1].text, numberUI[2].text);
        }

        public void UpdateNumber(int index, int lockNumber)
        {
            if (index >= 0 && index < numberUI.Length)
            {
                numberUI[index].text = lockNumber.ToString();
            }
            else
            {
                Debug.LogError("Invalid index for UpdateNumber: " + index);
            }
        }

        public void UpdateUIState(int index)
        {
            for (int i = 0; i < numberUI.Length; i++)
            {
                selectionBtn[i].interactable = (i == index);
                numberUI[i].color = (i == index) ? Color.white : Color.gray;

                ColorBlock arrowCB = selectionBtn[i].colors;
                arrowCB.normalColor = (i == index) ? Color.white : Color.gray;
                selectionBtn[i].colors = arrowCB;
            }
        }

        public void SetInteractPrompt(bool on)
        {
            AKUIManager.instance.EnableInteractPrompt(on);
        }
        #endregion

        #region Phone UI Logic
        public void SetPhoneController(PhoneController _myController)
        {
            _phoneController = _myController;
        }

        public void ShowPayPhoneCanvas(bool on)
        {
            payPhoneCanvas.SetActive(on);
            _phoneType = PhoneType.Pay;
        }

        public void ShowOfficePhoneCanvas(bool on)
        {
            officePhoneCanvas.SetActive(on);
            _phoneType = PhoneType.Office;
        }

        public void ShowMobilePhoneCanvas(bool on)
        {
            mobilePhoneCanvas.SetActive(on);
            _phoneType = PhoneType.Mobile;
        }

        public void PhoneKeyPressString(string keyString)
        {
            _phoneController.SingleBeepSound();

            if (!firstPhoneClick)
            {
                ClearPhoneInputFields();
                firstPhoneClick = true;
            }

            InputField activeInputField = GetActivePhoneInputField();
            if (activeInputField != null && activeInputField.characterLimit <= (_phoneController.inputLimit - 1))
            {
                activeInputField.characterLimit++;
                activeInputField.text += keyString;
            }
        }

        public void PhoneKeyPressCall()
        {
            _phoneController.SingleBeepSound();
            InputField activeInputField = GetActivePhoneInputField();
            if (activeInputField != null)
            {
                _phoneController.CheckCode(activeInputField);
            }
        }

        public void PhoneKeyPressClear()
        {
            _phoneController.SingleBeepSound();
            _phoneController.StopAudio();
            InputField activeInputField = GetActivePhoneInputField();
            ClearPhoneFieldData(activeInputField);
        }

        public void PhoneKeyPressClose()
        {
            _phoneController.SingleBeepSound();
            _phoneController.StopAudio();
            _phoneController.CloseKeypad();
            firstPhoneClick = false;
        }

        private void ClearPhoneInputFields()
        {
            ClearPhoneFieldData(payPhoneCodeText);
            ClearPhoneFieldData(officePhoneCodeText);
            ClearPhoneFieldData(mobilePhoneCodeText);
        }

        private void ClearPhoneFieldData(InputField inputField)
        {
            if (inputField != null)
            {
                inputField.characterLimit = 0;
                inputField.text = string.Empty;
            }
        }

        private InputField GetActivePhoneInputField()
        {
            switch (_phoneType)
            {
                case PhoneType.Pay:
                    return payPhoneCodeText;
                case PhoneType.Office:
                    return officePhoneCodeText;
                case PhoneType.Mobile:
                    return mobilePhoneCodeText;
                default:
                    return null;
            }
        }

        public void SetPhoneInteractPrompt(bool on)
        {
            EnableInteractPrompt(on);
        }
        #endregion

        #region Keypad UI Logic
        public void SetKeypadController(KeypadController _myController)
        {
            _keypadController = _myController;
        }

        public void ShowModernCanvas(bool on)
        {
            modernCanvas.SetActive(on);
            _keypadType = KeypadType.Modern;
        }

        public void ShowScifiCanvas(bool on)
        {
            scifiCanvas.SetActive(on);
            _keypadType = KeypadType.Scifi;
        }

        public void ShowKeyboardCanvas(bool on)
        {
            keyboardCanvas.SetActive(on);
            _keypadType = KeypadType.Keyboard;
        }

        public void KeypadKeyPressString(string keyString)
        {
            _keypadController.SingleBeepSound();

            if (!firstKeypadClick)
            {
                ClearKeypadInputFields();
                firstKeypadClick = true;
            }

            InputField activeInputField = GetActiveKeypadInputField();
            if (activeInputField != null && activeInputField.characterLimit <= (_keypadController.inputLimit - 1))
            {
                activeInputField.characterLimit++;
                activeInputField.text += keyString;
            }
        }

        public void KeypadKeyPressEnter()
        {
            _keypadController.SingleBeepSound();
            InputField activeInputField = GetActiveKeypadInputField();
            if (activeInputField != null)
            {
                _keypadController.CheckCode(activeInputField);
            }
        }

        public void KeypadKeyPressClear()
        {
            _keypadController.SingleBeepSound();
            InputField activeInputField = GetActiveKeypadInputField();
            ClearKeypadFieldData(activeInputField);
        }

        public void KeypadKeyPressClose()
        {
            KeypadKeyPressClear();
            _keypadController.SingleBeepSound();
            _keypadController.CloseKeypad();
        }

        private void ClearKeypadInputFields()
        {
            ClearKeypadFieldData(modernCodeText);
            ClearKeypadFieldData(scifiCodeText);
            ClearKeypadFieldData(keyboardCodeText);
        }

        private void ClearKeypadFieldData(InputField inputField)
        {
            if (inputField != null)
            {
                inputField.characterLimit = 0;
                inputField.text = string.Empty;
            }
        }

        private InputField GetActiveKeypadInputField()
        {
            switch (_keypadType)
            {
                case KeypadType.Modern:
                    return modernCodeText;
                case KeypadType.Scifi:
                    return scifiCodeText;
                case KeypadType.Keyboard:
                    return keyboardCodeText;
                default:
                    return null;
            }
        }

        public void SetKeypadInteractPrompt(bool on)
        {
            AKUIManager.instance.EnableInteractPrompt(on);
        }
        #endregion
    }
}
