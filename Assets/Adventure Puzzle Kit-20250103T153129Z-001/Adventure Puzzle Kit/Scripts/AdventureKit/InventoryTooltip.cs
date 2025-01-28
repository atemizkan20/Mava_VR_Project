using UnityEngine;
using UnityEngine.EventSystems;

namespace AdventurePuzzleKit
{
    public class InventoryTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public SystemType systemType = SystemType.None;

        public void OnPointerEnter(PointerEventData eventData)
        {
            AKTooltipManager.instance.SetSystemType(systemType);
            AKTooltipManager.instance.ToggleTooltip(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            AKTooltipManager.instance.ToggleTooltip(false);
        }
    }
}
