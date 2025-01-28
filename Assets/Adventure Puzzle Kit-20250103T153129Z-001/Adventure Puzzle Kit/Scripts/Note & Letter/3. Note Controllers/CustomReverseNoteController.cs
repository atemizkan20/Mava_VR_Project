using AdventurePuzzleKit.ExamineSystem;
using System.Collections;
using UnityEngine;

namespace AdventurePuzzleKit.NoteSystem
{
    public class CustomReverseNoteController : MonoBehaviour
    {
        [SerializeField] private bool _isReadable = true;

        [SerializeField] private Sprite pageImage = null;
        [SerializeField] private Vector2 pageScale = new Vector2(900, 900);

        [SerializeField] private bool hasMultPages = false;
        [TextArea(4, 8)] [SerializeField] private string[] noteReverseText = null;

        [SerializeField] private Vector2 mainTextAreaScale = new Vector2(495, 795);
        [SerializeField] private int mainTextSize = 25;
        [SerializeField] private Font mainFontType = null;
        [SerializeField] private FontStyle mainFontStyle = FontStyle.Normal;
        [SerializeField] private Color mainFontColor = Color.black;

        [SerializeField] private Color flipTextBGColor = Color.white;
        [SerializeField] private Vector2 flipTextAreaScale = new Vector2(1045, 300);
        [SerializeField] private Vector2 flipTextBGScale = new Vector2(1160, 300);
        [SerializeField] private int flipTextSize = 25;
        [SerializeField] private Font flipFontType = null;
        [SerializeField] private FontStyle flipFontStyle = FontStyle.Normal;
        [SerializeField] private Color flipFontColor = Color.black;

        [SerializeField] private bool _allowAudioPlayback = false;
        [SerializeField] private bool playOnOpen = false;
        [SerializeField] private Sound noteReadAudio = null;
        [SerializeField] private Sound noteFlipAudio = null;

        [SerializeField] private bool _isNoteTrigger = false;
        [SerializeField] private GameObject triggerObject = null;

        private bool canReverse;
        private bool canClick;
        private bool isNoteActivate;
        private BoxCollider boxCollider;
        private AKInteractor notesRaycastScript;
        private CustomReverseNoteUIManager noteUIController;
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
            CustomReverseNoteUIManager.instance.noteController = gameObject.GetComponent<CustomReverseNoteController>();
            noteUIController = CustomReverseNoteUIManager.instance;
            StartCoroutine(WaitTime());
            AKDisableManager.instance.DisablePlayerDefault(true, true, false);
            boxCollider.enabled = false;
            notesRaycastScript.enabled = false;

            if (pageNum <= 1)
            {
                noteUIController.ShowPreviousButton(false);
            }

            if (hasMultPages)
            {
                noteUIController.ShowPageButtons(true);
            }

            AKUIManager.instance.SetHighlightName(null, false, false);
            noteUIController.ReverseCustomInitialiseMainNote(pageImage, pageScale, noteReverseText[pageNum], mainTextAreaScale, mainTextSize, 
                mainFontStyle, mainFontType, mainFontColor);
            noteUIController.ReverseCustomInitialiseFlipSide(flipTextBGColor, flipTextAreaScale, noteReverseText[pageNum], flipTextSize, flipFontType, 
                flipFontStyle, flipFontColor, flipTextBGScale);

            PlayFlipAudio();
            isNoteActivate = true;

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
            noteUIController.SetReverseNoteAction(false);
            AKDisableManager.instance.DisablePlayerDefault(false, false, false);
            notesRaycastScript.enabled = true;
            boxCollider.enabled = true;
            canReverse = false;
            isNoteActivate = false;
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

        public void ReverseNoteAction()
        {
            if (isNoteActivate)
            {
                canReverse = !canReverse;

                if (canReverse)
                {
                    noteUIController.SetReverseNoteAction(true);
                }
                else
                {
                    noteUIController.SetReverseNoteAction(false);
                }
            }
        }

        public void NextPage()
        {
            if (pageNum < noteReverseText.Length - 1)
            {
                pageNum++;
                noteUIController.FillNoteText(noteReverseText[pageNum]);
                EnabledButtons();
                PlayFlipAudio();

                if (pageNum >= noteReverseText.Length - 1)
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
                noteUIController.FillNoteText(noteReverseText[pageNum]);
                EnabledButtons();
                PlayFlipAudio();

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
            //ReverseCustomNoteUIManager.instance.audioPromptUI.SetActive(false);
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
                print("ReverseCustomNoteController on" + " " + gameObject.name + ": Add a reference to the note flip sound Scriptable to the inspector");
            }

            if (allowAudioPlayback)
            {
                if (noteReadAudio == null)
                {
                    print("ReverseCustomNoteController on" + " " + gameObject.name + ": Add a reference to the sound Scriptable to the inspector");
                }
            }
        }
    }
}
