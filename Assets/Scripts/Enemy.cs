//////////////////////////////////////////////////////////////////
///
/// ---------------------- Enemy.cs -----------------------------
/// 
/// Made by: Bram Reuling
/// 
/// Description: Script for enemy behavior, such as moving
/// attacking and enemy death.
/// 
/// Enemy.cs contains the following classes:
/// - Private StateSwitcher()
/// - Private StateManager()
/// - Public TakeDamage()
/// - Private Die()
/// 
//////////////////////////////////////////////////////////////////

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class Enemy : MonoBehaviour
{

    #region Editor Variable Declarations

    // Variables that can be changed in the editor
    [Header("Movement")]
    [SerializeField, Tooltip("Movement speed of the Enemy"), Range(0f, 10f)]
    private int moveSpeed;

    [SerializeField]
    private int runSpeed;

    [Header("Player Detection")]
    [SerializeField, Tooltip("The closest the player needs to be from the enemy to be detected.")]
    private float maxLength;

    [SerializeField, Tooltip("The closest the player needs to be from the enemy to be damaged.")]
    private float minLength;

    [Header("Health and Damage")]
    [SerializeField]
    private int damage = 5;

    [SerializeField]
    private int health = 20;

    [Header("Other")]
    [SerializeField]
    private Player player;
    [SerializeField]
    private List<Transform> _wayPoints = new List<Transform>();

    #endregion

    #region Non-editor Variable Declarations

    // Variables that cannot be changed in the editor.
    private float length;
    private enum State { Idle, Chasing }
    private State state;
    private NavMeshAgent agent;
    private int currentWayPoint = 0;

    #endregion

    #region Getters and Setters

    public int Health
    {
        get
        {
            return health;
        }
    }

    #endregion

    #region Private Methods

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("There is no player attached to the enemy!");
        }

        health += player.Level * 2;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        // Getting the length of the vector between the player and the enemy
        Vector3 vectorToPlayerFromEnemy = transform.position - player.transform.position;
        length = vectorToPlayerFromEnemy.magnitude;

        StateSwitcher();

        StateManager();
    }

    private void StateSwitcher()
    {
        if (length <= maxLength)
        {
            state = State.Chasing;
        }
        else
        {
            state = State.Idle;
        }
    }

    private void StateManager()
    {
        if (state == State.Chasing)
        {
            agent.speed = runSpeed;
            agent.destination = player.transform.position;
        }

        if (state == State.Idle)
        {
            agent.speed = moveSpeed;

            if (currentWayPoint < _wayPoints.Count)
            {
                if (Vector3.Distance(transform.position, _wayPoints[currentWayPoint].position) > 1.5f)
                {
                    agent.destination = _wayPoints[currentWayPoint].position;
                }
                else
                {
                    currentWayPoint++;
                }
            }
            else
            {
                currentWayPoint = 0;
            }
        }
    }

    private void Die()
    {
        // If this instance dies add one to the total of enemies killed
        // in the DataHandler. If the level is over it will show the total
        // number of enemies killed.
        DataHandler.enemiesKilled += 1;
        Destroy(gameObject);
    }

    #endregion

    #region Public Methods

    public void TakeDamage (int amountOfDamage)
    {
        health -= amountOfDamage;

        if (health <= 0)
        {

            player.AddXP((int)Random.Range(5, 20));

            Die();
        }
    }

    #endregion

}
