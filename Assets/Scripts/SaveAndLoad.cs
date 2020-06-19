using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public static class SaveAndLoad
{

    public static string fileName = "/DungeonGameData.dgd";

    public static void SavePlayer(Player player)
    {
        Debug.Log("SAVING");

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + fileName;
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData playerData = new PlayerData(player);

        binaryFormatter.Serialize(stream, playerData);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        Debug.Log("LOADING");

        string path = Application.persistentDataPath + fileName;
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = binaryFormatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("FILE NOT FOUND: " + path);
            return null;
        }

    }

    public static void SaveGame(Player player, Scene scene)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + fileName;
        FileStream stream = new FileStream(path, FileMode.Create);

        SceneData sceneData = new SceneData(player, scene);

        DataHandler.data = sceneData;

        formatter.Serialize(stream, sceneData);
        stream.Close();
    }

    public static SceneData LoadGame()
    {
        string path = Application.persistentDataPath + fileName;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SceneData data = formatter.Deserialize(stream) as SceneData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("File not found: " + path);
            return null;
        }
    }
}
