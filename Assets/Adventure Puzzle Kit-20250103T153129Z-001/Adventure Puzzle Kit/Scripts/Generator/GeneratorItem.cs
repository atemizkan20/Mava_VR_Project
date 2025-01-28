using UnityEngine;
using UnityEngine.Events;

namespace AdventurePuzzleKit.GeneratorSystem
{
    public class GeneratorItem : MonoBehaviour
    {
        //Item Type
        public GeneratorItemType itemType;
        public enum GeneratorItemType { None, Jerrycan, Generator, FuelBarrel }

        //Use Fuel Overtime
        [SerializeField] private bool _canBurnFuel;
        [SerializeField] private float burnRate = 1;

        //Rumble Settings
        [SerializeField] private bool _canRumble = false;
        [SerializeField] private float rumbleSpeed = 5.0f;
        [SerializeField] private float rumbleIntensity = 0.01f;

        //Fuel Parameters
        [Range(0, 1000)] [SerializeField] private float _itemFuelAmount = 100.0f;
        [Range(0, 1000)] [SerializeField] private float _itemMaxFuelAmount = 500.0f;      

        //Object Canvas Settings
        [SerializeField] private bool _showUI = true;
        [SerializeField] private PopoutUI _popoutUI = null;

        [SerializeField] private Sound fuelSwishSound = null;
        [SerializeField] private Sound waterPourSound = null;

        [SerializeField] private UnityEvent activateGenerator = null;
        [SerializeField] private UnityEvent deactivateGenerator = null;

        private bool isGenFull;
        private bool rumbling;
        private float fuelRequired;
        private Vector3 generatorOrigin;
        private GeneratorInventory _generatorInventory;

        public float itemFuelAmount
        {
            get { return _itemFuelAmount; }
            set { _itemFuelAmount = value; }
        }

        public float itemMaxFuelAmount
        {
            get { return _itemMaxFuelAmount; }
            set { _itemMaxFuelAmount = value; }
        }

        public bool canBurnFuel
        {
            get { return _canBurnFuel; }
            set { _canBurnFuel = value; }
        }

        public bool canRumble
        {
            get { return _canRumble; }
            set { _canRumble = value; }
        }

        public bool showUI
        {
            get { return _showUI; }
            set { _showUI = value; }
        }

        private void Awake()
        {
            UpdateItemStats(true);
            if (itemType == GeneratorItemType.Generator)
            {
                generatorOrigin = transform.localPosition;
            }
        }

        private void Start()
        {
            _generatorInventory = GeneratorInventory.instance;
        }

        void ActivateGenerator()
        {
            rumbling = true;
            activateGenerator.Invoke();     
        }

        void DeactivateGenerator()
        {
            rumbling = false;
            deactivateGenerator.Invoke();
        }

        private void Update()
        {
            RumbleGenerator();
            GeneratorFuelBurnLogic();
        }

        public void ObjectInteract()
        {
            switch(itemType)
            {
                case GeneratorItemType.Jerrycan:
                    JerrycanLogic();
                    break;
                case GeneratorItemType.Generator:
                    GeneratorLogic();
                    break;
                case GeneratorItemType.FuelBarrel:
                    FuelBarrelLogic();
                    break;
            }
        }

        void JerrycanLogic()
        {
            _generatorInventory.CollectedJerrycan(true, itemFuelAmount);
            AudioFuelSwish();
            gameObject.SetActive(false);
        }

        void FuelBarrelLogic()
        {
            if (_generatorInventory.hasJerrycan && itemFuelAmount > 0)
            {
                float fuelToAdd = Mathf.Min(itemFuelAmount, _generatorInventory.maximumInvFuel - _generatorInventory.currentInvFuel);
                _generatorInventory.SetFuelAmounts(true, fuelToAdd);
                itemFuelAmount -= fuelToAdd;
                AudioFuelSwish();
            }
        }

        void GeneratorLogic()
        {
            if (_generatorInventory.hasJerrycan && _generatorInventory.currentInvFuel > 0 && itemFuelAmount < itemMaxFuelAmount)
            {
                fuelRequired = itemMaxFuelAmount - itemFuelAmount;
                float fuelToFill = Mathf.Min(fuelRequired, _generatorInventory.currentInvFuel);
                itemFuelAmount += fuelToFill;
                _generatorInventory.SetFuelAmounts(false, _generatorInventory.currentInvFuel - fuelToFill);
                AudioWaterPour();

                if (itemFuelAmount >= itemMaxFuelAmount)
                {
                    isGenFull = true;
                    ActivateGenerator();
                }
            }
        }

        void GeneratorFuelBurnLogic()
        {
            if (isGenFull && canBurnFuel && itemFuelAmount > 0)
            {
                itemFuelAmount -= Time.deltaTime * burnRate;
                if (itemFuelAmount <= 0)
                {
                    DeactivateGenerator();
                    itemFuelAmount = 0;
                    isGenFull = false;
                }
            }
        }

        public void RumbleGenerator()
        {
            if (canRumble && rumbling)
            {
                transform.localPosition = generatorOrigin + rumbleIntensity * new Vector3(
                Mathf.PerlinNoise(rumbleSpeed * Time.time, 1),
                Mathf.PerlinNoise(rumbleSpeed * Time.time, 2),
                Mathf.PerlinNoise(rumbleSpeed * Time.time, 3));
            }
        }

        void UpdateItemStats(bool on)
        {
            if (showUI)
            {
                _popoutUI.itemNameUI.text = _popoutUI.itemName;
                _popoutUI.iconImageUI.sprite = _popoutUI.iconImage;
                _popoutUI.fuelAmountUI.text = itemFuelAmount.ToString("0");
                _popoutUI.maxFuelAmountUI.text = itemMaxFuelAmount.ToString("0");
                _popoutUI.fuelGaugeUI.fillAmount = (itemFuelAmount / itemMaxFuelAmount);
            }
        }

        public void ShowObjectStats(bool showShow)
        {
            _popoutUI.itemCanvas.SetActive(showShow);
            if (showUI)
            {
                UpdateItemStats(showUI);
            }
        }

        void AudioFuelSwish()
        {
            AKAudioManager.instance.Play(fuelSwishSound);
        }

        void AudioWaterPour()
        {
            AKAudioManager.instance.Play(waterPourSound);
        }
    }
}
