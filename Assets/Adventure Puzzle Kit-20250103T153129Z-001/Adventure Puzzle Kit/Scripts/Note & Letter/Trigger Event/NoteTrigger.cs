using UnityEngine;

namespace AdventurePuzzleKit.NoteSystem
{
    public class NoteTrigger : MonoBehaviour
    {
        [Header("Add Note Controller here")]
        [SerializeField] private NoteTypeSelector myNote = null;

        [Header("Tag that is used for detection")]
        [SerializeField] private string playerTag = "Player";

        private bool canUse;

        private void Update()
        {
            ShowNoteInput();
        }

        private void ShowNoteInput()
        {
            if (canUse && Input.GetKeyDown(AKInputManager.instance.triggerInteractKey))
            {
                myNote.DisplayNotes();
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
