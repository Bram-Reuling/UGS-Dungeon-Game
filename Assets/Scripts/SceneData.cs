//////////////////////////////////////////////////////////////////
///
/// ---------------------- SceneData.cs ------------------------
/// 
/// Made by: Bram Reuling
/// 
/// Description: Script for storing the current scene data
/// 
/// SceneData.cs contains the following classes:
/// - NONE
/// 
//////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SceneData
{
    // Player Data
    public int health;
    public int xp;
    public int level;
    public int xpForLevelUp;
    public float[] position;

    // Other Data
    public string sceneName;

    public SceneData(Player player, Scene scene)
    {
        // Player
        health = player.Health;
        xp = player.XP;
        level = player.Level;
        xpForLevelUp = player.XPForLevelUp;

        position = new float[3];
        position[0] = player.Position.x;
        position[1] = player.Position.y;
        position[2] = player.Position.z;

        // Other
        sceneName = scene.name;
    }
}
