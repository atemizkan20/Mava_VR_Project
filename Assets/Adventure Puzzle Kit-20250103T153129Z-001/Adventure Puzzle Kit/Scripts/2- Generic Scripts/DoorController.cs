using UnityEngine;

namespace AdventurePuzzleKit
{
    public class DoorController : MonoBehaviour
    {
        private Animator doorAnim;
        [Space(10)]
        [SerializeField] private string animationName = "OpenDoor";
        [Space(10)]
        [SerializeField] private Sound soundClip = null;

        private void Awake()
        {
            doorAnim = gameObject.GetComponent<Animator>();
        }

        public void PlayAnimation()
        {
            AKAudioManager.instance.Play(soundClip);
            doorAnim.Play(animationName, 0, 0.0f);
        }
    }
}
