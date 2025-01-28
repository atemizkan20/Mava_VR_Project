using UnityEngine;

namespace AdventurePuzzleKit.PhoneSystem
{
    public class PhoneTrigger : MonoBehaviour
    {
        [Header("Phone Model")]
        [SerializeField] private PhoneItem phoneModelObject = null;

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
                phoneModelObject.ShowKeypad();
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
