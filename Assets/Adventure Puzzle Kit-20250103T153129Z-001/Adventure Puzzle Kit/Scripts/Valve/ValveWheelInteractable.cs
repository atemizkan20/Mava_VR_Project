using UnityEngine;
using UnityEngine.Events;

namespace AdventurePuzzleKit.ValveSystem
{
    public class ValveWheelInteractable : MonoBehaviour
    {
        [Header("Valve Turn Parameters")]
        [Range(0, 500)] [SerializeField] private float valveTurnSpeed = 200f;
        [Range(0, 5)] [SerializeField] private float progressSpeedUI = 0.5f;

        [Header("Audio Clips")]
        [SerializeField] private Sound valveCreakSound = null;

        [Header("Valve Unity Events")]
        [SerializeField] private UnityEvent valveOpened = null;

        private float maxValveLimit = 0.99f;
        private bool playOnce = true;
        private bool isComplete = false;
        private AKItem _akItem;

        private void Awake()
        {
            _akItem = GetComponent<AKItem>();
        }

        private void Update()
        {
            if (_akItem.isLookingAtObject && !isComplete)
            {
                if (Input.GetKey(AKInputManager.instance.pickupKey))
                {
                    TurnValveWheel();
                }
                else
                {
                    ReleaseValveWheel();
                }
            }
        }

        void OnValveOpen()
        {
            //Deactive this script within the UnityEvents for better optimisation
            isComplete = true;
            gameObject.tag = "Untagged";
            valveOpened.Invoke();
        }

        void TurnValveWheel()
        {
            if (AKUIManager.instance.ValveProgressUI.fillAmount <= maxValveLimit)
            {
                AKUIManager.instance.SliderOpacity(true);
                gameObject.transform.Rotate(0, 0, (valveTurnSpeed) * Time.deltaTime, Space.Self);

                float valveProgress = progressSpeedUI * Time.deltaTime;
                AKUIManager.instance.UpdateValveProgress(valveProgress);

                if (playOnce)
                {
                    AKAudioManager.instance.Play(valveCreakSound);
                    playOnce = false;
                }

                if (AKUIManager.instance.ValveProgressUI.fillAmount >= maxValveLimit)
                {
                    AKAudioManager.instance.StopPlaying(valveCreakSound);
                    playOnce = true;
                    AKUIManager.instance.SliderOpacity(false);
                    OnValveOpen();
                }
            }
        }

        void ReleaseValveWheel()
        {
            AKUIManager.instance.ResetProgress();
            AKAudioManager.instance.StopPlaying(valveCreakSound);
            playOnce = true;
        }
    }
}
