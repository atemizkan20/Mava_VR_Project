using System.Collections.Generic;
using UnityEngine;

namespace AdventurePuzzleKit.ChessSystem
{
    public class CPInventory : MonoBehaviour
    {
        public List<ChessPiece> chessPieceList = new List<ChessPiece>();

        public static CPInventory instance;

        void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }
        }

        public void AddChessPiece(ChessPiece chessPiece)
        {
            if (!chessPieceList.Contains(chessPiece))
            {
                chessPieceList.Add(chessPiece);
                AKUIManager.instance.FillChessInventorySlot();
                AKUIManager.instance.ChessPieceCollected();
            }
        }

        public void RemoveChessPiece(ChessPiece chessPiece)
        {
            if (chessPieceList.Contains(chessPiece))
            {
                int currentCount = chessPieceList.Count;
                chessPieceList.Remove(chessPiece);
                AKUIManager.instance.ResetChessInventorySlot();
            }
        }
    }
}

/*[HideInInspector] public bool hasPawnFuse;
  [HideInInspector] public bool hasRookFuse;
  [HideInInspector] public bool hasKnightFuse;
  [HideInInspector] public bool hasBishopFuse;
  [HideInInspector] public bool hasQueenFuse;
  [HideInInspector] public bool hasKingFuse;

  [HideInInspector] public bool usingPawnBox;
  [HideInInspector] public bool usingRookBox;
  [HideInInspector] public bool usingKnightBox;
  [HideInInspector] public bool usingBishopBox;
  [HideInInspector] public bool usingQueenBox;
  [HideInInspector] public bool usingKingBox;

  [Header("Main Key UI")]
  [SerializeField] private GameObject chessPuzzleInventoryUI;

  [Header("Key Icon UI")]
  [SerializeField] private Image pawnImageSlotUI = null;
  [SerializeField] private Image rookImageSlotUI = null;
  [SerializeField] private Image knightImageSlotUI = null;
  [SerializeField] private Image bishopImageSlotUI = null;
  [SerializeField] private Image queenImageSlotUI= null;
  [SerializeField] private Image kingImageSlotUI = null;

  [Header("Type of Key")]
  private InventoryPiece _inventoryPiece;
  public enum InventoryPiece { Pawn, Rook, Knight, Bishop, Queen, King }

  public static ChessInventoryManager instance;

  [HideInInspector] public ChessFuseBoxController invfuseBoxController;

  void Awake()
  {
      if (instance != null) { Destroy(gameObject); }
      else { instance = this; DontDestroyOnLoad(gameObject); }
  }

  public void OpenInventory()
  {
      AKUIManager.instance.OpenInventory();
  }

  public void PressButton(string buttonType)
  {
      EventSystem.current.SetSelectedGameObject(null);

      switch (buttonType)
      {
          case "Pawn":
              if (hasPawnFuse && !invfuseBoxController.fusePlaced)
              {
                  invfuseBoxController.PlaceFuse("Pawn");
                  hasPawnFuse = false;
                  pawnImageSlotUI.color = Color.black;
              }
              break;
          case "Rook":
              if (hasRookFuse && !invfuseBoxController.fusePlaced)
              {
                  invfuseBoxController.PlaceFuse("Rook");
                  hasRookFuse = false;
                  rookImageSlotUI.color = Color.black;
              }
              break;
          case "Knight":
              if (hasKnightFuse && !invfuseBoxController.fusePlaced)
              {
                  invfuseBoxController.PlaceFuse("Knight");
                  hasKnightFuse = false;
                  knightImageSlotUI.color = Color.black;
              }
              break;
          case "Bishop":
              if (hasBishopFuse && !invfuseBoxController.fusePlaced)
              {
                  invfuseBoxController.PlaceFuse("Bishop");
                  hasBishopFuse = false;
                  bishopImageSlotUI.color = Color.black;
              }
              break;
          case "Queen":
              if (hasQueenFuse && !invfuseBoxController.fusePlaced)
              {
                  invfuseBoxController.PlaceFuse("Queen");
                  hasQueenFuse = false;
                  queenImageSlotUI.color = Color.black;
              }
              break;
          case "King":
              if (hasKingFuse && !invfuseBoxController.fusePlaced)
              {
                  invfuseBoxController.PlaceFuse("King");
                  hasKingFuse = false;
                  kingImageSlotUI.color = Color.black;
              }
              break;
          case "RemoveFuse":
              if (invfuseBoxController.fusePlaced)
              {
                  string pieceName = invfuseBoxController.fuseName;

                  switch(pieceName)
                  {
                      case "Pawn": UpdateInventory(InventoryPiece.Pawn); break;
                      case "Rook": UpdateInventory(InventoryPiece.Rook); break;
                      case "Knight": UpdateInventory(InventoryPiece.Knight); break;
                      case "Bishop": UpdateInventory(InventoryPiece.Bishop); break;
                      case "Queen": UpdateInventory(InventoryPiece.Queen); break;
                      case "King": UpdateInventory(InventoryPiece.King); break;
                  }

                  invfuseBoxController.PlaceFuse("RemoveFuse");
              }
              break;
      }
  }

  public void UpdateInventory(InventoryPiece _inventoryPiece)
  {
      switch (_inventoryPiece)
      {
          case InventoryPiece.Pawn:
              hasPawnFuse = true;
              pawnImageSlotUI.color = Color.white;
              AKUIManager.instance.hasChessPiece = true;
              break;
          case InventoryPiece.Rook:
              hasRookFuse = true;
              rookImageSlotUI.color = Color.white;
              AKUIManager.instance.hasChessPiece = true;
              break;
          case InventoryPiece.Knight:
              hasKnightFuse = true;
              knightImageSlotUI.color = Color.white;
              AKUIManager.instance.hasChessPiece = true;
              break;
          case InventoryPiece.Bishop:
              hasBishopFuse = true;
              bishopImageSlotUI.color = Color.white;
              AKUIManager.instance.hasChessPiece = true;
              break;
          case InventoryPiece.Queen:
              hasQueenFuse = true;
              queenImageSlotUI.color = Color.white;
              AKUIManager.instance.hasChessPiece = true;
              break;
          case InventoryPiece.King:
              hasKingFuse = true;
              kingImageSlotUI.color = Color.white;
              AKUIManager.instance.hasChessPiece = true;
              break;
      }
  }
}*/
