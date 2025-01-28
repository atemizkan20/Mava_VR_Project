using AdventurePuzzleKit.ExamineSystem;
using AdventurePuzzleKit.FlashlightSystem;
using AdventurePuzzleKit.GasMaskSystem;
using AdventurePuzzleKit.GeneratorSystem;
using AdventurePuzzleKit.KeypadSystem;
using AdventurePuzzleKit.PadlockSystem;
using AdventurePuzzleKit.PhoneSystem;
using AdventurePuzzleKit.ThemedKey;
using AdventurePuzzleKit.ChessSystem;
using AdventurePuzzleKit.SafeSystem;
using AdventurePuzzleKit.FuseSystem;
using AdventurePuzzleKit.NoteSystem;
using AdventurePuzzleKit.LeverSystem;
using AdventurePuzzleKit.DoorSystem;
using AdventurePuzzleKit.ValveSystem;
using UnityEngine;

namespace AdventurePuzzleKit
{
    public class AKItem : MonoBehaviour
    {
        //Primary System Type
        public SystemType _systemType = SystemType.None;
        public enum SystemType { None, FlashlightSys, GeneratorSys, ExamineSys, NoteSys, GasMaskSys, KeypadSys, ThemedKeySys, PhoneSys, PadlockSys, ChessSys, SafeSys, buttonDoorSys, FuseBoxSys, LeverSys, ValveSys }

        //Secondary System Type - Only for Generator
        [SerializeField] private SecondarySystemType _secondarySystemType = SecondarySystemType.None;
        private enum SecondarySystemType { None, GeneratorSys }

        [Tooltip("This is to add a highlight name when looking at objects, ONLY if you're not using Examine Sys as Primary Type")]
        [SerializeField] private bool _showNameHighlight = false;
        [SerializeField] private bool _showNameHelpPrompt = false;
        [SerializeField] private string itemName = null;

        [SerializeField] private bool _isLookingAtObject = false;

        private FlashlightItem _flashlightItem;
        private ExaminableItem _examinableItem;
        private GeneratorItem _generatorItem;
        private NoteTypeSelector _noteItem;
        private GasMaskItem _gasMaskItem;
        private KeypadItem _keypadItem;
        private TKItem _themedKeyItem;
        private PhoneItem _phoneItem;
        private PadlockItem _padlockItem;
        private CPItem _chessItemController;
        private SafeItem _safeItem;
        private ButtonDoorController _buttonDoorController;
        private FuseItem _fuseboxItem;
        private LeverItem _leverItem;
        private ValveItem _valveItem;

        public bool showNameHighlight
        {
            get { return _showNameHighlight; }
            set { _showNameHighlight = value; }
        }

        public bool showNameHelpPrompt
        {
            get { return _showNameHelpPrompt; }
            set { _showNameHelpPrompt = value; }
        }

        public bool isLookingAtObject
        {
            get { return _isLookingAtObject; }
            set { _isLookingAtObject = value; }
        }

        private void Start()
        {
            switch (_systemType)
            {
                case SystemType.FlashlightSys: _flashlightItem = GetComponent<FlashlightItem>(); break;
                case SystemType.GeneratorSys: _generatorItem = GetComponent<GeneratorItem>(); break;
                case SystemType.ExamineSys: _examinableItem = GetComponent<ExaminableItem>(); break;
                case SystemType.NoteSys: _noteItem = GetComponent<NoteTypeSelector>(); break;
                case SystemType.GasMaskSys: _gasMaskItem = GetComponent<GasMaskItem>(); break;
                case SystemType.KeypadSys: _keypadItem = GetComponent<KeypadItem>(); break;
                case SystemType.ThemedKeySys: _themedKeyItem = GetComponent<TKItem>(); break;
                case SystemType.PhoneSys: _phoneItem = GetComponent<PhoneItem>(); break;
                case SystemType.PadlockSys: _padlockItem = GetComponent<PadlockItem>(); break;
                case SystemType.ChessSys: _chessItemController = GetComponent<CPItem>(); break;
                case SystemType.SafeSys: _safeItem = GetComponent<SafeItem>(); break;
                case SystemType.buttonDoorSys: _buttonDoorController = GetComponent<ButtonDoorController>(); break;
                case SystemType.FuseBoxSys: _fuseboxItem = GetComponent<FuseItem>(); break;
                case SystemType.LeverSys: _leverItem = GetComponent<LeverItem>(); break;
                case SystemType.ValveSys: _valveItem = GetComponent<ValveItem>(); break;
            }
            switch (_secondarySystemType)
            {
                case SecondarySystemType.GeneratorSys: _generatorItem = GetComponent<GeneratorItem>(); break;
            }
        }

