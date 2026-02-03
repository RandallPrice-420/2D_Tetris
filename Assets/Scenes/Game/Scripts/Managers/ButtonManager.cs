using UnityEngine;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;


// Attach this script to the Scene_Game - ButtonManager GameObject.

public class ButtonManager : Singleton<ButtonManager>
{
    // -------------------------------------------------------------------------
    // Public Variables:
    // -----------------
    //   ButtonExit
    //   ButtonPause
    //   ButtonPlay
    //   ButtonYes
    //   ButtonNo
    //   PanelPaused
    //   PanelYouSure
    // -------------------------------------------------------------------------

    #region .  Public Variables  .

    public GameObject ButtonExit;
    public GameObject ButtonPause;
    public GameObject ButtonPlay;
    public GameObject ButtonYes;
    public GameObject ButtonNo;
    public GameObject PanelPaused;
    public GameObject PanelAreYouSure;

    #endregion



    // -------------------------------------------------------------------------
    // Public Methods:
    // ---------------
    //   ButtonExitClicked()
    //   ButtonNoClicked()
    //   ButtonPauseClicked()
    //   ButtonPlayClicked()
    //   ButtonYesClicked()
    // -------------------------------------------------------------------------

    #region .  ButtonExitClicked()  .
    // -------------------------------------------------------------------------
    //   Method.......:  ButtonExitClicked()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void ButtonExitClicked()
    {
        //Debug.Log("GameManager.ButtonExitClicked()");

        this.HideButtons();

        //this.ButtonPauseDisabled.SetActive(true);
        //this.ButtonExitDisabled .SetActive(true);
        this.PanelAreYouSure    .SetActive(true);

        GameManager.Instance.IsGameStarted = false;

    }   // ButtonExitClicked()
    #endregion


    #region .  ButtonNoClicked()  .
    // -------------------------------------------------------------------------
    //   Method.......:  ButtonNoClicked()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void ButtonNoClicked()
    {
        //Debug.Log("GameManager.ButtonNoClicked()");

        // Hide buttons and dialog panel.
        this.HideButtons();
        this.HidePanels();

        // Activate these buttons.
        this.ButtonPause.SetActive(true);
        this.ButtonExit .SetActive(true);

        // Resume the game.
        GameManager.Instance.IsGameStarted = true;

    }   // ButtonNoClicked()
    #endregion


    #region .  ButtonPauseClicked()  .
    // -------------------------------------------------------------------------
    //   Method.......:  ButtonPauseClicked()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void ButtonPauseClicked()
    {
        //Debug.Log("GameManager.ButtonPauseClicked()");

        this.HideButtons();
        this.HidePanels();

        // Show the Paused panel.
        this.PanelPaused.SetActive(true);

        this.ButtonPlay        .SetActive(true);
        //this.ButtonExitDisabled.SetActive(true);

        // Pause the game.
        GameManager.Instance.IsGameStarted = false;

    }   // ButtonPauseClicked()
    #endregion


    #region .  ButtonPlayClicked()  .
    // -------------------------------------------------------------------------
    //   Method.......:  ButtonPlayClicked()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void ButtonPlayClicked()
    {
        //Debug.Log("GameManager.ButtonPlayClicked()");

        this.HideButtons();
        this.HidePanels();

        this.ButtonPause.SetActive(true);
        this.ButtonExit .SetActive(true);

        GameManager.Instance.IsGameStarted = true;

    }   // ButtonPlayClicked()
    #endregion


    #region .  ButtonYesClicked()  .
    // -------------------------------------------------------------------------
    //   Method.......:  ButtonYesClicked()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void ButtonYesClicked()
    {
        //Debug.Log("GameManager.ButtonYesClicked()");

        // Stop the game.
        GameManager.Instance.IsGameStarted = false;

        SceneManager.LoadScene("Scene_MainMenu", LoadSceneMode.Single);

    }   // ButtonYesClicked()
    #endregion



    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   HideButtons()
    //   HidePanels()
    //   Start()
    // -------------------------------------------------------------------------

    #region .  HideButtons()  .
    // -------------------------------------------------------------------------
    //   Method.......:  HideButtons()
    //   Description..:  e
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void HideButtons()
    {
        this.ButtonExit         .SetActive(false);
        //this.ButtonExitDisabled .SetActive(false);
        this.ButtonPause        .SetActive(false);
        //this.ButtonPauseDisabled.SetActive(false);
        this.ButtonPlay         .SetActive(false);
        //this.ButtonPlayDisabled .SetActive(false);

    }   // HideButtons
    #endregion


    #region .  HidePanels()  .
    // -------------------------------------------------------------------------
    //   Method.......:  HidePanels()
    //   Description..:  e
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void HidePanels()
    {
        this.PanelPaused    .SetActive(false);
        this.PanelAreYouSure.SetActive(false);

    }   // HidePanels
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
        this.HideButtons();
        this.HidePanels();

        this.ButtonExit .SetActive(true);
        this.ButtonPause.SetActive(true);

    }   // Start()
    #endregion


}   // ButtonManager
