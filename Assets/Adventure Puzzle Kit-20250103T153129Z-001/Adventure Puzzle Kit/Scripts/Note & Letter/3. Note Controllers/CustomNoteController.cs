using AdventurePuzzleKit.ExamineSystem;
using System.Collections;
using UnityEngine;

namespace AdventurePuzzleKit.NoteSystem
{
    public class CustomNoteController : MonoBehaviour
    {
        [SerializeField] private bool _isReadable = true;

        [SerializeField] private Sprite pageImage = null;
        [SerializeField] private Vector2 pageScale = new Vector2(900, 900);

        [SerializeField] private bool hasMultPages = false;
        [TextArea(4, 8)] [SerializeField] private string[] noteText = null;

        [SerializeField] private Vector2 noteTextAreaScale = new Vector2(495, 795);
        [SerializeField] private int textSize = 25;
        [SerializeField] private Font fontType = null;
        [SerializeField] private FontStyle fontStyle = FontStyle.Normal;
        [SerializeField] private Color fontColor = Color.black;

        [SerializeField] private bool _allowAudioPlayback = false;
        [SerializeField] private bool playOnOpen = false;
        [SerializeField] private Sound noteReadAudio = null;
        [SerializeField] private Sound noteFlipAudio = null;

        [SerializeField] private bool _isNoteTrigger = false;
        [SerializeField] private GameObject triggerObject = null;

        private CustomNoteUIManager noteUIController;
        private AKInteractor notesRaycastScript;
        private BoxCollider boxCollider;
        private bool canClick;
        private int pageNum = 0;
        private bool audioPlaying;

        public bool isReadable
        {
            get { return _isReadable; }
            set { _isReadable = value; }
        }

        public bool allowAudioPlayback
        {
            get { return _allowAudioPlayback; }
            set { _allowAudioPlayback = value; }
        }

        public bool isNoteTrigger
        {
            get { return _isNoteTrigger; }
            set { _isNoteTrigger = value; }
        }

        private void Start()
        {
            canClick = false;
            notesRaycastScript = Camera.main.GetComponent<AKInteractor>();
            boxCollider = GetComponent<BoxCollider>();
            DebugReferenceCheck();
        }

        private void Update()
        {
            if (canClick)
            {
                if (Input.GetKeyDown(AKInputManager.instance.closeNoteKey))
                {
                    CloseNote();
                }
            }
        }

        public void ShowNote()
        {
            CustomNoteUIManager.instance.noteController = gameObject.GetComponent<CustomNoteController>();
            noteUIController = CustomNoteUIManager.instance;
            StartCoroutine(WaitTime());
            AKDisableManager.instance.DisablePlayerDefault(true, true, false);
            notesRaycastScript.enabled = false;
            boxCollider.enabled = false;

            if (pageNum <= 1)
            {
                noteUIController.ShowPreviousButton(false);
            }

            if (hasMultPages)
            {
                noteUIController.ShowPageButtons(true);
            }

            AKUIManager.instance.SetHighlightName(null, false, false);
            noteUIController.CustomNoteInitialize(pageImage, pageScale, noteText[pageNum], noteTextAreaScale, textSize, fontType, fontStyle, fontColor);
            PlayFlipAudio();

            if (allowAudioPlayback)
            {
                noteUIController.ShowAudioPrompt(true);
                if (playOnOpen)
                {
                    PlayAudio();
                }
            }

            if (isNoteTrigger)
            {
                EnableTrigger(false);
            }
        }

        public void CloseNote()
        {
            noteUIController.DisableNoteDisplay(false);
            AKDisableManager.instance.DisablePlayerDefault(false, false, false);
            notesRaycastScript.enabled = true;
            boxCollider.enabled = true;
            ResetNote();
            enabled = false;

            if (hasMultPages)
            {
                noteUIController.ShowPageButtons(false);
            }

            if (playOnOpen || allowAudioPlayback)
            {
                StopAudio();
            }

            if (isNoteTrigger)
            {
                EnableTrigger(true);
            }
        }

        public void NextPage()
        {
            if (pageNum < noteText.Length - 1)
            {
                pageNum++;
                noteUIController.FillNoteText(noteText[pageNum]);
                PlayFlipAudio();
                EnabledButtons();

                if (pageNum >= noteText.Length - 1)
                {
                    noteUIController.ShowNextButton(false);
                }
            }
        }

        public void BackPage()
        {
            if (pageNum >= 1)
            {
                pageNum--;
                noteUIController.FillNoteText(noteText[pageNum]);
                PlayFlipAudio();
                EnabledButtons();

                if (pageNum < 1)
                {
                    noteUIController.ShowPreviousButton(false);
                }
            }
        }

        void ResetNote()
        {
            noteUIController.ShowPreviousButton(false);
            noteUIController.ShowNextButton(true);
            //CustomNoteUIManager.instance.audioPromptUI.SetActive(false);
            pageNum = 0;
        }

        void EnabledButtons()
        {
            noteUIController.ShowPreviousButton(true);
            noteUIController.ShowNextButton(true);
        }

        private void EnableTrigger(bool enable)
        {
            AKUIManager.instance.EnableInteractPrompt(enable);
            triggerObject.SetActive(enable);
        }

        IEnumerator WaitTime()
        {
            const float WaitTimer = 0.1f;
            yield return new WaitForSeconds(WaitTimer);
            canClick = true;
        }

        void PlayFlipAudio()
        {
            AKAudioManager.instance.Play(noteFlipAudio);
        }

        public void NoteReadingAudio()
        {
            if (!audioPlaying)
            {
                PlayAudio();
            }
            else
            {
                PauseAudio();
            }
        }

        public void RepeatReadingAudio()
        {
            StopAudio();
            PlayAudio();
        }

        public void PlayAudio()
        {
            AKAudioManager.instance.Play(noteReadAudio);
            audioPlaying = true;
        }

        public void StopAudio()
        {
            AKAudioManager.instance.StopPlaying(noteReadAudio);
            audioPlaying = false;
        }

        public void PauseAudio()
        {
            AKAudioManager.instance.PausePlaying(noteReadAudio);
            audioPlaying = false;
        }

        void DebugReferenceCheck()
        {
            if (noteFlipAudio == null)
            {
                print("CustomNoteController on" + " " + gameObject.name + ": Add a reference to the note flip sound Scriptable to the inspector");
            }

            if (allowAudioPlayback)
            {
                if (noteReadAudio == null)
                {
                    print("CustomNoteController on" + " " + gameObject.name + ": Add a reference to the sound Scriptable to the inspector");
                }
            }
        }
    }
}
