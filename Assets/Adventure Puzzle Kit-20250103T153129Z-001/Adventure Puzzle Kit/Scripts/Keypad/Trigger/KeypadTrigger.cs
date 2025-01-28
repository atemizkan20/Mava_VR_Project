using UnityEngine;

namespace AdventurePuzzleKit.KeypadSystem
{
    public class KeypadTrigger : MonoBehaviour
    {
        [Header("Keypad Object")]
        [SerializeField] private KeypadItem myKeypad = null;

        [Header("Player Tag")]
        [SerializeField] private const string playerTag = "Player";

        private bool canUse;

        private void Update()
        {
            ShowKeypadInput();
        }

        private void ShowKeypadInput()
        {
            if (canUse && Input.GetKeyDown(AKInputManager.instance.triggerInteractKey))
            {
                myKeypad.ShowKeypad();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(playerTag))
            {
                canUse = true;
                AKUIManager.instance.EnableInteractPrompt(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(playerTag))
            {
                canUse = false;
                AKUIManager.instance.EnableInteractPrompt(false);
            }
        }
    }
}
