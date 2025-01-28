using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace AdventurePuzzleKit.PadlockSystem
{
    public class PadlockController : MonoBehaviour
    {      
        [Header("Padlock Code")]
        [SerializeField] private string yourCombination = null;

        [Header("Interactive Padlock")]
        [SerializeField] private GameObject interactableLock = null;
       
        [Header("Prefab To Spawn")]
        [SerializeField] private GameObject padlockPrefab = null;

        [Header("Spawn Distance")]
        [SerializeField] private float distanceFromCamera = 0.3f;

        [Header("Animation Name")]
        [SerializeField] private string lockOpen = "LockOpen";

        [Header("Sounds")]
        [SerializeField] private Sound padlockInteract = null;
        [SerializeField] private Sound padlockSpin = null;
        [SerializeField] private Sound padlockUnlock = null;

        [Header("Trigger Type - ONLY if using a trigger event")]
        [SerializeField] private bool isPadlockTrigger = false;
        [SerializeField] private PadlockTrigger triggerObject = null;

        [Header("Unlock Events")]
        [SerializeField] private UnityEvent unlock = null;

        public int combinationRow1 { get; set; }
        public int combinationRow2 { get; set; }
        public int combinationRow3 { get; set; }
        public int combinationRow4 { get; set; }

        private string playerCombi;
        private bool hasUnlocked;
        private bool isShowing;
        private Camera mainCamera;
        private Animator lockAnim;
        private GameObject instantiatedPadlock;

        void Awake()
        {
            mainCamera = Camera.main;
            combinationRow1 = 1;
            combinationRow2 = 1;
            combinationRow3 = 1;
            combinationRow4 = 1;
        }

        void Update()
        {
            if (isShowing && Input.GetKeyDown(AKInputManager.instance.padlockCloseKey))
            {
                DisablePadlock();
            }
        }

        void UnlockPadlock()
        {
            unlock.Invoke();
        }

        public void ShowPadlock()
        {
            isShowing = true;
            AKDisableManager.instance.DisablePlayerDefault(true, true, false);
            SpawnPadlock(distanceFromCamera);
            mainCamera.transform.localEulerAngles = new Vector3(0, 0, 0);
            InteractSound();

            if (isPadlockTrigger)
            {
                AKUIManager.instance.EnableInteractPrompt(false);
                triggerObject.enabled = false;
            }
        }

        void SpawnPadlock(float distance)
        {
            // Instantiate the padlock prefab
            //float distanceOffset = 0.2f;
            GameObject padlockInstance = Instantiate(padlockPrefab, mainCamera.transform);

            // Set the local position and rotation of the instantiated padlock
            padlockInstance.transform.localPosition = new Vector3(0, 0, distance);
            padlockInstance.transform.localRotation = Quaternion.Euler(0, 90, 0);

            // Assign the instantiated padlock to the field
            instantiatedPadlock = padlockInstance;

            // Find the Animator component in the instantiated padlock
            lockAnim = padlockInstance.GetComponentInChildren<Animator>();

            // Get all PadlockNumberSelector components on the padlock's child objects
            PadlockNumberSelector[] numberSelectors = padlockInstance.GetComponentsInChildren<PadlockNumberSelector>();

            // Update the PadlockController reference for each PadlockNumberSelector
            foreach (PadlockNumberSelector selector in numberSelectors)
            {
                selector.UpdatePadlockController(this);
            }
        }

        void DisablePadlock()
        {
            isShowing = false;
            AKDisableManager.instance.DisablePlayerDefault(false, false, false);
            Destroy(instantiatedPadlock);

            if (isPadlockTrigger)
            {
                AKUIManager.instance.EnableInteractPrompt(true);
                triggerObject.enabled = true;
            }
        }

        public void CheckCombination()
        {
            playerCombi = combinationRow1.ToString("0") + combinationRow2.ToString("0") + combinationRow3.ToString("0") + combinationRow4.ToString("0");

            if (playerCombi == yourCombination)
            {
                if (!hasUnlocked)
                {
                    StartCoroutine(CorrectCombination());
                    hasUnlocked = true;
                }
            }
        }

        IEnumerator CorrectCombination()
        {
            lockAnim.Play(lockOpen);
            UnlockSound();

            const float waitDuration = 1.2f;
            yield return new WaitForSeconds(waitDuration);

            Destroy(instantiatedPadlock);
            interactableLock.SetActive(false);
            UnlockPadlock();

            AKDisableManager.instance.DisablePlayerDefault(false, false, false);
            gameObject.SetActive(false);
        }

        void InteractSound()
        {
            AKAudioManager.instance.Play(padlockInteract);
        }

        public void SpinSound()
        {
            AKAudioManager.instance.Play(padlockSpin);
        }

        public void UnlockSound()
        {
            AKAudioManager.instance.Play(padlockUnlock);
        }
    }
}
