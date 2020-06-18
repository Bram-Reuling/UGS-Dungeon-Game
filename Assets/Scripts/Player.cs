//////////////////////////////////////////////////////////////////
///
/// ---------------------- Player.cs -----------------------------
/// 
/// Made by: Bram Reuling
/// 
/// Description: Script for player behavior, such as moving
/// shooting, player death and camera movement.
/// 
/// Player.cs contains the following classes (made by me, not
/// made by the guys of unity):
/// - Die()
/// - PlayerMove()
/// - PlayerRun()
/// 
//////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int Health
    {
        get => playerHealth;
    }

    public int Level
    {
        get => level;
    }

    public int XP
    {
        get => xp;
    }

    public int XPForLevelUp
    {
        get => xpForLevelUp;
    }

    public Vector3 Position
    {
        get => transform.position;
    }

    public static Player GetPlayerInstance() => playerSingleton;


    [Header("Player Movement")]
    [SerializeField, Range(0f, 100f)]
    private float moveSpeed = 0;

    [SerializeField, Range(0f, 50f)]
    private float moveSpeedAddOnShift = 0;

    [SerializeField]
    private string horizontalAxis = "Horizontal";
    [SerializeField]
    private string verticalAxis = "Vertical";

    [Header("Camera Movement")]

    [SerializeField, Range(0, 10)]
    private int mouseSensitivity = 10;

    [Header("Other")]

    [Tooltip("Player Health in %"), Range(0, 100)]
    public int playerHealth = 100;

    private static Player playerSingleton = null;

    private float oldMoveSpeed;
    private Rigidbody rb;
    private bool isShiftClicked = false;
    private bool moving;
    private float xRotation = 0f;
    private Transform playerCamera;

    private float collisionTimer;

    public float timeThresholdForDamage = .5f;

    public int xp = 0;
    public int level = 0;
    public int xpForLevelUp = 10;

    private void Awake()
    {
        if (playerSingleton == null)
        {
            playerSingleton = this;
        }
        else
        {
            throw new Exception("There can only be one Player in a scene!");
        }

        Cursor.lockState = CursorLockMode.Locked;
        oldMoveSpeed = moveSpeed;
        mouseSensitivity *= 100;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        playerCamera = GameObject.Find("Main Camera").transform;

    }

    void Update()
    {
        // Player and Camera rotation

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Prevent the player to go 360 degrees with the camera on the vertical axis.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        if (xp >= xpForLevelUp)
        {
            xp -= xpForLevelUp;
            level++;
            xpForLevelUp += (int)UnityEngine.Random.Range(10, 20);
        }
    }

    // Used for physics based movement.
    void FixedUpdate()
    {
        PlayerRun();

        PlayerMove();
    }

    private void Die()
    {
        Debug.LogError("Function not implemented");
    }

    private void PlayerMove()
    {

        // Checks if the player is pressing the move buttons.
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        // If the player presses the move buttons, move the player in game.
        if (moving)
        {
            float moveHorizontal = Input.GetAxis(horizontalAxis);
            float moveVertical = Input.GetAxis(verticalAxis);

            Vector3 move = transform.right * moveHorizontal + transform.forward * moveVertical;

            move = move.normalized;

            rb.AddForce(move * moveSpeed);
        }
        // If not, set the velocity to zero to prevent the player to slide.
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void PlayerRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !isShiftClicked)
        {
            moveSpeed += moveSpeedAddOnShift;
            isShiftClicked = true;
        }

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = oldMoveSpeed;
            isShiftClicked = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // Resetting the timer
            collisionTimer = 0f;

            playerHealth -= 5;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collisionTimer < timeThresholdForDamage)
            {
                collisionTimer += Time.deltaTime;
            }
            else
            {
                playerHealth -= 5;

                collisionTimer = 0f;
            }
        }
    }

    public void AddXP(int _xp)
    {
        xp = _xp;
    }

    public void SavePlayer()
    {
        SaveAndLoad.Save(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveAndLoad.Load();

        playerHealth = data.health;
        xp = data.xp;
        level = data.level;
        xpForLevelUp = data.xpForLevelUp;

        transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
    }

    private void OnDestroy()
    {
        playerSingleton = null;
    }
}
