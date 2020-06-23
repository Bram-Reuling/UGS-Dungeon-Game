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

    private Scene scene;
    public Player player;

    public SceneLoader sceneLoader;

    static public SceneData data;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void SaveGame()
    {
        SaveAndLoad.SaveGame(player, scene);
    }

    public void LoadGame()
    {
        data = SaveAndLoad.LoadGame();

        DataHandler.data = data;
        sceneLoader.ChangeScene(data);
    }
}
