using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveAndLoad
{

    public static string fileName = "/DungeonGameData.dgd";

    public static void Save(Player player)
    {
        Debug.Log("SAVING");

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + fileName;
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData playerData = new PlayerData(player);

        binaryFormatter.Serialize(stream, playerData);
        stream.Close();
    }

    public static PlayerData Load()
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
}
