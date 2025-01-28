using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AdventurePuzzleKit.ChessSystem
{
    public class ChessSlotWidget : MonoBehaviour
    {
        private ChessPiece _chessPiece;

        public CPFuseBoxInteractable FuseBox { get; set; }

        [SerializeField] private Button _button = null;
        [SerializeField] private Image _iconImage = null;

        public void SetPiece(ChessPiece chessPiece)
        {
            _chessPiece = chessPiece;

            _iconImage.sprite = _chessPiece != null ? _chessPiece.ChessSprite : null;
            _iconImage.enabled = _chessPiece != null;
            _button.interactable = _chessPiece != null;
        }

        // Add as a persistant callback in the Button component in the inspector. Or you can assign it to the button in the start method.
        public void PlaceFuseInFuseBox()
        {
            if (FuseBox == null || _chessPiece == null)
            {
                EventSystem.current.SetSelectedGameObject(null);
                return;
            }

            FuseBox.PlaceFuse(_chessPiece);
            AKUIManager.instance.DisableInventoryFusebox();
        }
    }
}
