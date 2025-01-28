using UnityEngine;
using UnityEngine.Events;

namespace ValveSystem
{
    public class ValvesCompleted : MonoBehaviour
    {
        [Header("Event to happen when all valves are turned")]
        [SerializeField] private UnityEvent valveEvent = null;

        //Which valves are completely turned?
        private bool hasTurnedRedValve;
        private bool hasTurnedBlueValve;
        private bool hasTurnedGreenValve;
        private bool hasTurnedBlackValve;

        public void CheckForCompletion(string valveType)
        {
            if (valveType == "Red")
            {
                hasTurnedRedValve = true;
            }
            else if (valveType == "Blue")
            {
                hasTurnedBlueValve = true;
            }
            else if (valveType == "Green")
            {
                hasTurnedGreenValve = true;
            }
            else if (valveType == "Black")
            {
                hasTurnedBlackValve = true;
            }

            if (hasTurnedRedValve && hasTurnedBlueValve && hasTurnedGreenValve && hasTurnedBlackValve)
            {
                print("All Valves Turned On");
                valveEvent.Invoke();
            }
        }
    }
}

