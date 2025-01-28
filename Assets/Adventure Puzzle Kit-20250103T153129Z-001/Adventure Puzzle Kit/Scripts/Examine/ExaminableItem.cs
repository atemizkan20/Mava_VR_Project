using UnityEngine;
using System.Collections;
using AdventurePuzzleKit.FlashlightSystem;
using AdventurePuzzleKit.GeneratorSystem;
using AdventurePuzzleKit.GasMaskSystem;
using AdventurePuzzleKit.ThemedKey;
using AdventurePuzzleKit.ChessSystem;
using AdventurePuzzleKit.FuseSystem;
using AdventurePuzzleKit.ValveSystem;

namespace AdventurePuzzleKit.ExamineSystem
{
    public class ExaminableItem : MonoBehaviour
    {
        #region Parent / Child Fields
        [Tooltip("Select this option if the object which has this script has no mesh renderer and it's an empty parent which holds children")]
        [SerializeField] private bool isEmptyParent = false;

        [Tooltip("Select this option if the object you're examining has multiple children - Add the child objects to the array (Don't add inspect point)")]
        [SerializeField] private bool _hasChildren = false;
        [SerializeField] private GameObject[] childObjects = null;
        #endregion

        #region Offset Fields
        [SerializeField] private Vector3 initialRotationOffset = new Vector3(0, 0, 0);
        [Tooltip("Horizontal and Vertical offsets control how far vertically and horizentally the object is positioned when examined. Keep this a low value (-0.X) is left and (+0.X) is right")]
        [Range(-1, 1)] [SerializeField] private float horizontalOffset = 0;
        [Range(-1, 1)] [SerializeField] private float verticalOffset = 0;
        #endregion

        #region Zoom Fields
        [Tooltip("Creates a smooth pickup: Higher the value the longer it will take to lerp the item in front of the camera. Setting to 0 means it will appear like the legacy examine")]
        [SerializeField] private float smoothExamineSpeed = 0.2f;
        [Tooltip("The higher this value, the more zoomed out the item will look")]
        [SerializeField] private float initialZoom = 1f;
        [SerializeField] private Vector2 zoomRange = new Vector2(0.5f, 2f);
        [SerializeField] private float zoomSensitivity = 0.1f;
        #endregion

        #region Rotation Fields
        [SerializeField] private float rotationSpeed = 5.0f;
        [SerializeField] private bool invertRotation = false;
        #endregion

        #region Highlight Fields
        [SerializeField] private bool showEmissionHighlight = false;
        [SerializeField] private bool showNameHighlight = false;
        #endregion

        #region InspectPoint Fields
        [SerializeField] bool _hasInspectPoints = false;
        [SerializeField] private GameObject[] inspectPoints = null;
        private LayerMask inspectPointLayer;
        private float viewDistance = 25;
        private bool disableInspectInput = false;
        #endregion

        #region Sound Fields
        [SerializeField] private Sound pickupSound = null;
        [SerializeField] private Sound dropSound = null;
        #endregion

        #region Text Customisation Fields
        [SerializeField] private UIType _UIType = UIType.None;
        [SerializeField] private enum UIType { None, BasicLowerUI, RightSideUI }

        [SerializeField] private string itemName = null;

        [SerializeField] private int textSize = 32;
        [SerializeField] private Font fontType = null;
        [SerializeField] private FontStyle fontStyle = FontStyle.Normal;
        [SerializeField] private Color fontColor = Color.white;

        [SerializeField] [TextArea] private string itemDescription = null;

        [SerializeField] private int textSizeDesc = 30;
        [SerializeField] private Font fontTypeDesc = null;
        [SerializeField] private FontStyle fontStyleDesc = FontStyle.Normal;
        [SerializeField] private Color fontColorDesc = Color.white;
        #endregion

        #region Initialisation Fields
        private Material objectMaterial;
        Vector3 originalPosition;
        Quaternion originalRotation;
        private Vector3 startPos;
        private bool canRotate;
        private float currentZoom = 1;
        private Camera mainCamera;
        private Transform examinePoint = null;
        private AKInteractor raycastManager;
        private AKUIManager akUIManager;
        private BoxCollider boxCollider;
        #endregion

        #region String Field References
        private const string emissive = "_EMISSION";
        private const string mouseX = "Mouse X";
        private const string mouseY = "Mouse Y";

        private const string examineLayer = "ExamineLayer";
        private const string defaultLayer = "Default";
        private const string inspectPointTag = "InspectPoint";
        private const string inspectLayer = "InspectPointLayer";
        #endregion

