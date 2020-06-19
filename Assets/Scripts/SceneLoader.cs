using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void NewGame(int sceneIndex)
    {
        DataHandler.data = null;
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1;
        // After loading the scene, save it.
    }

    public void NewGame(string sceneName)
    {
        DataHandler.data = null;
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }

    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1;
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }

    public void ChangeScene(SceneData data)
    {
        SceneManager.LoadScene(data.sceneName);
        Time.timeScale = 1;

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
