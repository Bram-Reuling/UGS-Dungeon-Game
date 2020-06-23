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
    [SerializeField]
    private Enemy enemy;
    private Slider slider;

    [SerializeField]
    private TextMeshProUGUI text;

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
    void Update()
    {
        slider.value = enemy.Health;
        text.text = enemy.Health.ToString() + " / " + slider.maxValue + " HP";
    }
}
