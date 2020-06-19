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

    public ParticleSystem explosion;

    private bool sawPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        health += (int)Mathf.Floor(player.GetComponent<Player>().level * 1.5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 _diff = transform.position - player.transform.position;
        length = _diff.magnitude;

        if (length <= maxLength)
        {
            sawPlayer = true;
        }

        if (sawPlayer)
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

            player.gameObject.GetComponent<Player>().AddXP((int)Random.Range(5, 10));

            Die();
        }
    }

    void Die()
    {
        //Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        
    }
}
