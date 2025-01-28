using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace AdventurePuzzleKit.PhoneSystem
{
    [System.Serializable]
    public class PhoneCodes
    {
        public string phoneCode;
        public Sound phoneClip;
    }

    public class PhoneController : MonoBehaviour
    {
        [Header("Phone UI Type")]
        [SerializeField] private PhoneType _phoneType = PhoneType.None;
        private enum PhoneType { None, Pay, Office, Mobile };

        [Header("Keypad Parameters")]
        [SerializeField] private int _inputLimit = 10;

        [Header("Phone Codes")]
        [SerializeField] private PhoneCodes[] phoneCodesList = null;

        [Header("Sound Effects")]
        [SerializeField] private Sound deadDialSound = null;
        [SerializeField] private Sound singleBeepSound = null;

        [Header("Trigger Type - ONLY if using a trigger event")]
        [SerializeField] private bool isPhoneTrigger = false;
        [SerializeField] private PhoneTrigger triggerObject = null;

        private AudioSource mainAudio;
        private bool isOpen = false;

        public int inputLimit
        {
            get { return _inputLimit; }
            set { _inputLimit = value; }
        }

        private void Awake()
        {
            mainAudio = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (isOpen && Input.GetKeyDown(AKInputManager.instance.closeKeypadKey))
            {
                CloseKeypad();
            }
        }

        public void ShowKeypad()
        {
            isOpen = true;
            AKDisableManager.instance.DisablePlayerDefault(true, true, false);
            AKUIManager.instance.SetPhoneController(this);
            SetPhoneTypeActive(true);

            if (isPhoneTrigger)
            {
                AKUIManager.instance.SetPhoneInteractPrompt(false);
                triggerObject.enabled = false;
            }
        }

        public void CloseKeypad()
        {
            isOpen = false;
            AKDisableManager.instance.DisablePlayerDefault(false, false, false);
            AKUIManager.instance.PhoneKeyPressClear();
            SetPhoneTypeActive(false);

            if (isPhoneTrigger)
            {
                AKUIManager.instance.SetPhoneInteractPrompt(true);
                triggerObject.enabled = true;
            }
        }

        void SetPhoneTypeActive(bool on)
        {
            switch (_phoneType)
            {
                case PhoneType.Pay:
                    AKUIManager.instance.ShowPayPhoneCanvas(on);
                    break;
                case PhoneType.Office:
                    AKUIManager.instance.ShowOfficePhoneCanvas(on);
                    break;
                case PhoneType.Mobile:
                    AKUIManager.instance.ShowMobilePhoneCanvas(on);
                    break;
            }
        }

        public void CheckCode(InputField numberInputField)
        {
            StopAudio();
            var code = phoneCodesList.FirstOrDefault(x => x.phoneCode == numberInputField.text);
            if (code != null)
            {
                AKAudioManager.instance.Play(code.phoneClip);
            }
            else
            {
                DeadDialSound();
            }
        }

        public void SingleBeepSound()
        {
            AKAudioManager.instance.Play(singleBeepSound);
        }

        void DeadDialSound()
        {
            AKAudioManager.instance.Play(deadDialSound);
        }

        void StopDeadDialSound()
        {
            AKAudioManager.instance.StopPlaying(deadDialSound);
        }

        public void StopAudio()
        {
            AKAudioManager.instance.StopAll();
            StopDeadDialSound();
        }
    }
}
