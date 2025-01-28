using UnityEngine;

namespace ChessPuzzleSystem
{
    public class ChessPowerExample : MonoBehaviour
    {
        [SerializeField] private Renderer[] thisMaterial = null;

        public void PowerLights()
        {
            foreach (Renderer emissiveMaterial in thisMaterial)
            {
                emissiveMaterial.material.EnableKeyword("_EMISSION");
            }
        }
    }
}
