using UnityEngine;

namespace AdventurePuzzleKit
{
    public class RemoveTags : MonoBehaviour
    {
        [SerializeField] private GameObject taggedObject = null;

        public void RemoveAllTags()
        {
            taggedObject.tag = "Untagged";
        }
    }
}
