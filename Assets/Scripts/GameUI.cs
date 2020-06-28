//////////////////////////////////////////////////////////////////
///
/// ---------------------- GameUI.cs -----------------------------
/// 
/// Made by: Bram Reuling
/// 
/// Description: Script for opening and closing the in-game menu.
/// 
/// GameUI.cs contains the following classes:
/// - NONE
/// 
//////////////////////////////////////////////////////////////////
using UnityEngine;

public class GameUI : MonoBehaviour
{

    #region Editor Variable Declarations

    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject gameUI;

    #endregion

    #region Non-editor Variable Declarations

    private bool isGamePaused = false;
    private bool isButtonPressed = false;

    #endregion

    #region Private Methods

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGamePaused && !isButtonPressed)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            pauseMenu.SetActive(true);
            gameUI.SetActive(false);
            Time.timeScale = 0;
            isGamePaused = true;
            isButtonPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isGamePaused && !isButtonPressed)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pauseMenu.SetActive(false);
            gameUI.SetActive(true);
            Time.timeScale = 1;
            isGamePaused = false;
            isButtonPressed = true;
        }

        if (!Input.GetKeyDown(KeyCode.Escape))
        {
            isButtonPressed = false;
        }
    }

    #endregion

}
