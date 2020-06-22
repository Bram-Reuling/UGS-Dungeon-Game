[System.Serializable]
public class PlayerData
{
    public int health;
    public int xp;
    public int level;
    public int xpForLevelUp;
    public float[] position;

    public PlayerData (Player player)
    {
        health = player.Health;
        xp = player.XP;
        level = player.Level;
        xpForLevelUp = player.XPForLevelUp;

        position = new float[3];
        position[0] = player.Position.x;
        position[1] = player.Position.y;
        position[2] = player.Position.z;
    }
}
