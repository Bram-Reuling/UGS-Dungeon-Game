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

public class Minimap : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
