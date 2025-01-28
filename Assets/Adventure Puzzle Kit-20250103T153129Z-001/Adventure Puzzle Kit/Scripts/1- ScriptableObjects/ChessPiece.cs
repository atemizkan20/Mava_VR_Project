using UnityEngine;

namespace AdventurePuzzleKit.ChessSystem
{
    [CreateAssetMenu(menuName = "Fuse")]
    public class ChessPiece : ScriptableObject
    {
        [SerializeField] private Sprite chessSprite = null;

        [SerializeField] private GameObject chessPrefab = null;

        public GameObject ChessPrefab
        {
            get { return chessPrefab; }
        }

        public Sprite ChessSprite
        {
            get { return chessSprite; }
        }
    }
}
