using TMPro;


// Attach this script to the Scene_Game - UIManager GameObject.

public class UIManager : Singleton<UIManager>
{
    // -------------------------------------------------------------------------
    // Public Variables:
    // -----------------
    //   LevelValueText
    //   LivesValueText
    //   ScoreValueText
    //   SpeedValueText
    // -------------------------------------------------------------------------

    #region .  Public Variables  .

    public TMP_Text LevelValueText;
    public TMP_Text LivesValueText;
    public TMP_Text ScoreValueText;
    public TMP_Text SpeedValueText;

    #endregion



    // -------------------------------------------------------------------------
    // Public Methods:
    // ---------------
    //   UpdateLivesValue()
    //   UpdateLevelValue()
    //   UpdateScoreValue()
    //   UpdateSpeedValue()
    // -------------------------------------------------------------------------

    #region .  UpdateLevelValue()  .
    // -------------------------------------------------------------------------
    //   Method.......:  UpdateLevelValue()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void UpdateLevelValue()
    {
        this.LevelValueText.text = GameManager.Instance.Level.ToString();

    }   // UpdateLevelValue()
    #endregion


    #region .  UpdateLivesValue()  .
    // -------------------------------------------------------------------------
    //   Method.......:  UpdateLivesValue()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void UpdateLivesValue()
    {
        this.LivesValueText.text = GameManager.Instance.Lives.ToString();

    }   // UpdateLivesValue()
    #endregion


    #region .  UpdateScoreValue()  .
    // -------------------------------------------------------------------------
    //   Method.......:  UpdateScoreValue()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void UpdateScoreValue()
    {
        this.ScoreValueText.text = GameManager.Instance.Score.ToString();

    }   // UpdateScoreValue()
    #endregion


    #region .  UpdateSpeedValue()  .
    // -------------------------------------------------------------------------
    //   Method.......:  UpdateSpeedValue()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void UpdateSpeedValue()
    {
        this.SpeedValueText.text = GameManager.Instance.Speed.ToString();

    }   // UpdateSpeedValue()
    #endregion


}   // class UIManager
