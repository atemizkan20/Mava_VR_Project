using UnityEngine;
using UnityEngine.UI;

namespace AdventurePuzzleKit.NoteSystem
{
    public class CustomReverseNoteUIManager : MonoBehaviour
    {
        [Header("Audio Prompt UI")]
        [SerializeField] private GameObject audioPromptUI = null;

        [Header("Page Buttons UI")]
        [SerializeField] private GameObject pageButtons = null;
        [SerializeField] private GameObject nextButton = null;
        [SerializeField] private GameObject previousButton = null;

        [Header("Main Note Settings")]
        [SerializeField] private GameObject customReverseMainNoteUI = null;
        [SerializeField] private Image customReverseNotePageUI = null;
        [SerializeField] private Text customReverseNoteTextUI = null;

        [Header("Custom Reverse Pop-out Settings")]
        [SerializeField] private GameObject customReverseNoteTextPanelBG = null;
        [SerializeField] private Image customReverseNoteTextImage = null;
        [SerializeField] private Text customReverseFlipNoteTextUI = null;

        public CustomReverseNoteController noteController { get; set; }

        public static CustomReverseNoteUIManager instance;

        private void Awake()
        {
            if (instance == null) { instance = this; }
        }

        public void ReverseCustomInitialiseMainNote(Sprite pageImage, Vector2 pageScale, string noteReverseText, Vector2 mainTextAreaScale, int mainTextSize, 
            FontStyle mainFontStyle, Font mainFontType, Color mainFontColor)
        {
            DisplayPage(pageImage);
            DisableNoteDisplay(true);

            customReverseNotePageUI.rectTransform.sizeDelta = pageScale;
            customReverseNoteTextUI.text = noteReverseText;

            customReverseNoteTextUI.rectTransform.sizeDelta = mainTextAreaScale;
            customReverseNoteTextUI.fontSize = mainTextSize;
            customReverseNoteTextUI.fontStyle = mainFontStyle;
            customReverseNoteTextUI.font = mainFontType;
            customReverseNoteTextUI.color = mainFontColor;
        }

        public void ReverseCustomInitialiseFlipSide(Color flipTextBGColor, Vector2 flipTextAreaScale, string noteReverseText, int flipTextSize, Font flipFontType,
            FontStyle flipFontStyle, Color flipFontColor, Vector2 flipTextBGScale)
        {
            customReverseNoteTextImage.color = flipTextBGColor;

            customReverseFlipNoteTextUI.rectTransform.sizeDelta = flipTextAreaScale;
            customReverseFlipNoteTextUI.text = noteReverseText;

            customReverseFlipNoteTextUI.fontSize = flipTextSize;
            customReverseFlipNoteTextUI.font = flipFontType;
            customReverseFlipNoteTextUI.fontStyle = flipFontStyle;
            customReverseFlipNoteTextUI.color = flipFontColor;

            customReverseNoteTextImage.rectTransform.sizeDelta = flipTextBGScale;
        }

        public void DisplayPage(Sprite pageImage)
        {
            customReverseNotePageUI.sprite = pageImage;
        }

        public void FillNoteText(string noteText)
        {
            customReverseNoteTextUI.text = noteText;
            customReverseFlipNoteTextUI.text = noteText;
        }

        public void DisableNoteDisplay(bool active)
        {
            customReverseMainNoteUI.SetActive(active);
        }

        public void SetReverseNoteAction(bool active)
        {
            customReverseNoteTextPanelBG.SetActive(active);
        }

        public void ShowPageButtons(bool shouldShow)
        {
            if (shouldShow)
            {
                pageButtons.SetActive(true);
            }
            else
            {
                pageButtons.SetActive(false);
            }
        }

        public void ShowPreviousButton(bool show)
        {
            previousButton.SetActive(show);
        }

        public void ShowNextButton(bool show)
        {
            nextButton.SetActive(show);
        }

        public void ShowAudioPrompt(bool show)
        {
            audioPromptUI.SetActive(show);
        }

        public void PlayPauseAudio()
        {
            noteController.NoteReadingAudio();
        }

        public void RepeatAudio()
        {
            noteController.RepeatReadingAudio();
        }

        public void ReverseNoteButton()
        {
            noteController.ReverseNoteAction();
        }

        public void CloseButton()
        {
            noteController.CloseNote();
        }

        public void NextPage()
        {
            noteController.NextPage();
        }

        public void BackPage()
        {
            noteController.BackPage();
        }
    }
}
