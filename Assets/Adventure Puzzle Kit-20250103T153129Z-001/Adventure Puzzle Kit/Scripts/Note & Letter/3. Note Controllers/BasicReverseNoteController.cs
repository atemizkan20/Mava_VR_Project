using System.Collections;
using UnityEngine;
using AdventurePuzzleKit.ExamineSystem;

namespace AdventurePuzzleKit.NoteSystem
{
    public class BasicReverseNoteController : MonoBehaviour
    {
        [SerializeField] private bool _isReadable = true;

        [Tooltip("Overall X, Y scale of the note")]
        [SerializeField] private Vector2 pageScale = new Vector2(900, 900);

        [SerializeField] private bool hasMultPages = false;

        [Tooltip("Add the image from your project panel to this slot, as a note background")]
        [SerializeField] private Sprite[] pageImages = null;

        [TextArea(4, 8)] [SerializeField] private string[] noteReverseText = null;

        [Tooltip("This is the scale of where the text is applied, usually slightly smaller than the object below")]
        [SerializeField] private Vector2 noteTextAreaScale = new Vector2(1045, 300);
        [Tooltip("This is the scale of background image for the reverse text")]
        [SerializeField] private Vector2 customTextBGScale = new Vector2(1160, 300);
        [Tooltip("This is the background colour of the reverse text - Make sure the alpha value is set to 1")]
        [SerializeField] private Color customTextBGColor = Color.white;

        [SerializeField] private int textSize = 25;
        [SerializeField] private Font fontType = null;
        [SerializeField] private FontStyle fontStyle = FontStyle.Normal;
        [Tooltip("Make sure the alpha value is set to 1")]
        [SerializeField] private Color fontColor = Color.black;

        [SerializeField] private bool _allowAudioPlayback = false;
        [SerializeField] private bool playOnOpen = false;
        [SerializeField] private Sound noteReadAudio = null;
        [SerializeField] private Sound noteFlipAudio = null;

        [SerializeField] private bool _isNoteTrigger = false;
        [SerializeField] private GameObject triggerObject = null;

        private BoxCollider boxCollider;
        private bool canReverse;
        private bool canClick;
        private bool isNoteActive;
        private AKInteractor notesRaycastScript;
        private BasicReverseNoteUIManager noteUIController;
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
            BasicReverseNoteUIManager.instance.noteController = gameObject.GetComponent<BasicReverseNoteController>();
            noteUIController = BasicReverseNoteUIManager.instance;
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
            noteUIController.BasicReverseInitialize(pageImages[pageNum], noteTextAreaScale, noteReverseText[pageNum], textSize, fontStyle, fontType, 
                fontColor, pageScale, customTextBGScale, customTextBGColor);

            PlayFlipAudio();
            isNoteActive = true;

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
            noteUIController.DisableReverseNoteDisplay(false);
            AKDisableManager.instance.DisablePlayerDefault(false, false, false);
            notesRaycastScript.enabled = true;
            boxCollider.enabled = true;
            isNoteActive = false;
            canReverse = false;
            isReadable = true;
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
            if (pageNum < pageImages.Length - 1)
            {
                pageNum++;
                noteUIController.DisplayPage(pageImages[pageNum]);
                noteUIController.FillReverseText(noteReverseText[pageNum]);
                PlayFlipAudio();
                EnabledButtons();

                if (pageNum >= pageImages.Length - 1)
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
                noteUIController.DisplayPage(pageImages[pageNum]);
                noteUIController.FillReverseText(noteReverseText[pageNum]);
                PlayFlipAudio();
                EnabledButtons();

                if (pageNum < 1)
                {
                    noteUIController.ShowPreviousButton(false);
                }
            }
        }

        public void ReverseNoteAction()
        {
            if (isNoteActive)
            {
                canReverse = !canReverse;

                if (canReverse)
                {
                    noteUIController.ShowReverseNotePanel(true);
                }
                else
                {
                    noteUIController.ShowReverseNotePanel(false);
                }
            }
        }

        void ResetNote()
        {
            noteUIController.ShowPreviousButton(false);
            noteUIController.ShowNextButton(true);
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
                print("BasicReverseNoteController on" + " " + gameObject.name + ": Add a reference to the note flip sound Scriptable to the inspector");
            }

            if (allowAudioPlayback && noteReadAudio == null)
            {
                print("BasicReverseNoteController on" + " " + gameObject.name + ": Add a reference to the sound Scriptable to the inspector");
            }
        }
    }
}
