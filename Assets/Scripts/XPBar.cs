//////////////////////////////////////////////////////////////////
///
/// ---------------------- XPBar.cs ------------------------
/// 
/// Made by: Bram Reuling
/// 
/// Description: Script for loading scenes and quiting the game.
/// 
/// XPBar.cs contains the following classes:
/// - NewGame()
/// - ChangeScene()
/// - QuitGame()
/// 
//////////////////////////////////////////////////////////////////
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class XPBar : MonoBehaviour
{

    #region Editor Variable Declarations

    [SerializeField]
    private Player player;
    [SerializeField]
    private TextMeshProUGUI text;

    #endregion

    #region Non-editor Variable Declarations

    private Slider slider;

    #endregion

    #region Private Methods

    // Start is called before the first frame update
    private void Start()
    {
        slider = GetComponent<Slider>();

        if (player == null)
        {
            Debug.LogError("There is no instance of player connected to XPBar");
        }

    }

    // Update is called once per frame
    private void Update()
    {
        if (player != null)
        {
            slider.maxValue = player.XPForLevelUp;
            slider.value = player.XP;
            text.text = "Level: " + player.Level;
        }
    }

    #endregion

}
