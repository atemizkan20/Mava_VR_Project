using UnityEngine;

namespace AdventurePuzzleKit
{
    [RequireComponent(typeof(Camera))]
    public class AKInteractor : MonoBehaviour
    {
        [Header("Raycast Features")]
        [SerializeField] private float interactDistance = 2.5f;
        private AKItem raycasted_obj;
        private Camera _camera;

        private const string pickupTag = "InteractiveObject";

        void Start()
        {
            _camera = GetComponent<Camera>();
        }

        private void Update()
        {
            if (Physics.Raycast(_camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f)), transform.forward, out RaycastHit hit, interactDistance))
            {
                var akItem = hit.collider.GetComponent<AKItem>();
                if (akItem != null && hit.collider.CompareTag(pickupTag))
                {
                    raycasted_obj = hit.collider.gameObject.GetComponent<AKItem>();
                    raycasted_obj.Highlight(true);
                    raycasted_obj.IsLooking(true);
                    HighlightCrosshair(true);
                }
                else
                {
                    ClearExaminable();
                }
            }
            else
            {
                ClearExaminable();
            }
            if (raycasted_obj != null)
            {
                if (Input.GetKeyDown(AKInputManager.instance.pickupKey))
                {
                    raycasted_obj.InteractionType();
                }
            }
        }

        private void ClearExaminable()
        {
            if (raycasted_obj != null)
            {
                raycasted_obj.IsLooking(false);
                raycasted_obj.Highlight(false);
                HighlightCrosshair(false);
                raycasted_obj = null;
            }
        }

        void HighlightCrosshair(bool on)
        {
            AKUIManager.instance.HighlightCrosshair(on);
        }
    }
}
