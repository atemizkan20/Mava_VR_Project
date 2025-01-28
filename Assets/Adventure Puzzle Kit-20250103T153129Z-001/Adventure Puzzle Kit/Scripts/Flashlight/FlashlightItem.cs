/// <summary>
/// This script inherits from the FlashlightItemBaseClass, so it can share fields between the trigger script and this one. Edit that script if you wish to make other changes to fields.
/// </summary>

namespace AdventurePuzzleKit.FlashlightSystem
{
    public class FlashlightItem : FlashlightItemBaseClass
    {
        public void ObjectInteract()
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
