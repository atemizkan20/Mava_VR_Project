using UnityEngine;

namespace AdventurePuzzleKit.KeypadSystem
{
    public class KeypadItem : MonoBehaviour
    {
        [SerializeField] private KeypadController _keypadController = null;
        public void ShowKeypad()
        {
            _keypadController.ShowKeypad();
        }
    }
}
