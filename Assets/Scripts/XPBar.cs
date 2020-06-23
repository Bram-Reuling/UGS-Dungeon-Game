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
    [SerializeField]
    private Player player;
    private Slider slider;

    [SerializeField]
    private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.maxValue = player.XPForLevelUp;
        slider.value = player.XP;
        text.text = "Level: "+player.Level;
    }
}
