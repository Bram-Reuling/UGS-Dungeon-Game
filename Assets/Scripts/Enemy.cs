//////////////////////////////////////////////////////////////////
///
/// ---------------------- Enemy.cs -----------------------------
/// 
/// Made by: Bram Reuling
/// 
/// Description: Script for enemy behavior, such as moving
/// attacking and enemy death.
/// 
/// Player.cs contains the following classes (made by me, not
/// made by the guys of unity):
/// - 
/// 
//////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Movement speed of the Enemy"), Range(0f, 10f)]
    public int moveSpeed;

    [Header("Player Detection")]
    [Tooltip("The closest the player needs to be from the enemy to be detected.")]
    public float maxLength;

    [Tooltip("The closest the player needs to be from the enemy to be damaged.")]
    public float minLength;

    private GameObject player;
    private float length;

    public int damage = 5;

    public int health = 10;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 _diff = transform.position - player.transform.position;
        length = _diff.magnitude;

        if (length <= maxLength)
        {
            transform.LookAt(player.transform.position);

            Vector3 _normalDiff = _diff.normalized;

            transform.position -= _normalDiff * Time.deltaTime * moveSpeed;
        }
    }

    public void TakeDamage (int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
