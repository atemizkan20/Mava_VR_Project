using UnityEngine;

namespace AdventurePuzzleKit.ThemedKey
{
    [CreateAssetMenu(menuName = "Key")]
    public class Key : ScriptableObject
    {
        [SerializeField] private Sprite keySprite = null;

        public Sprite _KeySprite
        {
            get { return keySprite; }
        }
    }
}