        #region Collectable Fields
        [SerializeField] private bool _isCollectable = false;
        [SerializeField] private SystemType _systemType = SystemType.None;
        private enum SystemType { None, FlashlightSys, GeneratorSys, GasMaskSys, ThemedKeySys, ChessSys, FuseBoxSys, ValveSys }

        private FlashlightItem _flashlightItemController;
        private GeneratorItem _generatorItem;
        private GasMaskItem _gasMaskItem;
        private TKItem _themedKeyItem;
        private CPItem _chessItemController;
        private FuseItem _fuseboxItem;
        private ValveItem _valveItem;
        #endregion

        #region Public Properties
        public bool hasChildren
        {
            get { return _hasChildren; }
            set { _hasChildren = value; }
        }

        public bool hasInspectPoints
        {
            get { return _hasInspectPoints; }
            set { _hasInspectPoints = value; }
        }

        public bool isCollectable
        {
            get { return _isCollectable; }
            set { _isCollectable = value; }
        }
        #endregion

        void Start()
        {
            inspectPointLayer = 1 << LayerMask.NameToLayer(inspectLayer); //This finds the mask we want and adds it to the variable "inspectPointLayer"

            initialZoom = Mathf.Clamp(initialZoom, zoomRange.x, zoomRange.y);
            originalPosition = transform.position; 
            originalRotation = transform.rotation;
            startPos = gameObject.transform.localEulerAngles;

            DisableEmissionOnChildrenHighlight(true);

            if (!isEmptyParent)
            {
                objectMaterial = GetComponent<Renderer>().material;
                DisableMatEmissive(true);
            }

            if (isCollectable)
            {
                SetType();
            }

            boxCollider = GetComponent<BoxCollider>();
            mainCamera = Camera.main;
            raycastManager = mainCamera.GetComponent<AKInteractor>();
            examinePoint = GameObject.FindWithTag("ExaminePoint").GetComponent<Transform>();
            CheckSoundDebug();
        }

        void SetExamineLayer(string currentLayer)
        {
            gameObject.layer = LayerMask.NameToLayer(currentLayer);
        }

        void Update()
        {
            if (canRotate)
            {
                ExamineInput();
                ExamineZooming();
            }
        }

        void ExamineInput()
        {
            float h = invertRotation ? rotationSpeed * Input.GetAxis(mouseX) : -rotationSpeed * Input.GetAxis(mouseX);
            float v = invertRotation ? rotationSpeed * Input.GetAxis(mouseY) : -rotationSpeed * Input.GetAxis(mouseY);

            if (hasInspectPoints)
            {
                FindInspectPoints();
            }

            if (Input.GetKey(AKInputManager.instance.rotateKey))
            {
                gameObject.transform.Rotate(v, h, 0);
            }

            else if (Input.GetKeyDown(AKInputManager.instance.dropKey))
            {
                DropObject(true);
            }

            else if (Input.GetKeyDown(AKInputManager.instance.pickupItemKey))
            {
                if (isCollectable)
                {
                    CollectItem();
                }
            }
        }

        void ExamineZooming()
        {
            bool zoomAdjusted = false;
            float scrollDelta = Input.mouseScrollDelta.y;
            if (scrollDelta > 0)
            {
                currentZoom += zoomSensitivity;
                zoomAdjusted = true;
            }
            else if (scrollDelta < 0)
            {
                currentZoom -= zoomSensitivity;
                zoomAdjusted = true;
            }

            if (zoomAdjusted)
            {
                currentZoom = Mathf.Clamp(currentZoom, zoomRange.x, zoomRange.y);
                ItemZoom(currentZoom);
            }
        }

        public void ExamineObject()
        {
            AKUIManager.instance._examinableItem = this;
            akUIManager = AKUIManager.instance;

            boxCollider.enabled = false;

            EnableInspectPoints();
            AKDisableManager.instance.DisablePlayerDefault(true, true, true);
            akUIManager.SetHighlightName(null, false, false);
            SetExamineLayer(examineLayer);
            PlayPickupSound();

            currentZoom = initialZoom;
            ItemZoom(currentZoom, false);

            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
            transform.Rotate(initialRotationOffset);

            DisableEmissionOnChildrenLayer(examineLayer);
            DisableMatEmissive(true);
            canRotate = true;

            akUIManager.ShowHelpPrompt(true);

            StartCoroutine(MoveToPosition(transform, examinePoint.transform.position, smoothExamineSpeed));

            switch (_UIType)
            {
                case UIType.None:
                    akUIManager.ShowCloseButton(true);
                    break;
                case UIType.BasicLowerUI:
                    akUIManager.SetBasicUIText(itemName, itemDescription, true);
                    TextCustomisation();
                    break;
                case UIType.RightSideUI:
                    akUIManager.SetRightSideUIText(itemName, itemDescription, true);
                    TextCustomisation();
                    break;
            }
        }

