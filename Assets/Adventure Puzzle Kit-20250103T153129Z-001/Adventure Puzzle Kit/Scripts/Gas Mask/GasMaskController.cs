using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace AdventurePuzzleKit.GasMaskSystem
{
  public class GasMaskController : MonoBehaviour
    {
        public enum GasMaskState { GasMaskOn, GasMaskOffInSmoke, GasMaskOffOutOfSmoke }
        private GasMaskState _gasMaskState;

        [Header("Gas Mask Features")]
        [SerializeField] private float maxEquipTimer = 1f; //The maximum time you want to wait before putting on or taking off the mask. Same as "maskTimer"
        private float maskBeforeTimer = 0.99f; //Just a millisecond before the max timer so we can stop this looping, it will autofill from the start function if edited
        private bool hasGasMask = false;
        private float equipTimer = 1f;
        private bool puttingOn = false, pullingOff = false;

        [Header("Player")]
        [SerializeField] private FirstPersonController player = null;

        [Header("Movement Speeds")]
        [SerializeField] private float walkNorm = 5;
        [SerializeField] private float walkGas = 2;
        [SerializeField] private float runNorm = 10;
        [SerializeField] private float runGas = 4;

        [Header("Filter Options")]
        [Range(0, 20)][SerializeField] private float filterFallRate = 2f; //Increase this to make the filter deplete faster
        [Range(0, 100)][SerializeField] private int warningPercentage = 20; //The percentage the system will give a warning

        [Range(0, 100)][SerializeField]private float _filterTimer = 100f; //Set the same as your max value, do not change!
        private bool hasFilter = true; //Whether you have a filter or not
        private bool filterChanged = false; //Has the filter changed
        [SerializeField] private int _maskFilters = 0; //How many filters does your player currently have? Increase this value at the start to give them more!

        [Header("Human Audio")]
        [SerializeField] private Sound deepBreathAudio = null;
        [SerializeField] private Sound breathInAudio = null;
        [SerializeField] private Sound breathOutAudio = null;
        [SerializeField] private Sound breathingFullAudio = null;
        [SerializeField] private Sound chokingAudio = null;

        [Header("Gas Mask Audio")]
        [SerializeField] private Sound pickupAudio = null;
        [SerializeField] private Sound replaceFilterAudio = null;
        [SerializeField] private Sound warningAudio = null;

        private bool canBreath = true; //Can be made visible for debugging - Can the player breath at any time?
        private bool playOnce = false;
        private bool shouldUpdateEquip = false;
        private bool shouldUpdateFilter = false;
        private bool isGasMaskEquipped = false;
        private float warningFilterTimerValue;

        public float maxFilterTimer { get; set; } = 100f;

        public float filterTimer
        {
            get { return _filterTimer; }
            set { _filterTimer = value; }
        }

        public int maskFilters
        {
            get { return _maskFilters; }
            set { _maskFilters = value; }
        }

        public static GasMaskController instance;

        void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else {  instance = this; DontDestroyOnLoad(gameObject); }
        }

        void Start()
        {
            filterTimer = maxFilterTimer;
            equipTimer = maxEquipTimer;
            maskBeforeTimer = maxEquipTimer - 0.01f;
            string filterAmount = maskFilters.ToString("0");
            AKUIManager.instance.UpdateFilterUI(AKUIManager.FilterState.FilterNumber, filterAmount, 0);
            warningFilterTimerValue = (maxFilterTimer / 100) * warningPercentage;
        }

        void PlayerMovement(bool slowPlayer)
        {
            if (slowPlayer)
            {
                player.m_WalkSpeed = walkGas;
                player.m_RunSpeed = runGas;
            }
            else
            {
                player.m_WalkSpeed = walkNorm;
                player.m_RunSpeed = runNorm;
            }
        }

        void Update()
        {
            EquippingGasMask();
            EquippingFilter();
            FilterUsage();
            SetGasMaskState();
        }

        void EquippingGasMask()
        {
            bool equipMaskKeyPressed = Input.GetKey(AKInputManager.instance.equipMaskKey);
            bool equipMaskKeyReleased = Input.GetKeyUp(AKInputManager.instance.equipMaskKey);
            bool canEquipMask = equipMaskKeyPressed && hasFilter && hasGasMask && !puttingOn && !pullingOff;

            // Equipping the gas mask
            if (canEquipMask && !isGasMaskEquipped)
            {
                shouldUpdateEquip = false;
                equipTimer -= Time.deltaTime;
                AKUIManager.instance.EnableRadialIndicatorUI(equipTimer);

                if (equipTimer <= 0)
                {
                    equipTimer = maxEquipTimer;
                    AKUIManager.instance.DisableRadialIndicatorUI(maxEquipTimer);
                    StartCoroutine(MaskOn());
                    StartCoroutine(Wait());
                }
            }
            // Unequipping the gas mask
            else if (canEquipMask && isGasMaskEquipped)
            {
                shouldUpdateEquip = false;
                equipTimer -= Time.deltaTime;
                AKUIManager.instance.EnableRadialIndicatorUI(equipTimer);

                if (equipTimer <= 0)
                {
                    equipTimer = maxEquipTimer;
                    AKUIManager.instance.DisableRadialIndicatorUI(maxEquipTimer);
                    pullingOff = true;
                    MaskOff();
                    StartCoroutine(Wait());
                }
            }
            else if (shouldUpdateEquip)
            {
                equipTimer += Time.deltaTime;
                AKUIManager.instance.EnableRadialIndicatorUI(equipTimer);

                if (equipTimer >= maskBeforeTimer)
                {
                    equipTimer = maxEquipTimer;
                    AKUIManager.instance.DisableRadialIndicatorUI(maxEquipTimer);
                    shouldUpdateEquip = false;
                    StartCoroutine(Wait());
                }
            }

            if (equipMaskKeyReleased)
            {
                shouldUpdateEquip = true;
            }
        }

        void EquippingFilter()
        {
            if (hasGasMask)
            {
                if (Input.GetKey(AKInputManager.instance.replaceFilterKey) && _maskFilters >= 1)
                {
                    shouldUpdateFilter = false;
                    equipTimer -= Time.deltaTime;
                    AKUIManager.instance.EnableRadialIndicatorUI(equipTimer);
                    if (equipTimer <= 0)
                    {
                        equipTimer = maxEquipTimer;
                        AKUIManager.instance.DisableRadialIndicatorUI(maxEquipTimer);
                        ReplaceFilter();
                    }
                }
                else
                {
                    if (shouldUpdateFilter)
                    {
                        equipTimer += Time.deltaTime;
                        AKUIManager.instance.EnableRadialIndicatorUI(equipTimer);

                        if (equipTimer >= maxEquipTimer)
                        {
                            equipTimer = maxEquipTimer;
                            AKUIManager.instance.DisableRadialIndicatorUI(maxEquipTimer);
                            shouldUpdateFilter = false;
                        }
                    }
                }

                if (Input.GetKeyUp(AKInputManager.instance.replaceFilterKey))
                {
                    shouldUpdateFilter = true;
                }
            }
        }
        void FilterUsage()
        {
            if (hasGasMask && isGasMaskEquipped)
            {
                filterTimer -= Time.deltaTime * filterFallRate;
                float filterFillAmount = filterTimer / maxFilterTimer;
                AKUIManager.instance.UpdateFilterUI(AKUIManager.FilterState.FilterValue, null, filterFillAmount);

                if (filterTimer <= 1)
                {
                    if (_maskFilters >= 1)
                    {
                        ReplaceFilter();
                    }
                    else
                    {
                        filterTimer = 0;
                        AKUIManager.instance.UpdateFilterUI(AKUIManager.FilterState.FilterValue, null, filterFillAmount);
                        hasFilter = false;
                        MaskOff();
                    }
                }

                if (filterTimer <= warningFilterTimerValue && !filterChanged)
                {
                    AKUIManager.instance.UpdateFilterUI(AKUIManager.FilterState.FilterAlarm, null, 0);
                    AKAudioManager.instance.Play(warningAudio);
                    filterChanged = true;
                }
            }
        }

        void SetGasMaskState()
        {
            if (isGasMaskEquipped)
            {
                _gasMaskState = GasMaskState.GasMaskOn;
            }
            else
            {
                _gasMaskState = canBreath ? GasMaskState.GasMaskOffOutOfSmoke : GasMaskState.GasMaskOffInSmoke;
            }

            switch (_gasMaskState)
            {
                case GasMaskState.GasMaskOffOutOfSmoke:
                    if (playOnce)
                    {
                        AKAudioManager.instance.StopPlaying(chokingAudio);
                        AKAudioManager.instance.Play(deepBreathAudio);
                        playOnce = false;
                    }
                    break;
                case GasMaskState.GasMaskOffInSmoke:
                    if (!playOnce)
                    {
                        AKAudioManager.instance.StopPlaying(deepBreathAudio);
                        AKAudioManager.instance.StopPlaying(chokingAudio);
                        AKAudioManager.instance.Play(chokingAudio);
                        playOnce = true;
                    }
                    GasMaskHealthManager.instance.DamageHealth();
                    break;
                case GasMaskState.GasMaskOn:
                    AKAudioManager.instance.StopPlaying(chokingAudio);
                    AKAudioManager.instance.StopPlaying(deepBreathAudio);
                    break;
            }
            return;
        }

        public void PickupGasMask()
        {
            if(!hasGasMask)
            {
                hasGasMask = true;
                AKUIManager.instance.GasMaskCollected();
                AKUIManager.instance.FilterCollected();
                AKAudioManager.instance.Play(pickupAudio);
                AKUIManager.instance.UpdateMaskUI(AKUIManager.MaskUIState.MaskNormal);
            }
        }

        public void PickupFilter()
        {
            _maskFilters++;
            AKUIManager.instance.FilterCollected();
            AKAudioManager.instance.Play(pickupAudio);
            string filterAmount = maskFilters.ToString("0");
            AKUIManager.instance.UpdateFilterUI(AKUIManager.FilterState.FilterNumber, filterAmount, 0);
        }

        void ReplaceFilter()
        {
            _maskFilters--;
            filterTimer = maxFilterTimer;
            hasFilter = true;

            AKAudioManager.instance.Play(replaceFilterAudio);

            string filterAmount = maskFilters.ToString("0");
            float filterFillAmount = filterTimer / maxFilterTimer;

            AKUIManager.instance.UpdateFilterUI(AKUIManager.FilterState.FilterNormal, null, 0);
            AKUIManager.instance.UpdateFilterUI(AKUIManager.FilterState.FilterNumber, filterAmount, 0);
            AKUIManager.instance.UpdateFilterUI(AKUIManager.FilterState.FilterValue, null, filterFillAmount);
            filterChanged = false;
        }

        public void DamageGas()
        {
            if (!isGasMaskEquipped)
            {
                canBreath = false;
                PlayerMovement(true);

                GasMaskHealthManager.instance.UpdateHealth();
                GasMaskHealthManager.instance.RegenerateHealth(false);
                AKUIManager.instance.GasChokingEffect(true, true);
            }
            else
            {
                PlayerMovement(false);
                AKUIManager.instance.GasChokingEffect(true, false);
            }
        }

        public void EnableBreathing()
        {
            canBreath = true;
            PlayerMovement(false);
            GasMaskHealthManager.instance.RegenerateHealth(true);
            AKUIManager.instance.GasChokingEffect(null, false);
        }

        IEnumerator MaskOn()
        {
            isGasMaskEquipped = true;
            AKAudioManager.instance.Play(breathInAudio);

            AKUIManager.instance.UpdateMaskUI(AKUIManager.MaskUIState.MaskEquipped);
            AKUIManager.instance.GasMaskVisorUI(true);

            const float waitDuration = 1.5f;
            yield return new WaitForSeconds(waitDuration);

            AKAudioManager.instance.Play(breathingFullAudio);         
        }

        void MaskOff()
        {
            isGasMaskEquipped = false;
            AKAudioManager.instance.Play(breathOutAudio);
            AKAudioManager.instance.StopPlaying(breathingFullAudio);
            AKAudioManager.instance.StopPlaying(deepBreathAudio);

            AKUIManager.instance.UpdateMaskUI(AKUIManager.MaskUIState.MaskNormal);
            AKUIManager.instance.GasMaskVisorUI(false);
        }

        IEnumerator Wait()
        {
            if (!isGasMaskEquipped) pullingOff = true;
            else puttingOn = true;

            const float waitDuration = 2.5f;
            yield return new WaitForSeconds(waitDuration);
            puttingOn = pullingOff = false;
        }
    }
}
