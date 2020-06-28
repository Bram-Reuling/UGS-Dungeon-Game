//////////////////////////////////////////////////////////////////
///
/// ---------------------- Game.cs -----------------------------
/// 
/// Made by: Bram Reuling
/// 
/// Description: Script for saving and loading the game.
/// 
/// Game.cs contains the following classes:
/// - SaveGame()
/// - LoadGame()
/// 
//////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{

    #region Editor Variable Declarations

    [SerializeField]
    private Player player;
    [SerializeField]
    private SceneLoader sceneLoader;

    #endregion

    #region Non-editor Variable Declarations

    private Scene scene;
    static public SceneData dataOfScene;

    #endregion

    #region Private Methods

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    #endregion

    #region Public Methods

    public void SaveGame()
    {
        SaveAndLoad.SaveGame(player, scene);
    }

    public void LoadGame()
    {
        dataOfScene = SaveAndLoad.LoadGame();

        DataHandler.data = dataOfScene;
        sceneLoader.ChangeScene(dataOfScene);
    }

    #endregion

}
