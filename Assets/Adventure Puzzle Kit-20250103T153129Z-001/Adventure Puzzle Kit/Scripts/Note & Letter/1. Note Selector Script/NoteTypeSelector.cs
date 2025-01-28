using UnityEngine;

namespace AdventurePuzzleKit.NoteSystem
{
    public class NoteTypeSelector : MonoBehaviour
    {
        [Space(5)]
        [SerializeField] private UIType _NoteType = UIType.None;
        private enum UIType { None, Basic, BasicReverse, NormalCustom, ReverseCustom }

        private BasicNoteController basicNoteController;
        private BasicReverseNoteController basicReverseNoteController;
        private CustomNoteController normalCustomNoteController;
        private CustomReverseNoteController reverseCustomController;

        private void Awake()
        {
            basicNoteController = GetComponent<BasicNoteController>();
            basicReverseNoteController = GetComponent<BasicReverseNoteController>();
            normalCustomNoteController = GetComponent<CustomNoteController>();
            reverseCustomController = GetComponent<CustomReverseNoteController>();
        }

        public void DisplayNotes()
        {
            switch (_NoteType)
            {
                case UIType.Basic:
                    if (basicNoteController.isReadable)
                    {
                        basicNoteController.enabled = true;
                        basicNoteController.ShowNote();
                    }
                    break;
                case UIType.BasicReverse:
                    if (basicReverseNoteController.isReadable)
                    {
                        basicReverseNoteController.enabled = true;
                        basicReverseNoteController.ShowNote();
                    }
                    break;
                case UIType.NormalCustom:
                    if (normalCustomNoteController.isReadable)
                    {
                        normalCustomNoteController.enabled = true;
                        normalCustomNoteController.ShowNote();
                    }
                    break;
                case UIType.ReverseCustom:
                    if (reverseCustomController.isReadable)
                    {
                        reverseCustomController.enabled = true;
                        reverseCustomController.ShowNote();
                    }
                    break;
            }
        }
    }
}
