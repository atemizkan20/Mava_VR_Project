using UnityEngine;
using UnityEngine.Events;

namespace AdventurePuzzleKit.GasMaskSystem
{
    public class GasMaskHealthManager : MonoBehaviour
    {
        [Header("Health Variables")]
        [Range(0, 100)] [SerializeField] private float currentHealth = 100.0f;
        [Range(0, 100)] [SerializeField] private float maxHealth = 100.0f;
        [SerializeField] private float healthFall = 2;

        [Header("Health Regeneration Variables")]
        [SerializeField] private float maxHealthTimer = 1.0f; //Make sure this is the default start time of the regeneration
        private float currentHealthTimer = 1.0f; //How long it takes before regeneration health
        private bool regenHealth = false; //Whether we should regenerate health or not

        [Header("Death Event")]
        [SerializeField] private UnityEvent onDeath = null;

        public static GasMaskHealthManager instance;

        void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }

            currentHealthTimer = maxHealthTimer;
        }

        public void UpdateHealth()
        {
            AKUIManager.instance.UpdateHealthUI(currentHealth, maxHealth);
            if (currentHealth <= 0)
            {
                Death();
            }
        }

        public void RegenerateHealth(bool on)
        {
            regenHealth = on;
        }

        private void Update()
        {
            RegenHealth();
        }

        private void RegenHealth()
        {
            if (regenHealth)
            {
                if (currentHealth <= maxHealth)
                {
                    currentHealthTimer -= Time.deltaTime;

                    if (currentHealthTimer <= 0)
                    {
                        currentHealth += Time.deltaTime * 10;
                        UpdateHealth();
                        currentHealthTimer = 0;

                        if (currentHealth >= maxHealth)
                        {
                            currentHealthTimer = maxHealthTimer;
                            regenHealth = false;
                        }
                    }
                }
                else
                {
                    regenHealth = false;
                    currentHealthTimer = maxHealthTimer;
                }
            }
        }

        public void DamageHealth()
        {
            currentHealth -= healthFall * Time.deltaTime;
            UpdateHealth();
        }

        public void Death()
        {
            onDeath.Invoke();
        }
    }
}
