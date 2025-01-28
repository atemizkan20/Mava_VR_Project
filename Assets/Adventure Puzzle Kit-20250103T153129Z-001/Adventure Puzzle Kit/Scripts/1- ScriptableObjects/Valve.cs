using UnityEngine;

namespace AdventurePuzzleKit.ValveSystem
{
    [CreateAssetMenu(menuName = "Valve")]
    public class Valve : ScriptableObject
    {
        [SerializeField] private Sprite _valveSprite = null;

        public Sprite ValveSprite
        {
            get { return _valveSprite; }
        }
    }
}
