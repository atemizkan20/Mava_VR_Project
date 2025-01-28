using AdventurePuzzleKit.ExamineSystem;
using System.Collections;
using UnityEngine;

namespace AdventurePuzzleKit.NoteSystem
{
    public class BasicNoteController : MonoBehaviour
    {
        [SerializeField] private bool _isReadable = true;

        [Tooltip("Overal X, Y scale of the note")]
        [SerializeField] private Vector2 noteScale = new Vector2(900, 900);

        [SerializeField] private bool hasMultPages = false;

        [Tooltip("Add the image from your project panel to this slot, as a note background")]
        [Space(5)] [SerializeField] private Sprite[] pageImages = null;

        [SerializeField] private bool _allowAudioPlayback = false;
        [SerializeField] private bool playOnOpen = false;
        [SerializeField] private Sound noteReadAudio = null;
        [SerializeField] private Sound noteFlipAudio = null;

        [SerializeField] private GameObject triggerObject = null;
        [SerializeField] private bool _isNoteTrigger = false;

        private BasicNoteUIManager noteUIController;
        private AKInteractor notesRaycastScript;
        private BoxCollider boxCollider;
        private bool canClick;
        private bool audioPlaying;
        private int pageNum = 0;

        public bool isReadable
        {
            get { return _isReadable; }
            set { _isReadable = value; }}

        public bool allowAudioPlayback
        {
            get { return _allowAudioPlayback; }
            set { _allowAudioPlayback = value; }}

        public bool isNoteTrigger
        {
            get { return _isNoteTrigger; }
            set { _isNoteTrigger = value; }}

        private void Awake()
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
            BasicNoteUIManager.instance.noteController = gameObject.GetComponent<BasicNoteController>();
            noteUIController = BasicNoteUIManager.instance;
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
            noteUIController.BasicNoteInitialize(pageImages[pageNum], noteScale);
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
            isReadable = true;
            ResetNote();
            enabled = false;

            if (hasMultPages)
            {
                noteUIController.ShowPageButtons(false);
                noteUIController.ShowAudioPrompt(false);
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
                PlayFlipAudio();
                EnabledButtons();
               
                if (pageNum < 1)
                {
                    noteUIController.ShowPreviousButton(false);
                }
            }
        }

        void EnabledButtons()
        {
            noteUIController.ShowPreviousButton(true);
            noteUIController.ShowNextButton(true);
        }

        void ResetNote()
        {
            noteUIController.ShowPreviousButton(false);
            noteUIController.ShowNextButton(true);
            pageNum = 0;
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

        void PlayFlipAudio()
        {
            AKAudioManager.instance.Play(noteFlipAudio);
        }

        public void RepeatReadingAudio()
        {
            StopAudio();
            PlayAudio();
        }

        private void PlayAudio()
        {
            AKAudioManager.instance.Play(noteReadAudio);
            audioPlaying = true;
        }

        private void StopAudio()
        {
            AKAudioManager.instance.StopPlaying(noteReadAudio);
            audioPlaying = false;
        }

        private void PauseAudio()
        {
            AKAudioManager.instance.PausePlaying(noteReadAudio);
            audioPlaying = false;
        }

        void DebugReferenceCheck()
        {
            if (noteFlipAudio == null)
            {
                print("BasicNoteController on" + " " + gameObject.name + ": Add a reference to the note flip sound Scriptable to the inspector");
            }

            if (allowAudioPlayback && noteReadAudio == null)      
            {
                print("BasicNoteController on" + " " + gameObject.name + ": Add a reference to the sound Scriptable to the inspector");
            }
        }
    }
}
