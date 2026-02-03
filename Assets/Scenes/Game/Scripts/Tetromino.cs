using UnityEngine;
using UnityEngine.Tilemaps;


// -------------------------------------------------------------------------
// Public Enum:
// -----------
//   Tetromino
// -------------------------------------------------------------------------

#region .  Public Enum  .

public enum Tetromino
{
    I, J, L, O, S, T, Z
}

#endregion



[System.Serializable]
public struct TetrominoData
{
    // -------------------------------------------------------------------------
    // Public Variables:
    // -----------------
    //   tile
    //   tetromino
    // -------------------------------------------------------------------------

    #region .  Public Variables  .

    public Tile      tile;
    public Tetromino tetromino;

    #endregion



    // -------------------------------------------------------------------------
    // Public Properties:
    // ------------------
    //   Cells
    //   WallKicks
    // -------------------------------------------------------------------------

    #region .  Public Properties  .

    public Vector2Int[] Cells { get; private set; }
    public Vector2Int[,] Wallkicks { get; private set; }

    #endregion



    // -------------------------------------------------------------------------
    // Public Methods:
    // ---------------
    //   Initialize()
    // -------------------------------------------------------------------------

    #region .  Initialize()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Initialize()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void Initialize()
    {
        Cells     = Data.Cells[tetromino];
        Wallkicks = Data.WallKicks[tetromino];

    }   // Initialize()
    #endregion


}   // struct TetrominoData
