using UnityEngine;

namespace AdventurePuzzleKit.ThemedKey
{
    public class TKItem : MonoBehaviour
    {
        [Header("Item Type")]
        [SerializeField] private ItemType _itemType = ItemType.None;
        private enum ItemType { None, Door, Key }

        private TKKeyCollectable keyController;
        private TKDoorInteractable doorController;

        private void Awake()
        {
            switch (_itemType)
            {
                case ItemType.Door:      
                    doorController = GetComponent<TKDoorInteractable>();
                    break;
                case ItemType.Key:
                    keyController = GetComponent<TKKeyCollectable>();
                    break;
            }
        }

        public void ObjectInteract()
        {
            switch (_itemType)
            {
                case ItemType.Door:
                    doorController.CheckDoor();
                    break;
                case ItemType.Key:
                    keyController.KeyPickup();
                    break;
            }
        }
    }
}
