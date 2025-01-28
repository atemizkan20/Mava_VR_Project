using UnityEngine;

namespace AdventurePuzzleKit.ChessSystem
{
    public class CPFuseCollectable : MonoBehaviour
    {
        [Header("Chess Piece ScriptableObject")]
        [SerializeField] private ChessPiece chessPieceScriptable = null;

        [Header("Pickup Audio Clip")]
        [SerializeField] private Sound pickupSound = null;

        public void PickupChessPiece()
        {
            CPInventory.instance.AddChessPiece(chessPieceScriptable);

            AKAudioManager.instance.Play(pickupSound);
            gameObject.SetActive(false);
        }
    }
}
