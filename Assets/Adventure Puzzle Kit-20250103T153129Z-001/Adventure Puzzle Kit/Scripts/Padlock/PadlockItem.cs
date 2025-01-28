using UnityEngine;

namespace AdventurePuzzleKit.PadlockSystem
{
    public class PadlockItem : MonoBehaviour
    {
        [SerializeField] private PadlockController _padlockController = null;

        public void ObjectInteract()
        {
            _padlockController.ShowPadlock();
        }
    }
}
