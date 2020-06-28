//////////////////////////////////////////////////////////////////
///
/// ---------------------- Minimap.cs ----------------------------
/// 
/// Made by: Bram Reuling
/// 
/// Description: Script for the minimap. The minimap rotates and
/// moves with the player.
/// 
/// Minimap.cs contains the following classes:
/// - NONE
/// 
//////////////////////////////////////////////////////////////////
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{

    #region Editor Variable Declarations

    [SerializeField]
    private Transform player;

    #endregion

    #region Private Methods

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("The transform of the player is missing!");
        }
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            Vector3 newPosition = player.position;
            newPosition.y = transform.position.y;
            transform.position = newPosition;

            transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
        }
    }

    #endregion

}
