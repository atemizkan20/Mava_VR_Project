using UnityEngine;

namespace AdventurePuzzleKit.GasMaskSystem
{
    public class GasMaskItem : MonoBehaviour
    {
        private enum ItemType { None, GasMask, Filter }
        [SerializeField] private ItemType _itemType = ItemType.None;

        public void ObjectInteract()
        {
            switch (_itemType)
            {
                case ItemType.GasMask:
                    GasMaskController.instance.PickupGasMask();
                    break;

                case ItemType.Filter:
                    GasMaskController.instance.PickupFilter();
                    break;
            }
            gameObject.SetActive(false);
        }
    }
}
