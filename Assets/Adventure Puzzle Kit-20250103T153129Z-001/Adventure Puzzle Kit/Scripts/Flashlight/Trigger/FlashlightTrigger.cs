/// <summary>
/// This script inherits from the FlashlightItemBaseClass, so it can share fields between the trigger script and this one. Edit that script if you wish to make other changes to fields.
/// </summary>

using UnityEngine;

namespace AdventurePuzzleKit.FlashlightSystem
{
    public class FlashlightTrigger : FlashlightItemBaseClass
    {
        [Header("Player Tag")]
        [SerializeField] private const string playerTag = "Player";

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(playerTag))
            {
                PickupFlashlightItem();
            }
        }

        private void PickupFlashlightItem()
        {
            switch (_objectType)
            {
                case ObjectType.Flashlight:
                    FlashlightController.instance.CollectFlashlight();
                    break;
                case ObjectType.Battery:
                    FlashlightController.instance.CollectBattery(batteryNumber);
                    break;
            }
            gameObject.SetActive(false);
        }
    }
}
