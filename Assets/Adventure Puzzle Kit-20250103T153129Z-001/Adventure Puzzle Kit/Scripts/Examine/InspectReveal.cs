using UnityEngine;

namespace AdventurePuzzleKit.ExamineSystem
{
    public class InspectReveal : MonoBehaviour
    {
        [Header("Basic Reveal Elements")]
        [SerializeField] private GameObject objectToHide = null;
        [SerializeField] private GameObject objectToReveal = null;

        public void RevealHidden()
        {
            objectToHide.SetActive(false);
            objectToReveal.SetActive(true);
        }

        public void PickupExample()
        {
            objectToHide.SetActive(false);
            Debug.Log("Add some additional code for when this item is collected");
        }
    }
}
