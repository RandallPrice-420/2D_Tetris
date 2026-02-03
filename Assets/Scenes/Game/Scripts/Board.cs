using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;


// Attach this script to the Scene_Game - Board GameObject.

[DefaultExecutionOrder(-1)]
public class Board : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // Public Variables:
    // -----------------
    //   BoardSize
    //   SpawnPosition
    //   Tetrominoes
    // -------------------------------------------------------------------------

    #region .  Public Variables  .

    public Vector2Int      BoardSize     = new(10, 20);
    public Vector3Int      SpawnPosition = new(-1, 8, 0);
    public TetrominoData[] Tetrominoes;

    #endregion



    // -------------------------------------------------------------------------
    // Public Properties:
    // ------------------
    //   ActivePiece
    //   TileMap
    //   Bounds
    // -------------------------------------------------------------------------

    #region .  Public Properties  .

    public Piece   ActivePiece { get; private set; }

    public Tilemap TileMap     { get; private set; }

    public RectInt Bounds
    {
        get
        {
            Vector2Int position = new(-BoardSize.x / 2, -BoardSize.y / 2);
            return new RectInt(position, BoardSize);
        }

    }   // Bounds

    #endregion



    // -------------------------------------------------------------------------
    // Public Methods:
    // ---------------
    //   Clear()
    //   ClearLines()
    //   GameOver()
    //   IsLineFull()
    //   IsValidPosition()
    //   LineClear()
    //   Set()
    //   SpawnPiece()
    //   StartGame()
    // -------------------------------------------------------------------------

    #region .  Clear()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Clear()
    //   Description..:  
    //   Parameters...:  Piece : The piece to clear from the board.
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void Clear(Piece piece)
    {
        for (int i = 0; i < piece.Cells.Length; i++)
        {
            Vector3Int tilePosition = piece.Cells[i] + piece.Position;
            this.TileMap.SetTile(tilePosition, null);
        }

    }   // Clear()
    #endregion


    #region .  ClearLines()  .
    // -------------------------------------------------------------------------
    //   Method.......:  ClearLines()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void ClearLines()
    {
        RectInt bounds = this.Bounds;
        int row = bounds.yMin;

        // Clear from bottom to top.
        while (row < bounds.yMax)
        {
            // Only advance to the next row if the current row is not cleared
            // because the tiles above will fall down when a row is cleared.
            if (this.IsLineFull(row))
            {
                this.LineClear(row);
            }
            else
            {
                row++;
            }
        }

    }   // ClearLines()
    #endregion


    #region .  GameOver()  .
    // -------------------------------------------------------------------------
    //   Method.......:  GameOver()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void GameOver()
    {
        this.TileMap.ClearAllTiles();

        // Do anything else you want on game over here.

    }   // GameOver()
    #endregion


    #region .  IsLineFull()  .
    // -------------------------------------------------------------------------
    //   Method.......:  IsLineFull()
    //   Description..:  
    //   Parameters...:  int row : The row to check.
    //   Returns......:  bool    : True if the line is full, false otherwise.
    // -------------------------------------------------------------------------
    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = this.Bounds;

        // The position is only valid if every cell is valid.
        for (int i = 0; i < piece.Cells.Length; i++)
        {
            Vector3Int tilePosition = piece.Cells[i] + position;

            // An out of bounds tile is invalid
            if (!bounds.Contains((Vector2Int)tilePosition))
            {
                return false;
            }

            // A tile already occupies the position, thus invalid.
            if (this.TileMap.HasTile(tilePosition))
            {
                return false;
            }
        }

        return true;

    }   // IsLineFull()
    #endregion


    #region .  IsValidPosition()  .
    // -------------------------------------------------------------------------
    //   Method.......:  IsValidPosition()
    //   Description..:  
    //   Parameters...:  Piece piece : The piece to check.
    //                   Vector3Int position : The position to check.
    //   Returns......:  bool : True if the position is valid, false otherwise.
    // -------------------------------------------------------------------------
    public bool IsLineFull(int row)
    {
        RectInt bounds = this.Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new(col, row, 0);

            // The line is not full if a tile is missing.
            if (!this.TileMap.HasTile(position))
            {
                return false;
            }
        }

        return true;

    }   // IsValidPosition()
    #endregion


    #region .  LineClear()  .
    // -------------------------------------------------------------------------
    //   Method.......:  LineClear()
    //   Description..:  
    //   Parameters...:  int row : The row to clear.
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void LineClear(int row)
    {
        RectInt bounds = this.Bounds;

        // Clear all tiles in the row.
        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new(col, row, 0);
            this.TileMap.SetTile(position, null);
        }

        // Shift every row above down one.
        while (row < bounds.yMax)
        {
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new(col, row + 1, 0);
                TileBase above = this.TileMap.GetTile(position);

                position = new Vector3Int(col, row, 0);
                this.TileMap.SetTile(position, above);
            }

            row++;
        }

    }   // LineClear()
    #endregion


    #region .  Set()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Set()
    //   Description..:  
    //   Parameters...:  Piece piece : The piece to set on the board.
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.Cells.Length; i++)
        {
            Vector3Int tilePosition = piece.Cells[i] + piece.Position;
            this.TileMap.SetTile(tilePosition, piece.Data.tile);
        }

    }   // Set()
    #endregion


    #region .  SpawnPiece()  .
    // -------------------------------------------------------------------------
    //   Method.......:  SpawnPiece()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void SpawnPiece()
    {
        int random = Random.Range(0, this.Tetrominoes.Length);
        TetrominoData data = this.Tetrominoes[random];

        this.ActivePiece.Initialize(this, this.SpawnPosition, data);

        if (this.IsValidPosition(this.ActivePiece, this.SpawnPosition))
        {
            this.Set(ActivePiece);
        }
        else
        {
            this.GameOver();
        }

    }   // SpawnPiece()
    #endregion



    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   Awake()
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
        this.TileMap     = GetComponentInChildren<Tilemap>();
        this.ActivePiece = GetComponentInChildren<Piece>();

        for (int i = 0; i < this.Tetrominoes.Length; i++) {
            this.Tetrominoes[i].Initialize();
        }

    }   // Awake()
    #endregion


    #region .  Start()  .
    // -------------------------------------------------------------------------
    //   Method.......:  StaStartrtGame()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void Start()
    {
        this.SpawnPiece();

    }   // Start()
    #endregion


}   // class Board
