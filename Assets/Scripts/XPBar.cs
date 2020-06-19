using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
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
        slider.maxValue = player.GetComponent<Player>().xpForLevelUp;
        slider.value = player.GetComponent<Player>().currentXP;
        text.text = "Level: "+player.GetComponent<Player>().Level;
    }
}
