using UnityEngine;
using System.Collections.Generic;

namespace AdventurePuzzleKit.ThemedKey
{
    public class TKInventory : MonoBehaviour
    {
        public List<Key> _keyList = new List<Key>();

        public static TKInventory instance;

        void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }
        }

        public void AddKey(Key key)
        {
            if (!_keyList.Contains(key))
            {
                _keyList.Add(key);
                AKUIManager.instance.ThemedKeyCollected();
                AKUIManager.instance.FillTKInventorySlot();

                // Update the hotbar
                FindObjectOfType<HotbarManager>()?.UpdateHotbar();
            }
        }

        public void RemoveKey(Key key)
        {
            if (_keyList.Contains(key))
            {
                int currentCount = _keyList.Count;
                _keyList.Remove(key);
                AKUIManager.instance.ResetTKInventorySlot(currentCount);
            }
        }
    }
}
