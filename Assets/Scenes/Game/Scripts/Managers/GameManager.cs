using UnityEngine.SceneManagement;


// Attach this script to the Scene_Game - GameManager GameObject.

public class GameManager : Singleton<GameManager>
{
    // -------------------------------------------------------------------------
    // Public Variables:
    // ------------------
    //   AvailableLives
    //   IsGameStarted
    //   Level
    //   Lives
    //   Score
    //   Speed
    // -------------------------------------------------------------------------

    #region .  Public Variables  .

    public int  AvailableLives;
    public bool IsGameStarted;
    public int  Level;
    public int  Lives;
    public int  Score;
    public int  Speed;

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
    //   Description..:  Start is called before the first frame update.
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void Awake()
    {
        this.AvailableLives = 3;
        this.IsGameStarted  = false;
        this.Level          = 1;
        this.Lives          = this.AvailableLives;
        this.Score          = 0;
        this.Speed          = 1;

   }   // Awake()
    #endregion


    #region .  Start()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Start()
    //   Description..:  Start is called before the first frame update.
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void Start()
    {
        UIManager.Instance.UpdateLevelValue();
        UIManager.Instance.UpdateLivesValue();
        UIManager.Instance.UpdateScoreValue();
        UIManager.Instance.UpdateSpeedValue();

        // Start the game.
        this.IsGameStarted = true;

    }   // Start()
    #endregion


}   // class GameManager
