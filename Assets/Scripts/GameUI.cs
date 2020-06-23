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
    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject gameUI;

    private bool gameOnPause = false;
    private bool buttonPressed = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOnPause && !buttonPressed)
        {
            Cursor.lockState = CursorLockMode.Confined;
            pauseMenu.SetActive(true);
            gameUI.SetActive(false);
            Time.timeScale = 0;
            gameOnPause = true;
            buttonPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && gameOnPause && !buttonPressed)
        {
            Cursor.lockState = CursorLockMode.Locked;
            pauseMenu.SetActive(false);
            gameUI.SetActive(true);
            Time.timeScale = 1;
            gameOnPause = false;
            buttonPressed = true;
        }

        if (!Input.GetKeyDown(KeyCode.Escape))
        {
            buttonPressed = false;
        }
    }
}
