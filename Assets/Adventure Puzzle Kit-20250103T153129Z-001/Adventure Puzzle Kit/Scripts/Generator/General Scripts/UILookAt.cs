using UnityEngine;

namespace GeneratorSystem
{
    public class UILookAt : MonoBehaviour
    {
        private Transform player;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player").transform;
        }

        private void OnEnable()
        {
            transform.LookAt(2 * transform.position - player.position);
        }
    }
}
