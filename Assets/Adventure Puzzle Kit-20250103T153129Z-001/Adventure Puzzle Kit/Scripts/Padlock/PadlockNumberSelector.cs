using UnityEngine;
using UnityEngine.EventSystems;

namespace AdventurePuzzleKit.PadlockSystem
{
    public class PadlockNumberSelector : MonoBehaviour, IPointerClickHandler
    {
        [Header("Padlock Row")]
        [SerializeField] private PadlockRow selectedRow = PadlockRow.row1;
        private enum PadlockRow { row1, row2, row3, row4 }

        private int spinnerNumber;
        private int spinnerLimit;
        private PadlockController _padlockController;

        private void Awake()
        {
            spinnerNumber = 1;
            spinnerLimit = 9;
        }

        public void UpdatePadlockController(PadlockController newController)
        {
            _padlockController = newController;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            RotateSpinner();
            UpdatePadlockController();
            _padlockController.CheckCombination();
        }

        void RotateSpinner()
        {
            spinnerNumber = (spinnerNumber % spinnerLimit) + 1;
            transform.Rotate(0, 0, transform.rotation.z + 40);
            _padlockController.SpinSound();
        }

        void UpdatePadlockController()
        {
            int updatedRowValue = spinnerNumber;
            switch (selectedRow)
            {
                case PadlockRow.row1:
                    _padlockController.combinationRow1 = updatedRowValue;
                    break;
                case PadlockRow.row2:
                    _padlockController.combinationRow2 = updatedRowValue;
                    break;
                case PadlockRow.row3:
                    _padlockController.combinationRow3 = updatedRowValue;
                    break;
                case PadlockRow.row4:
                    _padlockController.combinationRow4 = updatedRowValue;
                    break;
            }
        }

        //transform.Rotate(0, 0, transform.rotation.z + 40);
        //_padlockController.SpinSound();
        //Rotate();

        /*void Rotate()
        {
            if (spinnerNumber <= spinnerLimit - 1)
            {
                spinnerNumber++;
            }

            else
            {
                spinnerNumber = 1;
            }

            switch (selectedRow)
            {
                case PadlockRow.row1:
                    _padlockController.combinationRow1 = spinnerNumber;
                    break;
                case PadlockRow.row2:
                    _padlockController.combinationRow2 = spinnerNumber;
                    break;
                case PadlockRow.row3:
                    _padlockController.combinationRow3 = spinnerNumber;
                    break;
                case PadlockRow.row4:
                    _padlockController.combinationRow4 = spinnerNumber;
                    break;
            }
            _padlockController.CheckCombination();
        }*/
    }
}


