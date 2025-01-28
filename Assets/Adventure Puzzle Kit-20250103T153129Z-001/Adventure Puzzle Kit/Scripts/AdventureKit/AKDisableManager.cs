using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.ImageEffects;
using UnityStandardAssets.Utility;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace AdventurePuzzleKit
{
    public class AKDisableManager : MonoBehaviour
    {
        [Header("Main Camera Fields")]
        [SerializeField] private AKInteractor akInteractor = null;
        [SerializeField] private BlurOptimized blur = null;

        [Header("First Person Fields")]
        [SerializeField] private bool isFirstPerson = false;
        [SerializeField] private FirstPersonController player = null;

        [Header("Third Person Fields")]
        [SerializeField] private bool isThirdPerson = false;
        public ThirdPersonUserControl thirdPersonController = null;
        public SimpleMouseRotator thirdPersonRotator = null;

        [Header("Post Processing")]
        [SerializeField] private Volume _postProcessingVolume = null;
        private DepthOfField _dof;

        public static AKDisableManager instance;

        void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }

            if (isThirdPerson)
            {
                AKUIManager.instance.ShowCursor(false);
            }

            _postProcessingVolume.profile.TryGet(out _dof);
        }

        public void DisablePlayerDefault(bool disable, bool isInteracting, bool isExamine)
        {
            if (disable)
            {
                akInteractor.enabled = false;
                AKUIManager.instance.ShowCursor(true);
                AKUIManager.instance.isInteracting = isInteracting; //true
                AKUIManager.instance.ShowCrosshair(false);
                AKUIManager.instance.ToggleNameHighlight(false);

                if (isExamine)
                {
                    blur.enabled = true;

                    _dof.active = true;
                    _dof.focusDistance.value = 0.1f;
                }

                if (isFirstPerson)
                {
                    player.enabled = false;
                }

                if (isThirdPerson)
                {
                    thirdPersonController.enabled = false;
                    thirdPersonRotator.enabled = false;
                }
            }

            else
            {
                akInteractor.enabled = true;
                AKUIManager.instance.ShowCursor(false);
                AKUIManager.instance.isInteracting = isInteracting; //false
                AKUIManager.instance.ShowCrosshair(true);
                AKUIManager.instance.ToggleNameHighlight(true);

                if (isExamine)
                {
                    blur.enabled = false;
                    _dof.active = false;
                    _dof.focusDistance.value = 10.0f;
                }

                if (isFirstPerson)
                {
                    player.enabled = true;
                }

                if (isThirdPerson)
                {
                    thirdPersonController.enabled = true;
                    thirdPersonRotator.enabled = true;
                }
            }
        }
    }
}