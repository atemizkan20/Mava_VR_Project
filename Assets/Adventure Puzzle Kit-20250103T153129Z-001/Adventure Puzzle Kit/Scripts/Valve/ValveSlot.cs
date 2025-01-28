using System.Collections;
using UnityEngine;

namespace AdventurePuzzleKit.ValveSystem
{
    public class ValveSlot : MonoBehaviour
    {
        [Header("Valve ScriptableObject")]
        [SerializeField] private Valve valveScriptable = null;

        [Header("Valve Slot Objects")]
        [SerializeField] private GameObject valveWheel = null;
        [SerializeField] private GameObject questionMarkPopout = null;

        [Header("Valve Attach - Audio Clip")]
        [SerializeField] private Sound attachSound = null;

        public bool _AttachSlot { get; set; } = false;

        public void CheckValveSlot()
        {
            bool valveExistsInInventory = ValveInventory.instance._valvesList.Contains(valveScriptable);

            if (valveExistsInInventory)
            {
                ValveInventory.instance.RemoveValve(valveScriptable);
                AKAudioManager.instance.Play(attachSound);
                valveWheel.SetActive(true);
                gameObject.SetActive(false);
            }
            else if (questionMarkPopout != null && valveExistsInInventory)
            {
                StartCoroutine(ShowValveTextUI());
            }
        }

        IEnumerator ShowValveTextUI()
        {
            questionMarkPopout.SetActive(true);
            yield return new WaitForSeconds(1);
            questionMarkPopout.SetActive(false);
        }
    }
}
