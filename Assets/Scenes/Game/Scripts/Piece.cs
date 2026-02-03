using UnityEngine;


// Attach this script to the Scene_Game - Board GameObject.

public class Piece : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // Public Variables:
    // -----------------
    //   LockDelay
    //   MoveDelay
    //   StepDelay
    // -------------------------------------------------------------------------

    #region .  Public Variables  .

    public float LockDelay = 0.5f;
    public float MoveDelay = 0.1f;
    public float StepDelay = 1.0f;

    #endregion



    // -------------------------------------------------------------------------
    // Public Properties:
    // ------------------
    //   Cells
    //   Data
    //   GameBoard
    //   Position
    //   RotationIndex
    // -------------------------------------------------------------------------

    #region .  Public Properties  .

    public Vector3Int[]  Cells         { get; private set; }
    public TetrominoData Data          { get; private set; }
    public Board         GameBoard     { get; private set; }
    public Vector3Int    Position      { get; private set; }
    public int           RotationIndex { get; private set; }

    #endregion



    // -------------------------------------------------------------------------
    // Private Variables:
    // -------------------
    //   _lockTime
    //   _moveTime
    //   _stepTime
    // -------------------------------------------------------------------------

    #region . Private Variables  .

    private float _lockTime;
    private float _moveTime;
    private float _stepTime;

    #endregion



    // -------------------------------------------------------------------------
    // Public Methods:
    // ---------------
    //   Initialize()
    // -------------------------------------------------------------------------

    #region .  Initialize()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Initialize()
    //   Description..:  Initialize the piece with the given board, position,
    //                   and data.
    //   Parameters...:  Board : board
    //                   Vector3Int : position
    //                   TetrominoData : data
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void Initialize(Board board, Vector3Int position, TetrominoData data)
    {
        this.Data      = data;
        this.GameBoard = board;
        this.Position  = position;

        this.RotationIndex = 0;
        this._stepTime     = Time.time + this.StepDelay;
        this._moveTime     = Time.time + this.MoveDelay;
        this._lockTime     = 0f;

        this.Cells ??= new Vector3Int[data.Cells.Length];

        for (int i = 0; i < this.Cells.Length; i++)
        {
            this.Cells[i] = (Vector3Int)data.Cells[i];
        }

    }   // Initialize()
    #endregion



    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   ApplyRotationMatrix()
    //   GetWallKickIndex()
    //   HardDrop()
    //   Move()
    //   OnCollisionEnter2D()
    //   Step()
    //   Rotate()
    //   TestWallKicks()
    //   Update()
    //   Wrap()
    // -------------------------------------------------------------------------

    #region .  ApplyRotationMatrix()  .
    // -------------------------------------------------------------------------
    //   Method.......:  ApplyRotationMatrix()
    //   Description..:  Handle rotation of the current piece.
    //   Parameters...:  int : rotation direction
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void ApplyRotationMatrix(int direction)
    {
        float[] matrix = global::Data.RotationMatrix;

        // Rotate all of the cells using the rotation matrix.
        for (int i = 0; i < this.Cells.Length; i++)
        {
            Vector3 cell = this.Cells[i];

            int x, y;

            switch (this.Data.tetromino)
            {
                case Tetromino.I:
                case Tetromino.O:
                    // "I" and "O" are rotated from an offset center point.
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    x = Mathf.CeilToInt((cell.x * matrix[0] * direction) + (cell.y * matrix[1] * direction));
                    y = Mathf.CeilToInt((cell.x * matrix[2] * direction) + (cell.y * matrix[3] * direction));
                    break;

                default:
                    x = Mathf.RoundToInt((cell.x * matrix[0] * direction) + (cell.y * matrix[1] * direction));
                    y = Mathf.RoundToInt((cell.x * matrix[2] * direction) + (cell.y * matrix[3] * direction));
                    break;
            }

            this.Cells[i] = new Vector3Int(x, y, 0);
        }

    }   // ApplyRotationMatrix()
    #endregion


    #region .  GetWallKickIndex()  .
    // -------------------------------------------------------------------------
    //   Method.......:  GetWallKickIndex()
    //   Description..:  
    //   Parameters...:  int : rotation index
    //                   int : rotation direction
    //   Returns......:  int : wall kick index
    // -------------------------------------------------------------------------
    private int GetWallKickIndex(int rotationIndex, int rotationDirection)
    {
        int wallKickIndex = rotationIndex * 2;

        if (rotationDirection < 0)
        {
            wallKickIndex--;
        }

        return Wrap(wallKickIndex, 0, this.Data.Wallkicks.GetLength(0));

    }   // GetWallKickIndex()
    #endregion


    #region .  HandleMoveInputs()  .
    // -------------------------------------------------------------------------
    //   Method.......:  HandleMoveInputs()
    //   Description..:  Handle left, right and soft drop movement of the current
    //                   piece.
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void HandleMoveInputs()
    {
        // Soft drop movement.
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (Move(Vector2Int.down))
            {
                // Update the step time to prevent double movement.
                this._stepTime = Time.time + this.StepDelay;
            }
        }

        // Left/right movement.
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            Move(Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            Move(Vector2Int.right);
        }

    }   // HandleMoveInputs()
    #endregion


    #region .  HardDrop()  .
    // -------------------------------------------------------------------------
    //   Method.......:  HardDrop()
    //   Description..:  Handle hard drop of the current piece.
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void HardDrop()
    {
        while (this.Move(Vector2Int.down))
        {
            continue;
        }

        this.Lock();

    }   // HardDrop()
    #endregion


    #region .  Lock()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Lock()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void Lock()
    {
        this.GameBoard.Set(this);
        this.GameBoard.ClearLines();
        this.GameBoard.SpawnPiece();

    }   // Lock()
    #endregion


    #region .  Move()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Move()
    //   Description..:  Handle movement of the current piece.
    //   Parameters...:  Vector2Int : new position
    //   Returns......:  bool : true if the move was successful
    // -------------------------------------------------------------------------
    private bool Move(Vector2Int translation)
    {
        Vector3Int newPosition = Position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;

        bool valid = this.GameBoard.IsValidPosition(this, newPosition);

        // Only save the movement if the new position is valid.
        if (valid)
        {
            this.Position  = newPosition;
            this._moveTime = Time.time + this.MoveDelay;
            this._lockTime = 0f;
        }

        return valid;

    }   // Move()
    #endregion


    #region .  Rotate()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Rotate()
    //   Description..:  Handle rotation of the current piece.
    //   Parameters...:  int : rotation direction
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void Rotate(int direction)
    {
        // Store the current rotation in case the rotation fails and you need to
        // revert to the original rotation.
        int originalRotation = this.RotationIndex;

        // Rotate all of the cells using a rotation matrix.
        this.RotationIndex = this.Wrap(this.RotationIndex + direction, 0, 4);
        this.ApplyRotationMatrix(direction);

        // Revert the rotation if the wall kick tests fail.
        if (!this.TestWallKicks(this.RotationIndex, direction))
        {
            this.RotationIndex = originalRotation;
            this.ApplyRotationMatrix(-direction);
        }

    }   // Rotate()
    #endregion


    #region .  Step()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Step()
    //   Description..:  Step the current piece down one row.
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void Step()
    {
        this._stepTime = Time.time + this.StepDelay;

        // Step down to the next row.
        this.Move(Vector2Int.down);

        // Once the piece has been inactive for too long it becomes locked.
        if (this._lockTime >= this.LockDelay)
        {
            this.Lock();
        }

    }   // Step()
    #endregion


    #region .  TestWallKicks()  .
    // -------------------------------------------------------------------------
    //   Method.......:  TestWallKicks()
    //   Description..:  
    //   Parameters...:  int : rotation index
    //                   int : rotation direction
    //   Returns......:  bool : true if a wall kick was successful
    // -------------------------------------------------------------------------
    private bool TestWallKicks(int rotationIndex, int rotationDirection)
    {
        int wallKickIndex = this.GetWallKickIndex(rotationIndex, rotationDirection);

        for (int i = 0; i < this.Data.Wallkicks.GetLength(1); i++)
        {
            Vector2Int translation = Data.Wallkicks[wallKickIndex, i];

            if (this.Move(translation))
            {
                return true;
            }
        }

        return false;

    }   // TestWallKicks()
    #endregion


    #region .  Update()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Update()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void Update()
    {
        if (GameManager.Instance.IsGameStarted == false)
        {
            return;
        }

        this.GameBoard.Clear(this);

        // Use a timer to allow the player to make adjustments to the piece
        // before it locks in place.
        this._lockTime += Time.deltaTime;

        // Handle rotation.
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Q))
        {
            this.Rotate(-1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.E))
        {
            this.Rotate(1);
        }

        // Handle hard drop.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.HardDrop();
        }

        // Allow the player to hold movement keys but only after a move delay
        // so it does not move too fast.
        if (Time.time > this._moveTime)
        {
            this.HandleMoveInputs();
        }

        // Advance the piece to the next row every x seconds.
        if (Time.time > this._stepTime)
        {
            this.Step();
        }

        this.GameBoard.Set(this);

    }   // Update()
    #endregion


    #region .  Wrap()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Wrap()
    //   Description..:  
    //   Parameters...:  int : value to check
    //                   int : minimum value
    //                   int : maximum value
    //   Returns......:  int : wrapped value
    // -------------------------------------------------------------------------
    private int Wrap(int input, int min, int max)
    {
        return (input < min) ? max - (min - input) % (max - min)
                             : min + (input - min) % (max - min);

    }   // Wrap()
    #endregion


}   // class Piece
