using UnityEngine;

namespace AdventurePuzzleKit.ChessSystem
{
    public class CPItem : MonoBehaviour
    {
        [SerializeField] private ItemType _itemType = ItemType.None;
        private enum ItemType { None, ChessFuse, Fusebox }

        private CPFuseCollectable _fuseCollectable;
        private CPFuseBoxInteractable _fuseboxInteractable;

        private void Awake()
        {
            switch (_itemType)
            {
                case ItemType.ChessFuse:
                    _fuseCollectable = GetComponent<CPFuseCollectable>();
                    break;
                case ItemType.Fusebox:
                    _fuseboxInteractable = GetComponent<CPFuseBoxInteractable>();
                    break;
            }
        }

        public void ObjectInteract()
        {
            switch (_itemType)
            {
                case ItemType.ChessFuse:
                    _fuseCollectable.PickupChessPiece();
                    break;
                case ItemType.Fusebox:
                    _fuseboxInteractable.InteractFuseBox();
                    break;
            }
        }
    }
}
