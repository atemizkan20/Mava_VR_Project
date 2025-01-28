using UnityEngine;

namespace AdventurePuzzleKit.GeneratorSystem
{
    public class GeneratorInventory : MonoBehaviour
    {
        [Header("Jerry can OnStart?")]
        [SerializeField] private bool _hasJerrycan;

        [Header("Fuel Levels")]
        [SerializeField] private float _currentInvFuel = 0;
        [SerializeField] private float _maximumInvFuel = 100;

        public float currentInvFuel
        {
            get { return _currentInvFuel; }
            set { _currentInvFuel = Mathf.Min(value, _maximumInvFuel); }
        }

        public float maximumInvFuel
        {
            get { return _maximumInvFuel; }
            set { _maximumInvFuel = value; }
        }

        public bool hasJerrycan
        {
            get { return _hasJerrycan; }
            set { _hasJerrycan = value; }
        }

        public static GeneratorInventory instance;

        void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }            
        }
        private void Start()
        {
            SetFuelAmounts(true, 0);
        }

        public void CollectedJerrycan(bool shouldAdd, float fuelAmount)
        {
            hasJerrycan = true;
            AKUIManager.instance.JerrycanCollected();
            SetFuelAmounts(shouldAdd, fuelAmount);
        }

        public void SetFuelAmounts(bool shouldAdd, float fuelAmount)
        {
            if (shouldAdd)
            {
                currentInvFuel += fuelAmount;
            }
            else
            {
                currentInvFuel = fuelAmount;
            }

            AKUIManager.instance.UpdateInventoryUI(currentInvFuel, maximumInvFuel);
        }
    }
}
