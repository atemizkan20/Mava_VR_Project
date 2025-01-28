using UnityEngine;

namespace AdventurePuzzleKit.LeverSystem
{
    public class LeverItem : MonoBehaviour
    {
        [SerializeField] private ObjectType _objectType = ObjectType.None;
        private enum ObjectType { None, Lever, TestButton, ResetButton }

        [Space(5)] [SerializeField] private int leverNumber = 1;

        //This is the name of the handle animation, which is usually a child of the object which has this script
        [Space(5)] [SerializeField] private string animationName = "Handle_Pull";
        private Animator handleAnimation;

        [Header("Controller Reference")]
        [SerializeField] private LeverSystemController _leverSystemController = null;

        private void Start()
        {
            handleAnimation = GetComponentInChildren<Animator>();
        }

        public void ObjectInteract()
        {
            switch (_objectType)
            {
                case ObjectType.Lever:
                    LeverNumber();
                    break;
                case ObjectType.TestButton:
                    LeverCheck();
                    break;
                case ObjectType.ResetButton:
                    LeverReset();
                    break;
            }
        }

        public void LeverNumber()
        {
            _leverSystemController.InitLeverPull(this, leverNumber);
        }

        public void HandleAnimation()
        {
            handleAnimation.Play(animationName, 0, 0.0f);
        }

        public void LeverReset()
        {
            _leverSystemController.LeverReset();
        }

        public void LeverCheck()
        {
            _leverSystemController.LeverCheck();
        }
    }
}