        public void DropObject(bool shouldLerp)
        {
            AKDisableManager.instance.DisablePlayerDefault(false, false, true);

            boxCollider.enabled = true;

            if (shouldLerp)
            {
                StartCoroutine(MoveToPosition(transform, originalPosition, smoothExamineSpeed));
            }
            transform.rotation = originalRotation;

            SetExamineLayer(defaultLayer);
            PlayDropSound();

            InspectPointUI(null, null, false);
            DisableEmissionOnChildrenLayer(defaultLayer);
            DisableInspectPoints();
            hasInspectPoints = false;
            canRotate = false;

            currentZoom = initialZoom;
            ItemZoom(currentZoom, false);

            if (hasChildren)
            {
                foreach (GameObject gameobjectToLayer in childObjects)
                {
                    gameobjectToLayer.layer = LayerMask.NameToLayer(defaultLayer);
                    Material thisMat = gameobjectToLayer.GetComponent<Renderer>().material;
                    thisMat.DisableKeyword(emissive);
                }
            }

            akUIManager.ShowHelpPrompt(false);

            switch (_UIType)
            {
                case UIType.None:
                    akUIManager.ShowCloseButton(false);
                    break;
                case UIType.BasicLowerUI:
                    akUIManager.SetBasicUIText(null, null, false);
                    break;
                case UIType.RightSideUI:
                    akUIManager.SetRightSideUIText(null, null, false);
                    break;
            }
        }

        //value = The distance from the camera to position the object
        //MoveSelf = Whether to move the actual object. If set to false the object may not move, but only the represented point
        private void ItemZoom(float value, bool moveSelf = true)
        {
            examinePoint.transform.localPosition = new Vector3(horizontalOffset, verticalOffset, value);

            if (moveSelf)
            {
                transform.position = examinePoint.transform.position;
            }
        }

        IEnumerator MoveToPosition(Transform target, Vector3 destination, float duration)
        {
            float elapsedTime = 0;
            Vector3 startingPos = target.position;
            while (elapsedTime < duration)
            {
                target.position = Vector3.Lerp(startingPos, destination, (elapsedTime / duration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            target.position = destination;
        }

        public void ItemHighlight(bool isHighlighted)
        {
            if (showNameHighlight)
            {
                if (isHighlighted)
                {
                    AKUIManager.instance.SetHighlightName(itemName, isHighlighted, true);
                }
                else
                {
                    AKUIManager.instance.SetHighlightName(null, false, false);
                }
            }

            if (showEmissionHighlight)
            {
                if (isHighlighted)
                {
                    DisableMatEmissive(false);
                    DisableEmissionOnChildrenHighlight(false);
                }
                else
                {
                    DisableMatEmissive(true);
                    DisableEmissionOnChildrenHighlight(true);
                }
            }
        }

        private void TextCustomisation()
        {
            switch (_UIType)
            {
                case UIType.BasicLowerUI:
                    akUIManager.SetBasicUITextSettings(textSize, fontType, fontStyle, fontColor, textSizeDesc, fontTypeDesc, fontStyleDesc, fontColorDesc);
                    break;
                case UIType.RightSideUI:
                    akUIManager.SetRightUITextSettings(textSize, fontType, fontStyle, fontColor, textSizeDesc, fontTypeDesc, fontStyleDesc, fontColorDesc);
                    break;
            }         
        }

        void FindInspectPoints()
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, viewDistance/*, inspectPointLayer*/))
            {
                if (hit.transform.CompareTag(inspectPointTag))
                {
                    InspectPointUI(hit.transform.gameObject, mainCamera, true); //Enable inspect point UI
                    if (Input.GetKeyDown(AKInputManager.instance.interactKey) && !disableInspectInput)
                    {
                        StartCoroutine(NextIspectPointTimer(1f));
                        hit.transform.gameObject.GetComponent<ExamineInspectPoint>().InspectPointInteract();
                    }
                }
                else
                {
                    InspectPointUI(null, null, false); //Disable inspect point UI
                }
            }
            else
            {
                InspectPointUI(null, null, false); //Disable inspect point UI
            }
        }

