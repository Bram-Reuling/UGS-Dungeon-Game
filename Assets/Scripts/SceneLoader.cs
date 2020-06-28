//////////////////////////////////////////////////////////////////
///
/// ---------------------- SceneLoader.cs ------------------------
/// 
/// Made by: Bram Reuling
/// 
/// Description: Script for loading scenes and quiting the game.
/// 
/// SaveAndLoad.cs contains the following classes:
/// - NewGame()
/// - ChangeScene()
/// - QuitGame()
/// 
//////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    #region Public Methods

    public void NewGame(int sceneIndex)
    {
        DataHandler.data = null;
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1;
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

    #endregion

}
