using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject gameUI;

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
            Debug.Log("PAUSE");
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
