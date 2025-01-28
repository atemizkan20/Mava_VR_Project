using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AdventurePuzzleKit.KeypadSystem
{
    [System.Serializable]
    public class KeypadCodes
    {
        public string keypadCode;
        [Space(10)]
        public UnityEvent keypadEvent;
    }

    public class KeypadController : MonoBehaviour
    {
        [Header("Keypad Type")]
        [SerializeField] private KeypadType _keypadType = KeypadType.None;
        private enum KeypadType { None, Modern, Scifi, Keyboard };

        [Header("Character Limit")]
        [SerializeField] private int _inputLimit = 10;

        [Header("Code List")]
        [SerializeField] private KeypadCodes[] keypadCodesList = null;

        [Header("Keypad Sounds")]
        [SerializeField] private Sound keypadBeep = null;
        [SerializeField] private Sound keypadDenied = null;

        [Header("Trigger Event")]
        [SerializeField] private bool isTriggerEvent = false;
        [SerializeField] private KeypadTrigger triggerObject = null;
        private bool isOpen = false;

        public int inputLimit
        {
            get { return _inputLimit; }
            set { _inputLimit = value; }
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
            AKUIManager.instance.SetKeypadController(this);
            SetKeypadTypeActive(true);

            if (isTriggerEvent)
            {
                AKUIManager.instance.SetKeypadInteractPrompt(false);
                triggerObject.enabled = false;
            }
        }

        public void CloseKeypad()
        {
            isOpen = false;
            AKDisableManager.instance.DisablePlayerDefault(false, false, false);
            AKUIManager.instance.KeypadKeyPressClear();
            SetKeypadTypeActive(false);

            if (isTriggerEvent)
            {
                AKUIManager.instance.SetKeypadInteractPrompt(true);
                triggerObject.enabled = true;
            }
        }

        void SetKeypadTypeActive(bool on)
        {
            switch (_keypadType)
            {
                case KeypadType.Modern:
                    AKUIManager.instance.ShowModernCanvas(on);
                    break;
                case KeypadType.Scifi:
                    AKUIManager.instance.ShowScifiCanvas(on);
                    break;
                case KeypadType.Keyboard:
                    AKUIManager.instance.ShowKeyboardCanvas(on);
                    break;
            }
        }

        public void CheckCode(InputField numberInputField)
        {
            var code = keypadCodesList.FirstOrDefault(x => x.keypadCode == numberInputField.text);
            if (code != null)
            {
                code.keypadEvent.Invoke();
            }
            else
            {
                KeyPadDeniedSound();
            }
        }

        public void SingleBeepSound()
        {
            AKAudioManager.instance.Play(keypadBeep);
        }

        public void KeyPadDeniedSound()
        {
            AKAudioManager.instance.Play(keypadDenied);
        }
    }
}
