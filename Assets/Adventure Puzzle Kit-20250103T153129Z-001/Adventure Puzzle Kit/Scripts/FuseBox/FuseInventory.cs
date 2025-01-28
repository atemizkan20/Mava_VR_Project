using UnityEngine;
using UnityEngine.UI;

namespace AdventurePuzzleKit.FuseSystem
{
    public class FuseInventory : MonoBehaviour
    {
        [Header("Fuses in Inventory")]
        [SerializeField] private int _inventoryFuses;

        public int inventoryFuses
        {
            get { return _inventoryFuses; }
            set { _inventoryFuses = value; }
        }

        public static FuseInventory instance;

        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }
        }


        public void AddFuse()
        {
            inventoryFuses++;
            AKUIManager.instance.FuseCollected();
            AKUIManager.instance.UpdateFuseCountUI(_inventoryFuses);
        }

        public void RemoveFuse()
        {
            inventoryFuses--;
            AKUIManager.instance.UpdateFuseCountUI(_inventoryFuses);
        }
    }
}
