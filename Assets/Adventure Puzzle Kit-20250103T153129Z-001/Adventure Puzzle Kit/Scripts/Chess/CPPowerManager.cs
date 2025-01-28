using UnityEngine;
using UnityEngine.Events;

namespace AdventurePuzzleKit.ChessSystem
{
    public class CPPowerManager : MonoBehaviour
    {
        [Header("Total Number of Fuse Boxes")]
        [SerializeField] private int maxFuseBoxCount = 6;
        [SerializeField] private int currentFuseBoxCount = 0;

        [Header("Disabling Interaction")]
        [SerializeField] private bool disablePuzzleAfterUse = false;

        [Header("Fuse Box List - To Disable Tags")]
        [SerializeField] private GameObject[] fuseBoxList = null;

        [Header("Power on - Chess pieces correct")]
        [SerializeField] private UnityEvent powerUp = null;

        public void UpdateFuseCount(bool fuseBoxPowered)
        {
            currentFuseBoxCount += fuseBoxPowered ? 1 : -1;

            if (currentFuseBoxCount >= maxFuseBoxCount)
            {
                PowerFuseBox();
                currentFuseBoxCount = maxFuseBoxCount;
            }
        }

        void RemoveTags()
        {
            for (int i = 0; i < fuseBoxList.Length; i++)
            {
                if (fuseBoxList[i].CompareTag("Untagged"))
                    continue;

                fuseBoxList[i].tag = "Untagged";
            }
        }

        public void PowerFuseBox()
        {
            if (disablePuzzleAfterUse)
            {
                AKUIManager.instance.DisableInventoryFusebox();
                RemoveTags();
            }
            powerUp.Invoke();
        }
    }
}
