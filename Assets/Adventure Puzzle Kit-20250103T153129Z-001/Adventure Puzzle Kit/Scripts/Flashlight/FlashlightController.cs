using UnityEngine;

namespace AdventurePuzzleKit.FlashlightSystem
{
    public class FlashlightController : MonoBehaviour
    {
        [Header("Flashlight On Start")]
        [SerializeField] private bool hasFlashlight = false;

        [Header("Inventory Toggle")] 
        [Tooltip("If this is true, allows the user to toggle inventory on / off")]
        [SerializeField] private bool showFlashlightInventory = false;

        [Header("Infinite Flashlight")]
        [SerializeField] private bool infiniteFlashlight = false;

        [Header("Battery Parameters")]
        [SerializeField] private float batteryDrainAmount = 0.01f;
        [SerializeField] private int batteryCount = 1;

        [Header("Battery Reload Timers")]
        [SerializeField] private float replaceBatteryTimer = 1.0f;
        [SerializeField] private float maxReplaceBatteryTimer = 1.0f;

        [Header("Flashlight Parameters")]
        [Range(0, 10)][SerializeField] private float maxFlashlightIntensity = 1.0f;
        [Range(1, 10)][SerializeField] private int flashlightRotationSpeed = 2;

        [Header("Main Flashlight References")]
        [SerializeField] private Light flashlightSpot = null;
        [SerializeField] private FlashlightMovement flashlightMovement = null;

        [Header("Sounds")]
        [SerializeField] private Sound flashlightPickup = null;
        [SerializeField] private Sound flashlightClick = null;
        [SerializeField] private Sound flashlightReload = null;

        private bool shouldUpdate = false;
        private bool isFlashlightOn;

        public static FlashlightController instance;

        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }
        }

        void Start()
        {
            flashlightSpot.intensity = maxFlashlightIntensity;
            flashlightMovement.speed = flashlightRotationSpeed;
            maxReplaceBatteryTimer = replaceBatteryTimer;

            if(!showFlashlightInventory && !infiniteFlashlight)
            {
                print("You may want to make the flashlight infinite if you're not showing the flashlight UI!");
            }

            if (!showFlashlightInventory)
            {
                AKUIManager.instance.ToggleFlashlightUI(false);
                AKUIManager.instance.disableFlashlightUI = true;
            }
            else
            {
                AKUIManager.instance.UpdateBatteryCountUI(batteryCount);
            }

            if (hasFlashlight)
            {
                UpdateUIElements();
            }
        }

        private void UpdateUIElements(bool UpdateBatteryCount = false, bool isFlashlight = false)
        {
            if (showFlashlightInventory)
            {
                if (isFlashlight)
                {
                    AKUIManager.instance.FlashlightCollected();
                }

                if (UpdateBatteryCount)
                {
                    AKUIManager.instance.BatteryCollected();
                    AKUIManager.instance.UpdateBatteryCountUI(batteryCount);
                }
                AKUIManager.instance.FlashlightIndicatorColor(isFlashlightOn);
            }
        }

        void Update()
        {
            if (hasFlashlight && !AKUIManager.instance.isInventoryOpen)
            {
                PlayerInput();
                DrainBattery();
            }
        }

        void PlayerInput()
        {
            if (!AKUIManager.instance.isInventoryOpen)
            {
                if (Input.GetKeyDown(AKInputManager.instance.flashlightSwitch)) //TURNING FLASHLIGHT ON/OFF
                {
                    FlashlightSwitch();
                }

                if (!infiniteFlashlight)
                {
                    if (Input.GetKey(AKInputManager.instance.reloadBattery) && batteryCount >= 1)
                    {
                        ReplaceBattery();
                    }
                    else
                    {
                        CoolDownTimer();
                    }

                    if (Input.GetKeyUp(AKInputManager.instance.reloadBattery))
                    {
                        shouldUpdate = true;
                    }
                }
            }
        }

        public void CollectFlashlight()
        {
            hasFlashlight = true;           
            FlashlightPickupSound();
            UpdateUIElements(true, true);
        }

        public void CollectBattery(int batteries)
        {
            batteryCount = batteryCount + batteries;
            FlashlightPickupSound();
            UpdateUIElements(true);
        }

        void FlashlightSwitch()
        {
            isFlashlightOn = !isFlashlightOn;

            flashlightSpot.enabled = isFlashlightOn;
            UpdateUIElements();
            FlashlightClickSound();
        }

        void ReplaceBattery()
        {
            shouldUpdate = false;
            replaceBatteryTimer -= Time.deltaTime;
            if (showFlashlightInventory)
            {
                AKUIManager.instance.EnableRadialIndicatorUI(replaceBatteryTimer);
            }

            if (replaceBatteryTimer <= 0)
            {
                batteryCount--;
                UpdateUIElements(true);
                flashlightSpot.intensity = maxFlashlightIntensity;
                if (showFlashlightInventory)
                {
                    AKUIManager.instance.MaximumBatteryLevel(maxFlashlightIntensity);
                }
                FlashlightReloadSound();

                replaceBatteryTimer = maxReplaceBatteryTimer;
                if (showFlashlightInventory)
                {
                    AKUIManager.instance.DisableRadialIndicatorUI(maxReplaceBatteryTimer);
                }
            }
        }

        void CoolDownTimer()
        {
            if (shouldUpdate)
            {
                replaceBatteryTimer += Time.deltaTime;
                if (showFlashlightInventory)
                {
                    AKUIManager.instance.EnableRadialIndicatorUI(replaceBatteryTimer);
                }

                if (replaceBatteryTimer >= maxReplaceBatteryTimer)
                {
                    replaceBatteryTimer = maxReplaceBatteryTimer;
                    if (showFlashlightInventory)
                    {
                        AKUIManager.instance.DisableRadialIndicatorUI(maxReplaceBatteryTimer);
                    }
                    shouldUpdate = false;
                }
            }
        }

        void DrainBattery()
        {
            if (!infiniteFlashlight && isFlashlightOn)
            {
                flashlightSpot.intensity = Mathf.Clamp(flashlightSpot.intensity - batteryDrainAmount * Time.deltaTime * maxFlashlightIntensity, 0, maxFlashlightIntensity);
                if (showFlashlightInventory)
                {
                    AKUIManager.instance.UpdateBatteryLevelUI(batteryDrainAmount * Time.deltaTime);
                }
            }
        }

        void FlashlightPickupSound()
        {
            AKAudioManager.instance.Play(flashlightPickup);
        }

        void FlashlightClickSound()
        {
            AKAudioManager.instance.Play(flashlightClick);
        }

        void FlashlightReloadSound()
        {
            AKAudioManager.instance.Play(flashlightReload);
        }
    }
}