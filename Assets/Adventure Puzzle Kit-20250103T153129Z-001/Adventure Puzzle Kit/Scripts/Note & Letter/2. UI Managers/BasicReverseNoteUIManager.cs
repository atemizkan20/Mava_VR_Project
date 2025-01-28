using UnityEngine;
using UnityEngine.UI;

namespace AdventurePuzzleKit.NoteSystem
{
    public class BasicReverseNoteUIManager : MonoBehaviour
    {
        [Header("Audio Prompt UI")]
        [SerializeField] private GameObject audioPromptUI = null;

        [Header("Page Buttons UI")]
        [SerializeField] private GameObject pageButtons = null;
        [SerializeField] private GameObject nextButton = null;
        [SerializeField] private GameObject previousButton = null;

        [Header("Reverse Note Main UI's")]
        [SerializeField] private GameObject reverseNoteMainUI = null;
        [SerializeField] private Image reverseNotePageUI = null;

        [Header("Reverse Note Text UI's")]
        [SerializeField] private GameObject reverseNoteTextPanelUI = null;
        [SerializeField] private Image reverseNoteTextImage = null;
        [SerializeField] private Text reverseNoteTextUI = null;

        public BasicReverseNoteController noteController { get; set; }

        public static BasicReverseNoteUIManager instance;

        private void Awake()
        {
            if (instance == null) { instance = this; }
        }

        public void BasicReverseInitialize(Sprite pageImage, Vector2 textAreaScale, string noteText, int textSize, FontStyle fontStyle, Font fontType, 
            Color fontColor, Vector2 pageScale, Vector2 customTextBGScale, Color customTextBGColor)
        {
            DisplayPage(pageImage);
            DisableNoteDisplay(true);

            reverseNotePageUI.rectTransform.sizeDelta = pageScale;

            reverseNoteTextImage.rectTransform.sizeDelta = customTextBGScale;
            reverseNoteTextImage.color = customTextBGColor;

            reverseNoteTextUI.rectTransform.sizeDelta = textAreaScale;
            reverseNoteTextUI.text = noteText;
            reverseNoteTextUI.fontSize = textSize;
            reverseNoteTextUI.fontStyle = fontStyle;
            reverseNoteTextUI.font = fontType;
            reverseNoteTextUI.color = fontColor;
        }

        public void DisableNoteDisplay(bool active)
        {
            reverseNoteMainUI.SetActive(active);
        }

        public void DisableReverseNoteDisplay(bool active)
        {
            reverseNoteTextPanelUI.SetActive(active);
        }

        public void DisplayPage(Sprite pageImage)
        {
            reverseNotePageUI.sprite = pageImage;
        }

        public void FillReverseText(string textString)
        {
            reverseNoteTextUI.text = textString;
        }

        public void ShowReverseNotePanel(bool show)
        {
            reverseNoteTextPanelUI.SetActive(show);
        }

        public void ShowPreviousButton(bool show)
        {
            previousButton.SetActive(show);
        }

        public void ShowNextButton(bool show)
        {
            nextButton.SetActive(show);
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
