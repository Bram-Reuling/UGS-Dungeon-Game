//////////////////////////////////////////////////////////////////
///
/// ---------------------- LoadGameButton.cs ---------------------
/// 
/// Made by: Bram Reuling
/// 
/// Description: Script for showing the Load Game button if there
/// is any previous game data.
/// 
/// LoadGameButton.cs contains the following classes:
/// - NONE
/// 
//////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEngine.UI;

public class LoadGameButton : MonoBehaviour
{
    private Button button;

    private void Awake()
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
