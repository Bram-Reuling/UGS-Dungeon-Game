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

[RequireComponent(typeof(Button))]

public class LoadGameButton : MonoBehaviour
{

    #region Non-editor Variable Declarations

    private Button button;

    #endregion

    #region Private Methods

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

    #endregion

}
