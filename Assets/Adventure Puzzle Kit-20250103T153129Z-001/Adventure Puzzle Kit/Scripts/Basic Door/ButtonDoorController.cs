using UnityEngine;
using System.Collections;

namespace AdventurePuzzleKit.DoorSystem
{
    public class ButtonDoorController : MonoBehaviour
    {
        [Header("Door Object")]
        [SerializeField] private Animator doorAnim = null;
        private bool doorOpen;

        [Header("Door Animation Names")]
        [SerializeField] private string openAnimationName = "DoorOpen";
        [SerializeField] private string closeAnimationName = "DoorClose";
        private string animationName;

        [Header("Sounds")]
        [SerializeField] private Sound doorOpenSound = null;
        [SerializeField] private Sound doorCloseSound = null;
        private Sound doorSound;

        [Header("Pause Timer")]
        [SerializeField] private int waitTimer = 1;
        private bool pauseInteraction = false;

        private IEnumerator PauseDoorInteraction()
        {
            pauseInteraction = true;
            yield return new WaitForSeconds(waitTimer);
            pauseInteraction = false;

        }

        public void PlayAnimation()
        {
            if (!pauseInteraction)
            {
                animationName = doorOpen ? closeAnimationName : openAnimationName;
                doorSound = doorOpen ? doorCloseSound : doorOpenSound;
                doorAnim.Play(animationName, 0, 0.0f);
                doorOpen = !doorOpen;
                AKAudioManager.instance.Play(doorSound);
                StartCoroutine(PauseDoorInteraction());
            }
        }
    }
}
