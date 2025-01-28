using System.Collections.Generic;
using UnityEngine;

namespace AdventurePuzzleKit.ValveSystem
{
    public class ValveInventory : MonoBehaviour
    {
        public List<Valve> _valvesList = new List<Valve>();

        public static ValveInventory instance;

        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }
        }

        public void AddValve(Valve valve)
        {
            if (!_valvesList.Contains(valve))
            {
                _valvesList.Add(valve);
                AKUIManager.instance.FillValveInventorySlot();
                AKUIManager.instance.ValveCollected();
            }
        }

        public void RemoveValve(Valve valve)
        {
            if (_valvesList.Contains(valve))
            {
                int currentCount = _valvesList.Count;
                _valvesList.Remove(valve);
                AKUIManager.instance.ResetValveInventorySlot(currentCount);
            }
        }
    }
}
