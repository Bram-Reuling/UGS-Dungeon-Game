using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameButton : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    private void Update()
    {
        if (DataHandler.data == null)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }
}
