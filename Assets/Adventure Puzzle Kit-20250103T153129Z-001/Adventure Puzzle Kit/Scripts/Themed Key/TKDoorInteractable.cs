using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace AdventurePuzzleKit.ThemedKey
{
    public class TKDoorInteractable : MonoBehaviour
    {
        [Header("Key ScriptableObject")]
        [SerializeField] private bool removeKeyAfterUse = false;
        [SerializeField] private Key keyScriptable = null;

        [Header("Animated Key Object")]
        [SerializeField] private GameObject animatedDoorKey = null;
        [SerializeField] private string keyAnimation = "HeartKey_Anim_Unlock";

        [Header("Door - Audio Clips")]
        [SerializeField] private Sound lockedDoorSound = null;
        [SerializeField] private Sound insertKeySound = null;

        [Header("Door Opening Sound Delays")]
        [SerializeField] private float keyAudioDelay = 0.5f;
        [SerializeField] private float doorOpenDelay = 1.5f;

        [Header("Animation Event")]
        [SerializeField] private UnityEvent onUnlock = null;

        private Animator anim;
        private Coroutine animationCoroutine;

        private void Start()
        {
            anim = animatedDoorKey.GetComponent<Animator>();
        }

        public void CheckDoor()
        {
            if (TKInventory.instance._keyList.Contains(keyScriptable))
            {
                if (removeKeyAfterUse)
                {
                    TKInventory.instance.RemoveKey(keyScriptable);
                }

                if (animationCoroutine != null)
                {
                    StopCoroutine(animationCoroutine);
                }

                gameObject.tag = "Untagged";
                animationCoroutine = StartCoroutine(PlayAnimation());
            }
            else
            {
                LockedDoorSound();
            }
        }

        public IEnumerator PlayAnimation()
        {
            animatedDoorKey.SetActive(true);
            anim.Play(keyAnimation, 0, 0.0f);

            yield return new WaitForSeconds(keyAudioDelay);
            InsertKeySound();
            yield return new WaitForSeconds(doorOpenDelay);

            animatedDoorKey.SetActive(false);
            onUnlock.Invoke();
        }

        void LockedDoorSound()
        {
            AKAudioManager.instance.Play(lockedDoorSound);
        }

        void InsertKeySound()
        {
            AKAudioManager.instance.Play(insertKeySound);
        }
    }
}
