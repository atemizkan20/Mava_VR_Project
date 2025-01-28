using UnityEngine;

namespace AdventurePuzzleKit.GasMaskSystem
{
    public class GasDamage : MonoBehaviour
    {
        [Header("Player Tag")]
        [SerializeField] private const string playerTag = "Player";

        void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(playerTag))
            {
                GasMaskController.instance.DamageGas();
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(playerTag))
            {
                GasMaskController.instance.EnableBreathing();
            }
        }
    }
}