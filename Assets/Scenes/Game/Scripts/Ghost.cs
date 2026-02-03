using UnityEngine;
using UnityEngine.Tilemaps;


// Attach this script to the Scene_Game - Board GameObject.

public class Ghost : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // Public Variables:
    // -----------------
    //   GameBoard
    //   tile
    //   TrackingPiece
    // -------------------------------------------------------------------------

    #region .  Public Variables  .

    public Board GameBoard;
    public Tile  tile;
    public Piece TrackingPiece;

    #endregion



    // -------------------------------------------------------------------------
    // Public Properties:
    // ------------------
    //   Cells
    //   Position
    //   TileMap
    // -------------------------------------------------------------------------

    #region .  Public Properties  .

    public Vector3Int[] Cells    { get; private set; }
    public Vector3Int   Position { get; private set; }
    public Tilemap      TileMap  { get; private set; }

    #endregion



    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   Awake()
    //   Clear()
    //   Copy()
    //   Drop()
    //   LateUpdate()
    //   Set()
    //   Start()
    // -------------------------------------------------------------------------

    #region .  Awake()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Awake()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void Awake()
    {
        this.TileMap = GetComponentInChildren<Tilemap>();
        this.Cells   = new Vector3Int[4];

    }   // Awake()
    #endregion


    #region .  Clear()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Clear()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void Clear()
    {
        for (int i = 0; i < this.Cells.Length; i++)
        {
            Vector3Int tilePosition = this.Cells[i] + this.Position;
            this.TileMap.SetTile(tilePosition, null);
        }

    }   // Clear

    #endregion


    #region .  Copy()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Copy()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void Copy()
    {
        for (int i = 0; i < this.Cells.Length; i++) {
            this.Cells[i] = this.TrackingPiece.Cells[i];
        }

    }   // Copy()
    #endregion


    #region .  Drop()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Drop()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void Drop()
    {
        Vector3Int position = this.TrackingPiece.Position;

        int current = position.y;
        int bottom = -this.GameBoard.BoardSize.y / 2 - 1;

        this.GameBoard.Clear(this.TrackingPiece);

        for (int row = current; row >= bottom; row--)
        {
            position.y = row;

            if (this.GameBoard.IsValidPosition(this.TrackingPiece, position))
            {
                this.Position = position;
            }
            else
            {
                break;
            }
        }

        this.GameBoard.Set(this.TrackingPiece);

    }   // Drop()
    #endregion


    #region .  LateUpdate()  .
    // -------------------------------------------------------------------------
    //   Method.......:  LateUpdate()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void LateUpdate()
    {
        this.Clear();
        this.Copy();
        this.Drop();
        this.Set();

    }   // LateUpdate()
    #endregion


    #region .  Set()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Set()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void Set()
    {
        for (int i = 0; i < this.Cells.Length; i++)
        {
            Vector3Int tilePosition = this.Cells[i] + this.Position;
            this.TileMap.SetTile(tilePosition, this.tile);
        }

    }   // Set()
    #endregion


}   // class Ghost
