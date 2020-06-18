using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{

    public GameObject enemy;
    public Slider slider;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = enemy.GetComponent<Enemy>().health;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = enemy.GetComponent<Enemy>().health;
        text.text = enemy.GetComponent<Enemy>().health.ToString() + " / " + slider.maxValue + " HP";
    }
}
