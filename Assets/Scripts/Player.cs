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
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{

    #region Getters and Setters

    public int Health
    {
        get => playerHealth;
    }

    public int Level { get; private set; } = 0;

    public int XP { get; private set; } = 0;

    public int XPForLevelUp { get; private set; } = 100;

    public Vector3 Position
    {
        get => transform.position;
    }

    #endregion

    #region Editor Variable Declarations

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

    [SerializeField, Tooltip("Player Health in %"), Range(0, 100)]
    private int playerHealth = 100;

    [SerializeField]
    private SceneLoader sceneLoader;
    [SerializeField]
    private Game game;

    #endregion

    #region Non-editor Variable Declarations

    private static Player playerSingleton = null;

    private float oldMoveSpeed;
    private Rigidbody rb;
    private bool isShiftClicked = false;
    private bool moving;
    private float xRotation = 0f;
    private Transform playerCamera;

    private float collisionTimer;

    private readonly float timeThresholdForDamage = .5f;
    private readonly int xpBase = 100;
    private readonly float xpIncrease = 1.15f;

    #endregion

    #region Private Methods

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
        Cursor.visible = false;
        oldMoveSpeed = moveSpeed;
        mouseSensitivity *= 100;

        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    private void Start()
    {

        playerCamera = GameObject.Find("Main Camera").transform;

        if (DataHandler.data != null)
        {
            LoadPlayer(DataHandler.data);
        }

    }

    private void Update()
    {
        // Player and Camera rotation

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Prevent the player to go 360 degrees with the camera on the vertical axis.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        if (playerHealth <= 0)
        {
            Die();
        }
    }

    // Used for physics based movement.
    private void FixedUpdate()
    {
        PlayerRun();

        PlayerMove();

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            //Debug.Log(hit.transform.tag);

            if (hit.transform.CompareTag("Finish"))
            {
                game.SaveGame();
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                DataHandler.levelPlayer = Level;
                DataHandler.healthPlayer = Health;
                sceneLoader.ChangeScene("LevelComplete");
            }
        }
    }

    private void Die()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        sceneLoader.ChangeScene("DeathScreen");
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

    private void OnDestroy()
    {
        playerSingleton = null;
    }

    #endregion

    #region Public Methods

    public void AddXP(int _xp)
    {
        XP += _xp;

        if (XP >= XPForLevelUp)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        XP -= XPForLevelUp;
        Level++;
        float t = Mathf.Pow(xpIncrease, Level);
        XPForLevelUp = (int)Mathf.Floor(xpBase * t);
    }

    public void LoadPlayer(SceneData data)
    {
        playerHealth = data.health;
        XP = data.xp;
        Level = data.level;
        XPForLevelUp = data.xpForLevelUp;
    }

    #endregion

}
