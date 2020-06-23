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
/// - StateSwitcher()
/// - StateManager()
/// - TakeDamage()
/// - Die()
/// 
//////////////////////////////////////////////////////////////////

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class Enemy : MonoBehaviour
{
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

    // Variables that cannot be changed in the editor.
    private float length;
    private enum State { Idle, Chasing }
    private State state;
    private NavMeshAgent _agent;
    private int _currentWayPoint = 0;

    public int Health
    {
        get
        {
            return health;
        }
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            Debug.LogError("There is no player attached to the enemy!");
        }

        health += (int)Mathf.Floor(player.level * 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // Getting the length of the vector between the player and the enemy
        Vector3 _diff = transform.position - player.transform.position;
        length = _diff.magnitude;

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
            _agent.speed = runSpeed;
            _agent.destination = player.transform.position;
        }

        if (state == State.Idle)
        {
            _agent.speed = moveSpeed;

            if (_currentWayPoint < _wayPoints.Count)
            {
                if (Vector3.Distance(transform.position, _wayPoints[_currentWayPoint].position) > 1.5f)
                {
                    _agent.destination = _wayPoints[_currentWayPoint].position;
                }
                else
                {
                    _currentWayPoint++;
                }
            }
            else
            {
                _currentWayPoint = 0;
            }
        }
    }

    public void TakeDamage (int amount)
    {
        health -= amount;

        if (health <= 0)
        {

            player.AddXP((int)Random.Range(5, 20));

            Die();
        }
    }

    void Die()
    {
        // If this instance dies add one to the total of enemies killed
        // in the DataHandler. If the level is over it will show the total
        // number of enemies killed.
        DataHandler.enemiesKilled += 1;
        Destroy(gameObject);
    }
}
