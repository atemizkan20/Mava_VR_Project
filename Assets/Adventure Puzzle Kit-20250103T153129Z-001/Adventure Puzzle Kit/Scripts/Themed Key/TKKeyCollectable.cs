using UnityEngine;

namespace AdventurePuzzleKit.ThemedKey
{
    public class TKKeyCollectable : MonoBehaviour
    {
        [Header("Key ScriptableObject")]
        [SerializeField] private Key keyScriptable = null;

        [Header("Key Audio Clip")]
        [SerializeField] private Sound pickupSound = null;

        public void KeyPickup()
        {
            TKInventory.instance.AddKey(keyScriptable);

            AKAudioManager.instance.Play(pickupSound);
            gameObject.SetActive(false);
        }
    }
}
