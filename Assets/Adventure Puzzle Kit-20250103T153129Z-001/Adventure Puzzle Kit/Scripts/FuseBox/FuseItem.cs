using UnityEngine;

namespace AdventurePuzzleKit.FuseSystem
{
    public class FuseItem : MonoBehaviour
    {
        [SerializeField] private ObjectType _objectType = ObjectType.None;
        private enum ObjectType { None, Fusebox, Fuse }

        [Header("Sound Effect Scriptables (Only for Fuse)")]
        [SerializeField] private Sound pickupSound = null;

        public void ObjectInteract()
        {
            switch (_objectType)
            {
                case ObjectType.Fusebox:
                    GetComponent<FuseboxController>().CheckFuseBox();
                    break;
                case ObjectType.Fuse:
                    FuseInventory.instance.AddFuse();
                    AKAudioManager.instance.Play(pickupSound);
                    gameObject.SetActive(false);
                    break;
            }
        }
    }
}
