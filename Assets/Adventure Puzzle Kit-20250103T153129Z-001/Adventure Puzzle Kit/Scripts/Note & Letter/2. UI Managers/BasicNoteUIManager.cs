using UnityEngine;
using UnityEngine.UI;

namespace AdventurePuzzleKit.NoteSystem
{
    public class BasicNoteUIManager : MonoBehaviour
    {
        [Header("Audio Prompt UI")]
        [SerializeField] private GameObject audioPromptUI = null;

        [Header("Page Buttons UI")]
        [SerializeField] private GameObject pageButtons = null;
        [SerializeField] private GameObject nextButton = null;
        [SerializeField] private GameObject previousButton = null;

        [Header("Default Note UI")]
        [SerializeField] private GameObject basicNoteMainUI = null;
        [SerializeField] private Image basicNotePageUI = null;

        public BasicNoteController noteController { get; set; }

        public static BasicNoteUIManager instance;

        private void Awake()
        {
            if (instance == null) { instance = this; }
        }

        public void BasicNoteInitialize(Sprite pageImage, Vector2 noteScale)
        {
            DisplayPage(pageImage);
            basicNotePageUI.rectTransform.sizeDelta = noteScale;
            DisableNoteDisplay(true);
        }

        public void DisableNoteDisplay(bool active)
        {
            basicNoteMainUI.SetActive(active);
        }

        public void DisplayPage(Sprite pageImage)
        {
            basicNotePageUI.sprite = pageImage;
        }

        public void ShowPreviousButton(bool show)
        {
            previousButton.SetActive(show);
        }

        public void ShowNextButton(bool show)
        {
            nextButton.SetActive(show);
        }

        public void ShowPageButtons(bool show)
        {
            pageButtons.SetActive(show);
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
