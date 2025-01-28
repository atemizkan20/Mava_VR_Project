using UnityEngine;

namespace AdventurePuzzleKit.ValveSystem
{
    public class ValveItem : MonoBehaviour
    {
        [Space(10)]
        [SerializeField] private ItemType _itemType = ItemType.None;
        private enum ItemType { None, Valve, Slot, Wheel }

        private ValveCollectable _valveCollectable;
        private ValveSlot _valveSlot;
        private ValveWheelInteractable _wheelInteractable;

        private void Awake()
        {
            switch (_itemType)
            {
                case ItemType.Valve: _valveCollectable = GetComponent<ValveCollectable>(); break;
                case ItemType.Slot: _valveSlot = GetComponent<ValveSlot>(); break;
                //case ItemType.Wheel: _wheelInteractable = GetComponent<ValveWheelInteractable>(); break;
            }
        }

        public void ObjectInteract()
        {
            switch (_itemType)
            {
                case ItemType.Valve: _valveCollectable.ValvePickup(); break;
                case ItemType.Slot: _valveSlot.CheckValveSlot(); break;
                //case ItemType.Wheel: _wheelInteractable._CanTurn = isLooking; break;
            }   
        }
    }
}
