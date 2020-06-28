//////////////////////////////////////////////////////////////////
///
/// ---------------------- HealthBar.cs -------------------------
/// 
/// Made by: Bram Reuling
/// 
/// Description: Script for showing and updating the health bar
/// for the player according to the current health.
/// 
/// HealthBar.cs contains the following classes:
/// - NONE
/// 
//////////////////////////////////////////////////////////////////
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
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

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        if (player == null)
        {
            this.enabled = false;
        }
    }

    private void Update()
    {
        slider.value = player.Health;
        text.text = player.Health.ToString() + " / " + slider.maxValue + " HP";
    }

    #endregion

}
