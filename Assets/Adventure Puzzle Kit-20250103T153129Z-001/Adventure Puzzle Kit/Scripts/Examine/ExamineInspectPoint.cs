using UnityEngine;
using UnityEngine.Events;

namespace AdventurePuzzleKit.ExamineSystem
{
    public class ExamineInspectPoint : MonoBehaviour
    {
        [Header("Inspect Point Details")]
        [TextArea][SerializeField] private string inspectDescription = "";

        [Header("Click Event")]
        [SerializeField] private UnityEvent specialInteraction = null;

        public void InspectPointInteract()
        {
            specialInteraction.Invoke();
        }

        public string InspectInformation()
        {
            return inspectDescription;
        }
    }
}
