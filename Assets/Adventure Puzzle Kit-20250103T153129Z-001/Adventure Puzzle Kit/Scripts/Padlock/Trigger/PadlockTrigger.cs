using UnityEngine;

namespace AdventurePuzzleKit.PadlockSystem
{
    public class PadlockTrigger : MonoBehaviour
    {
        [Header("Padlock Controller Object")]
        [SerializeField] private PadlockController padlockController = null;

        [Header("Player Tag")]
        [SerializeField] private const string playerTag = "Player";

        private bool canUse;

        private void Update()
        {
            ShowPadlockInput();
        }

        void ShowPadlockInput()
        {
            if (canUse && Input.GetKeyDown(AKInputManager.instance.triggerInteractKey))
            {
                padlockController.ShowPadlock();
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
