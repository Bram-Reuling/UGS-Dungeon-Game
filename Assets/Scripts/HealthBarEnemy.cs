//////////////////////////////////////////////////////////////////
///
/// ---------------------- HealthBarEnemy.cs -------------------------
/// 
/// Made by: Bram Reuling
/// 
/// Description: Script for showing and updating the health bar
/// for the enemy according to the current health.
/// 
/// HealthBarEnemy.cs contains the following classes:
/// - NONE
/// 
//////////////////////////////////////////////////////////////////
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBarEnemy : MonoBehaviour
{

    #region Editor Variable Declarations

    [SerializeField]
    private Enemy enemy;
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
        if (enemy != null)
        {
            slider.maxValue = enemy.Health;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        slider.value = enemy.Health;
        text.text = enemy.Health.ToString() + " / " + slider.maxValue + " HP";
    }

    #endregion

}