        void InspectPointUI(GameObject item, Camera camera, bool detected) // Enable/disable inspect point UI
        {
            if (detected)
            {
                Vector3 setPosition = camera.WorldToScreenPoint(item.transform.position);
                akUIManager.SetInspectPointParent(true, setPosition);

                string inspectText = item.GetComponent<ExamineInspectPoint>().InspectInformation();
                akUIManager.SetInspectPointText(inspectText);
            }
            else
            {
                akUIManager.SetInspectPointParent(false, Vector3.zero); //Disable inspect UI - Doesn't need Vector3 ?
            }
        }

        IEnumerator NextIspectPointTimer(float waitTime)
        {
            disableInspectInput = true;
            yield return new WaitForSeconds(waitTime);
            disableInspectInput = false;
        }

        void EnableInspectPoints()
        {
            StartCoroutine(WaitBeforeEnable(0.1f));
        }

        IEnumerator WaitBeforeEnable(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            if (inspectPoints.Length >= 1)
            {
                hasInspectPoints = true;

                foreach (GameObject pointToEnable in inspectPoints)
                {
                    pointToEnable.SetActive(true);
                }
            }
        }

        void DisableInspectPoints()
        {
            if (hasInspectPoints)
            {
                foreach (GameObject pointToEnable in inspectPoints)
                {
                    pointToEnable.SetActive(false);
                }
            }
        }

        void DisableMatEmissive(bool disable)
        {
            if (!isEmptyParent && disable)
            {
                objectMaterial.DisableKeyword(emissive);
            }
            else if (!isEmptyParent && !disable)
            {
                objectMaterial.EnableKeyword(emissive);
            }
        }

        void DisableEmissionOnChildrenHighlight(bool enable)
        {
            if (hasChildren)
            {
                foreach (GameObject gameobjectToLayer in childObjects)
                {
                    Material thisMat = gameobjectToLayer.GetComponent<Renderer>().material;
                    if (!enable)
                    {
                        thisMat.EnableKeyword(emissive);
                    }
                    else
                    {
                        thisMat.DisableKeyword(emissive);
                    }
                }
            }
        }

        void DisableEmissionOnChildrenLayer(string layerToSet)
        {
            if (hasChildren)
            {
                foreach (GameObject gameobjectToLayer in childObjects)
                {
                    gameobjectToLayer.layer = LayerMask.NameToLayer(layerToSet);
                    Material thisMat = gameobjectToLayer.GetComponent<Renderer>().material;
                    thisMat.DisableKeyword(emissive);
                }
            }
        }

        void SetType()
        {
            switch (_systemType)
            {
                case SystemType.FlashlightSys: _flashlightItemController = GetComponent<FlashlightItem>(); break;
                case SystemType.GeneratorSys: _generatorItem = GetComponent<GeneratorItem>(); break;
                case SystemType.GasMaskSys: _gasMaskItem = GetComponent<GasMaskItem>(); break;
                case SystemType.ThemedKeySys: _themedKeyItem = GetComponent<TKItem>(); break;
                case SystemType.ChessSys: _chessItemController = GetComponent<CPItem>(); break;
                case SystemType.FuseBoxSys: _fuseboxItem = GetComponent<FuseItem>(); break;
                case SystemType.ValveSys: _valveItem = GetComponent<ValveItem>(); break;
            }
        }

        void CollectItem()
        {
            switch (_systemType)
            {
                case SystemType.FlashlightSys: _flashlightItemController.ObjectInteract(); break;
                case SystemType.GeneratorSys: _generatorItem.ObjectInteract(); break;
                case SystemType.GasMaskSys: _gasMaskItem.ObjectInteract(); break;
                case SystemType.ThemedKeySys: _themedKeyItem.ObjectInteract(); break;
                case SystemType.ChessSys: _chessItemController.ObjectInteract(); break;
                case SystemType.FuseBoxSys: _fuseboxItem.ObjectInteract(); break;
                case SystemType.ValveSys: _valveItem.ObjectInteract(); break;
            }
            DropObject(false);
        }

        void PlayPickupSound()
        {
            if (pickupSound != null)
            {
                AKAudioManager.instance.Play(pickupSound);
            }
        }

        void PlayDropSound()
        {
            if (dropSound != null)
            {
                AKAudioManager.instance.Play(dropSound);
            }
        }

        private void OnDestroy()
        {
            Destroy(objectMaterial);
        }

        private void CheckSoundDebug()
        {
            if (pickupSound == null)
            {
                print("Did you forget to add a sound Scriptable to item" + " " + gameObject);
            }

            else if (dropSound == null)
            {
                print("Did you forget to add a sound Scriptable to item" + " " + gameObject);
            }
        }
    }
}