using UnityEngine;

namespace AdventurePuzzleKit
{
    public class AKInputManager : MonoBehaviour
    {
        [Header("Raycast / First Person Pickup Inputs")]
        public KeyCode pickupKey;

        [Header("Open Inventory Input")]
        public KeyCode toggleInventoryKey;
        public KeyCode closeInventoryKey;

        [Header("Padlock Input")]
        public KeyCode padlockCloseKey;

        [Header("Keypad Trigger Input")]
        public KeyCode closeKeypadKey;
        public KeyCode triggerInteractKey;

        [Header("GasMask System Inputs")]
        public KeyCode equipMaskKey;
        public KeyCode replaceFilterKey;

        [Header("Note System Inputs")]
        public KeyCode closeNoteKey;
        public KeyCode reverseNoteKey;
        public KeyCode playAudioKey;

        [Header("Examine System Inputs")]
        public KeyCode interactKey;
        public KeyCode rotateKey;
        public KeyCode dropKey;
        public KeyCode pickupItemKey;

        [Header("Flashlight System Inputs")]
        public KeyCode flashlightSwitch;
        public KeyCode reloadBattery;

        public static AKInputManager instance;

        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }
        }
    }
}
