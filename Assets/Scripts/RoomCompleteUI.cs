//////////////////////////////////////////////////////////////////
///
/// ---------------------- RoomCompleteUI.cs --------------------
/// 
/// Made by: Bram Reuling
/// 
/// Description: Script for showing player info from last played
/// room. Shows things like what level the player is at the end
/// what health the player has and how many kills the player has.
/// 
/// RoomCompleteUI.cs contains the following classes:
/// - NONE
/// 
//////////////////////////////////////////////////////////////////
using TMPro;
using UnityEngine;

public class RoomCompleteUI : MonoBehaviour
{

    #region Editor Variable Declarations

    [SerializeField]
    private TextMeshProUGUI level;
    [SerializeField]
    private TextMeshProUGUI health;
    [SerializeField]
    private TextMeshProUGUI enemiesKilled;

    #endregion

    #region Private Methods

    // Start is called before the first frame update
    private void Start()
    {
        level.text = "Current player level: " + DataHandler.levelPlayer;
        health.text = "Current player health: " + DataHandler.healthPlayer;
        enemiesKilled.text = "Enemies killed: " + DataHandler.enemiesKilled;
    }

    #endregion

}
