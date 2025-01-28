using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace AdventurePuzzleKit.SafeSystem
{
    public class SafeController : MonoBehaviour
    {
        [Header("Safe Model Reference")]
        [SerializeField] private GameObject safeModel = null;
        [SerializeField] private Transform safeDial = null;

        [Header("Animation References")]
        [SerializeField] private string safeAnimationName = "SafeDoorOpen";

        [Header("Animation Timers")]
        [SerializeField] private float beforeAnimationStart = 1.0f; //Default: 1.0f
        [SerializeField] private float beforeOpenDoor = 0.5f; //Default: 0.5

        [Header("Safe Solution: 0-15")]
        [Range(0, 15)] [SerializeField] private int safeSolutionNum1 = 0;
        [Range(0, 15)] [SerializeField] private int safeSolutionNum2 = 0;
        [Range(0, 15)] [SerializeField] private int safeSolutionNum3 = 0;

        [Header("Trigger Interaction?")]
        [SerializeField] private bool isTriggerInteraction = false;
        [SerializeField] private GameObject triggerObject = null;

        [Header("Audio ScriptableObjects")]
        [SerializeField] private SafeAudioClips _safeAudioClips = null;

        [Header("Unity Event - What happens when you open the safe?")]
        [SerializeField] private UnityEvent safeOpened = null;

        private int lockState;
        private bool canClose = false;
        private bool isInteracting = false;
        private Animator safeAnim;
        private int currentLockNumber;

        private void Start()
        {
            safeAnim = safeModel.gameObject.GetComponent<Animator>();
        }

        public void ShowSafeUI()
        {
            Debug.Log("Safe UI opened (VR style).");
            // Possibly set a bool "isInteracting = true;" if you want
        }

        private void Update()
        {
            if (!canClose && isInteracting && Input.GetKeyDown(AKInputManager.instance.padlockCloseKey))
            {
                CloseSafeUI();
            }
        }

        private void CloseSafeUI()
        {
            if (isTriggerInteraction)
            {
                canClose = true;
                isInteracting = false;
                triggerObject.SetActive(true);
                AKUIManager.instance.SetInteractPrompt(true);
            }

            AKDisableManager.instance.DisablePlayerDefault(false, false, false);
            ResetSafeDial(false);
            AKUIManager.instance.ShowMainSafeUI(false);
            isInteracting = false;
        }

        void ResetSafeDial(bool hasComplete)
        {
            if (!hasComplete)
            {
                PlayRattleSound();
            }

            lockState = 1;
            AKUIManager.instance.ResetSafeUI();
            safeDial.transform.localEulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
            currentLockNumber = 0;
        }

        private IEnumerator CheckCode()
        {
            AKUIManager.instance.PlayerInputCode();
            string safeSolution = $"{safeSolutionNum1}{safeSolutionNum2}{safeSolutionNum3}";

            if (AKUIManager.instance.playerInputNumber == safeSolution)
            {
                AKDisableManager.instance.DisablePlayerDefault(false, false, false);
                AKUIManager.instance.ShowMainSafeUI(false);
                isInteracting = false;
                safeModel.tag = "Untagged";

                PlayBoltUnlockSound();
                yield return new WaitForSeconds(beforeAnimationStart);
                safeAnim.Play(safeAnimationName, 0, 0.0f);
                PlayHandleSpinSound();
                yield return new WaitForSeconds(beforeOpenDoor);
                PlayDoorOpenSound();

                // Disable the BoxCollider of the safe to allow item interaction
                BoxCollider boxCollider = safeModel.GetComponent<BoxCollider>();
                if (boxCollider != null)
                {
                    boxCollider.enabled = false;
                }

                if (isTriggerInteraction)
                {
                    canClose = true;
                    triggerObject.SetActive(false);
                }

                ResetSafeDial(true);
                safeOpened.Invoke();
            }
            else
            {
                ResetSafeDial(false);
            }
        }

        public void CheckDialNumber()
        {
            AKUIManager.instance.ResetEventSystem();
            PlayInteractSound();

            if (lockState < 3)
            {
                AKUIManager.instance.UpdateUIState(lockState);
                lockState++;
            }
            else
            {
                AKUIManager.instance.UpdateUIState(3);
                StartCoroutine(CheckCode());
                lockState = 1;
            }
        }

        public void MoveDialLogic(int lockNumberSelection)
        {
            AKUIManager.instance.ResetEventSystem();
            PlaySafeClickSound();

            if (lockNumberSelection == 1 || lockNumberSelection == 3)
            {
                currentLockNumber = (currentLockNumber + 1) % 16;
                RotateDial(false);
            }
            else if (lockNumberSelection == 2)
            {
                currentLockNumber = (currentLockNumber + 15) % 16;
                RotateDial(true);
            }

            AKUIManager.instance.UpdateNumber(lockNumberSelection - 1, currentLockNumber);
        }

        void RotateDial(bool positive)
        {
            if (positive)
            {
                safeDial.transform.Rotate(0.0f, 0.0f, 22.5f, Space.Self);
            }
            else
            {
                safeDial.transform.Rotate(0.0f, 0.0f, -22.5f, Space.Self);
            }
        }

        void PlayInteractSound()
        {
            _safeAudioClips.PlayInteractSound();
        }

        void PlayBoltUnlockSound()
        {
            _safeAudioClips.PlayBoltUnlockSound();
        }

        void PlayHandleSpinSound()
        {
            _safeAudioClips.PlayHandleSpinSound();
        }

        void PlayDoorOpenSound()
        {
            _safeAudioClips.PlayDoorOpenSound();
        }

        void PlayRattleSound()
        {
            _safeAudioClips.PlayRattleSound();
        }

        void PlaySafeClickSound()
        {
            _safeAudioClips.PlaySafeClickSound();
        }
    }
}