using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// Attach this script to the MainMenuManager GameObject.

public class MainMenuManager : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // Public Variables:
    // -----------------
    //   LogoIndex
    //   LogoSprites
    // -------------------------------------------------------------------------

    #region .  Pubic Variables  .

    public int      LogoIndex = 0;
    public Sprite[] LogoSprites;

    #endregion



    // -------------------------------------------------------------------------
    // Private Variables:
    // -------------------
    //   _logoImage
    // -------------------------------------------------------------------------

    #region . Private Variables  .

    private Image _logoImage;

    #endregion



    // -------------------------------------------------------------------------
    // Public Methods:
    // ---------------
    //   NextLogo()
    //   PlayGame()
    //   QuitGame()
    // -------------------------------------------------------------------------

    #region .  NextLogo()  .
    // -------------------------------------------------------------------------
    //   Method.......:  NextLogo()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void NextLogo()
    {
        this._logoImage = FindObjectOfType<Image>();

        // Get the next logo sprite (wrap around to the beginning if necessary).
        this.LogoIndex         = (this.LogoIndex == this.LogoSprites.Length) ? 1 : this.LogoIndex + 1;
        this._logoImage.sprite = this.LogoSprites[this.LogoIndex - 1];

    }   // NextLogo()
    #endregion


    #region .  PlayGame()  .
    // -------------------------------------------------------------------------
    //   Method.......:  PlayGame()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void PlayGame()
    {
        SceneManager.LoadScene("Scene_Game", LoadSceneMode.Single);

    }   // PlayGame()
    #endregion


    #region .  QuitGame()  .
    // -------------------------------------------------------------------------
    //   Method.......:  QuitGame()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();

    }   // QuitGame()
    #endregion


}   // class MainMenuManager
