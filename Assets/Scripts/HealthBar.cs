using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public GameObject player;
    public Slider slider;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = player.GetComponent<Player>().Health;
        text.text = player.GetComponent<Player>().Health.ToString() + " / " + slider.maxValue + " HP";
    }
}
