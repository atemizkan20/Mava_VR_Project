using UnityEngine;

namespace AdventurePuzzleKit.SafeSystem
{
    [System.Serializable]
    public class SafeAudioClips
    {
        [Header("Sounds")]
        public Sound interactSound;
        public Sound boltUnlockSound;
        public Sound handleSpinSound;
        public Sound doorOpenSound;
        public Sound lockRattleSound;
        public Sound safeClickSound;

        public void PlaySafeClickSound()
        {
            AKAudioManager.instance.Play(safeClickSound);
        }

        public void PlayRattleSound()
        {
            AKAudioManager.instance.Play(lockRattleSound);
        }

        public void PlayDoorOpenSound()
        {
            AKAudioManager.instance.Play(doorOpenSound);
        }

        public void PlayHandleSpinSound()
        {
            AKAudioManager.instance.Play(handleSpinSound);
        }

        public void PlayBoltUnlockSound()
        {
            AKAudioManager.instance.Play(boltUnlockSound);
        }

        public void PlayInteractSound()
        {
            AKAudioManager.instance.Play(interactSound);
        }
    }
}
