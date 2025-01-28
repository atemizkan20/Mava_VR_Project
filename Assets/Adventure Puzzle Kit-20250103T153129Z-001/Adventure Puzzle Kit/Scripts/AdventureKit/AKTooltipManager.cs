using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace AdventurePuzzleKit
{
    public enum SystemType { None, FlashlightSys, GeneratorSys, GasMaskSys, ThemedKeySys, ChessSys, FuseBoxSys, ValveSys }

    public class AKTooltipManager : MonoBehaviour
    {
        [Header("Tooltip UI Elements")]
        [SerializeField] private RectTransform tooltipObject = null;
        [SerializeField] private Text tooltipText = null;

        [Header("Main Canvas")]
        [SerializeField] private Canvas canvas = null;

        [Header("Position Tooltip")]
        [SerializeField] private Vector2 offset = new Vector2(0, 0);

        private SystemType currentSystemType = SystemType.None;

        public static AKTooltipManager instance;

        void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }
        }

        public void ToggleTooltip(bool isActive)
        {
            tooltipObject.gameObject.SetActive(isActive);

            if (isActive)
            {
                UpdateTooltipPosition();
            }
        }

        public void SetSystemType(SystemType systemType)
        {
            currentSystemType = systemType;
            switch (currentSystemType)
            {
                case SystemType.None:
                    break;
                case SystemType.FlashlightSys: tooltipText.text = "Flashlight";
                    break;
                case SystemType.GeneratorSys: tooltipText.text = "Jerrycan";
                    break;
                case SystemType.GasMaskSys: tooltipText.text = "Gas Mask";
                    break;
                case SystemType.ThemedKeySys: tooltipText.text = "Themed Keys";
                    break;
                case SystemType.ChessSys: tooltipText.text = "Chess";
                    break;
                case SystemType.FuseBoxSys: tooltipText.text = "Fuses";
                    break;
                case SystemType.ValveSys: tooltipText.text = "Valves";
                    break;
                default:
                    break;
            }
        }

        public void UpdateTooltipPosition()
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out Vector2 localPoint);
            tooltipObject.anchoredPosition = localPoint + offset;
        }
    }
}

