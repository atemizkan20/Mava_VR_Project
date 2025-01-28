using UnityEngine;

namespace AdventurePuzzleKit.FlashlightSystem
{
    public class FlashlightMovement : MonoBehaviour
    {
        private Vector3 v3Offset;
        private Transform followTransform;
        private Transform myTransform;

        [SerializeField] private float _speed = 3.0f;
        public float speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        void Start()
        {
            myTransform = transform;
            followTransform = Camera.main.transform;
            v3Offset = myTransform.position - followTransform.position;
        }

        void Update()
        {
            transform.position = followTransform.position + v3Offset;
            transform.rotation = Quaternion.Slerp(myTransform.rotation, followTransform.rotation, speed * Time.deltaTime);
        }
    }
}
