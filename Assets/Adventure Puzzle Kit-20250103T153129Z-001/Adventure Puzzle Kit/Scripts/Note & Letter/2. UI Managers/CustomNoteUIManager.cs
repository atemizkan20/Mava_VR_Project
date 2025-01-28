using UnityEngine;
using UnityEngine.UI;

namespace AdventurePuzzleKit.NoteSystem
{
    public class CustomNoteUIManager : MonoBehaviour
    {
        [Header("Audio Prompt UI")]
        [SerializeField] private GameObject audioPromptUI = null;

        [Header("Page Buttons UI")]
        [SerializeField] private GameObject pageButtons = null;
        [SerializeField] private GameObject nextButton = null;
        [SerializeField] private GameObject previousButton = null;

        [Header("Note Page UI's")]
        [SerializeField] private GameObject customNoteMainUI = null;
        [SerializeField] private Image customNotePageUI = null;

        [Header("Note Text UI's")]
        [SerializeField] private Text customNoteTextUI = null;

        public CustomNoteController noteController { get; set; } = null;

        public static CustomNoteUIManager instance;

        private void Awake()
        {
            if (instance == null) { instance = this; }
        }

        public void CustomNoteInitialize(Sprite pageImage, Vector2 pageScale, string noteText, Vector2 noteTextAreaScale, int textSize, 
            Font fontType, FontStyle fontStyle, Color fontColor)
        {
            DisplayPage(pageImage);
            DisableNoteDisplay(true);

            customNotePageUI.rectTransform.sizeDelta = pageScale;

            customNoteTextUI.text = noteText;

            customNoteTextUI.rectTransform.sizeDelta = noteTextAreaScale;

            customNoteTextUI.fontSize = textSize;
            customNoteTextUI.font = fontType;
            customNoteTextUI.fontStyle = fontStyle;
            customNoteTextUI.color = fontColor;

            customNoteMainUI.SetActive(true);
        }

        public void ShowPreviousButton(bool show)
        {
            previousButton.SetActive(show);
        }

        public void DisableNoteDisplay(bool active)
        {
            customNoteMainUI.SetActive(active);
        }

        public void DisplayPage(Sprite pageImage)
        {
            customNotePageUI.sprite = pageImage;
        }

        public void FillNoteText(string noteText)
        {
            customNoteTextUI.text = noteText;
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
