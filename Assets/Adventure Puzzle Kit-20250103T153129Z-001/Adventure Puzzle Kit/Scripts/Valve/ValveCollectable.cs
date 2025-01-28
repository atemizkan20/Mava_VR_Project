using UnityEngine;

namespace AdventurePuzzleKit.ValveSystem
{
    public class ValveCollectable : MonoBehaviour
    {
        [Header("Valve ScriptableObject")]
        [SerializeField] private Valve valveScriptable = null;

        [Header("Valve Audio Clip")]
        [SerializeField] private Sound pickupSound = null;

        public bool _CanCollect { get; set; } = false;

        public void ValvePickup()
        {
            ValveInventory.instance.AddValve(valveScriptable);

            AKAudioManager.instance.Play(pickupSound);
            gameObject.SetActive(false);
        }
    }
}
