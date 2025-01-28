using UnityEngine;

namespace AdventurePuzzleKit.PhoneSystem
{
    public class PhoneItem : MonoBehaviour
    {
        [SerializeField] private PhoneController _phoneController = null;
        public void ShowKeypad()
        {
            _phoneController.ShowKeypad();
        }
    }
}
