using UnityEngine;

namespace AdventurePuzzleKit.ChessSystem
{
    public class CPFuseBoxInteractable : MonoBehaviour
    {
        [Header("Fuse Box Type")]
        [SerializeField] private ChessPiece chessPieceScriptable = null;

        [Header("Started with a fuse?")]
        [SerializeField] private bool fusePlaced;
        [SerializeField] private ChessPiece starterFuseScriptable = null;

        [Header("Fuse Spawn Location")]
        [SerializeField] private Transform fuseLocation = null;

        [Header("Light Object")]
        [SerializeField] private Renderer fuseBoxLightRend = null;

        [Header("Power Manager")]
        [SerializeField] private CPPowerManager powerManager = null;

        [Header("Audio")]
        [SerializeField] private Sound insertFuseSound = null;

        private Vector3 spawnOffset = Vector3.zero; //Serialize this field if you wish to spawn a fuse with an offset further away from the spawn point
        private Quaternion fuseRotation = Quaternion.identity; //Serialize this field if you wish to spawn a fuse with an offset further away from the spawn point
        private GameObject spawnedFuse;
        private bool isPowered;
        private Material fuseBoxLightMaterial;

        public ChessPiece currentFuse { get; set; }

        //private CPFuseBoxInteractable fuseBoxController;

        private void Awake()
        {
            if (fusePlaced)
            {
                SpawnFuse(starterFuseScriptable);
            }

            fuseBoxLightMaterial = fuseBoxLightRend.material;
        }

        public void InteractFuseBox()
        {
            AKUIManager.instance.OpenInventoryFusebox(this);     
        }

        public void CheckFuseBox(ChessPiece fuseType)
        {
            bool wasPowered = isPowered;
            isPowered = fuseType == chessPieceScriptable;

            if (wasPowered != isPowered)
            {
                powerManager.UpdateFuseCount(isPowered);
            }
        }

        public void PlaceFuse(ChessPiece fuseType)
        {
            if (!fusePlaced)
            {
                fusePlaced = true;
                SpawnFuse(fuseType);
                CPInventory.instance.RemoveChessPiece(fuseType);
                CheckFuseBox(fuseType);
            }
            AKAudioManager.instance.Play(insertFuseSound);
        }

        private void SpawnFuse(ChessPiece fuseType)
        {
            currentFuse = fuseType;
            fuseBoxLightMaterial.color = Color.green;
            spawnedFuse = Instantiate(fuseType.ChessPrefab, fuseLocation.transform.position + spawnOffset, Quaternion.identity);
            spawnedFuse.transform.parent = fuseLocation.transform;
            spawnedFuse.transform.rotation = fuseRotation;
        }

        public void RemoveFuse(ChessPiece fuseType)
        {
            if (fusePlaced)
            {
                fusePlaced = false;
                fuseBoxLightMaterial.color = Color.red;
                CPInventory.instance.AddChessPiece(fuseType);
                Destroy(spawnedFuse);
                CheckFuseBox(null);
                AKAudioManager.instance.Play(insertFuseSound);
            }
        }

        private void OnDestroy()
        {
            Destroy(fuseBoxLightRend);
        }
    }
}
       