using UnityEngine;
using UnityEngine.UI;

namespace AdventurePuzzleKit.GeneratorSystem
{
    [System.Serializable]
    public class PopoutUI
    {
        [Header("Item Parameters")]
        public string itemName = null;
        public Sprite iconImage = null;

        [Header("UI - World Space Floating Canvas")]
        public GameObject itemCanvas = null;

        [Header("UI - World Space Floating Elements")]
        public Text itemNameUI = null;
        public Image iconImageUI = null;
        public Text fuelAmountUI = null;
        public Text maxFuelAmountUI = null;
        public Image fuelGaugeUI = null;
    }
}