        public void MainHighlight(bool isHighlighted)
        {
            if (showNameHighlight)
            {
                if (isHighlighted)
                {
                    AKUIManager.instance.SetHighlightName(itemName, isHighlighted, showNameHelpPrompt);
                }
                else
                {
                    AKUIManager.instance.SetHighlightName(null, false, false);
                }
            }
        }

        public void Highlight(bool highlight)
        {
            switch (_systemType)
            {
                case SystemType.ExamineSys:
                    if (highlight)
                    {
                        _examinableItem.ItemHighlight(true);
                        switch (_secondarySystemType)
                        {
                            case SecondarySystemType.GeneratorSys: _generatorItem.ShowObjectStats(true);
                                break;
                        }
                    }
                    else
                    {
                        _examinableItem.ItemHighlight(false);
                        switch (_secondarySystemType)
                        {
                            case SecondarySystemType.GeneratorSys: _generatorItem.ShowObjectStats(false);
                                break;
                        }
                    }
                    break;
                case SystemType.GeneratorSys:
                    if (highlight)
                    {
                        _generatorItem.ShowObjectStats(true);
                    }
                    else
                    {
                        _generatorItem.ShowObjectStats(false);
                    }
                    break;
            }

            if (showNameHighlight)
            {
                if (highlight)
                {
                    MainHighlight(true);
                }
                else
                {
                    MainHighlight(false);
                }
            }
        }

        public void IsLooking(bool looking)
        {
            isLookingAtObject = looking;
        }

        public void InteractionType()
        {
            if(isLookingAtObject)
            {
                switch (_systemType)
                {
                    case SystemType.FlashlightSys: _flashlightItem.ObjectInteract(); break;
                    case SystemType.GeneratorSys: _generatorItem.ObjectInteract(); break;
                    case SystemType.ExamineSys: ExamineLogic(); break;
                    case SystemType.NoteSys: _noteItem.DisplayNotes(); break;
                    case SystemType.GasMaskSys: _gasMaskItem.ObjectInteract(); break;
                    case SystemType.KeypadSys: _keypadItem.ShowKeypad(); break;
                    case SystemType.ThemedKeySys: _themedKeyItem.ObjectInteract(); break;
                    case SystemType.PhoneSys: _phoneItem.ShowKeypad(); break;
                    case SystemType.PadlockSys: _padlockItem.ObjectInteract(); break;
                    case SystemType.ChessSys: _chessItemController.ObjectInteract(); break;
                    case SystemType.SafeSys: _safeItem.ShowSafeLock(); break;
                    case SystemType.buttonDoorSys: _buttonDoorController.PlayAnimation(); break;
                    case SystemType.FuseBoxSys: _fuseboxItem.ObjectInteract(); break;
                    case SystemType.LeverSys: _leverItem.ObjectInteract(); break;
                    case SystemType.ValveSys: _valveItem.ObjectInteract(); break;
                }
            }
        }

        void ExamineLogic()
        {
            switch (_secondarySystemType)
            {
                case SecondarySystemType.GeneratorSys: _generatorItem.ShowObjectStats(false);
                    break;
            }
            _examinableItem.ExamineObject();
        }
    }
}
