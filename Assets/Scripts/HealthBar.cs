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

    [SerializeField]
    private Player player;
    private Slider slider;

    [SerializeField]
    private TextMeshProUGUI text;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = player.Health;
        text.text = player.Health.ToString() + " / " + slider.maxValue + " HP";
    }
}
