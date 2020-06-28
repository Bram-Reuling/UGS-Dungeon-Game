//////////////////////////////////////////////////////////////////
///
/// ---------------------- SaveAndLoad.cs ------------------------
/// 
/// Made by: Bram Reuling
/// 
/// Description: Script for saving and loading the game.
/// 
/// SaveAndLoad.cs contains the following classes:
/// - SaveGame()
/// - LoadGame()
/// 
//////////////////////////////////////////////////////////////////
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public static class SaveAndLoad
{

    #region Variable Declarations

    private static string fileName = "/DungeonGameData.dgd";

    #endregion

    #region Public Methods

    public static void SaveGame(Player player, Scene scene)
    {
        // Creates a new formatter to transfer all data to binary
        BinaryFormatter formatter = new BinaryFormatter();

        // Creates a path to a directory on the players computer
        // Doesnt matter if Windows or Mac.
        string path = Application.persistentDataPath + fileName;

        // Opens new filestream to write to
        FileStream stream = new FileStream(path, FileMode.Create);

        // Puts all the current player and scene data in a variable
        // of type SceneData
        SceneData sceneData = new SceneData(player, scene);

        // Stores a copy in the DataHandler class.
        DataHandler.data = sceneData;

        // Convert all data to binary and writes it to the stream.
        formatter.Serialize(stream, sceneData);

        // Close the stream.
        stream.Close();
    }

    public static SceneData LoadGame()
    {

        // Creates a path to a directory on the players computer
        // Doesnt matter if Windows or Mac.
        string path = Application.persistentDataPath + fileName;

        // Checks if the path exists
        if (File.Exists(path))
        {
            // Creates a new formatter to transfer all data back to normal
            BinaryFormatter formatter = new BinaryFormatter();

            // Opens new filestream to read data from file.
            FileStream stream = new FileStream(path, FileMode.Open);

            // Converts all data from binary to normal and stores it in the variable data
            SceneData data = formatter.Deserialize(stream) as SceneData;

            // Closes the stream
            stream.Close();

            // Returns the data.
            return data;
        }
        else
        {
            Debug.LogError("File not found: " + path);
            return null;
        }
    }

    #endregion

}
