using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{

    private Scene scene;
    private GameObject playerInstance;
    private Player player;

    public SceneLoader sceneLoader;

    static public SceneData data;

    public TextMeshProUGUI savedText;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        if (GameObject.Find("Player") != null)
        {
            savedText.enabled = false;
            playerInstance = GameObject.Find("Player");
            player = playerInstance.GetComponent<Player>();
            //StartCoroutine(AutoSave());
        }
    }

    IEnumerator AutoSave()
    {
        while (true)
        {
            SaveGame();
            savedText.enabled = true;
            yield return new WaitForSeconds(5);
            savedText.enabled = false;
            yield return new WaitForSeconds(10);
        }
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

//player.playerHealth = data.health;
//        player.xp = data.xp;
//        player.level = data.level;
//        player.xpForLevelUp = data.xpForLevelUp;

//        player.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
