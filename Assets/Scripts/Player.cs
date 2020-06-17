using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int GetPlayerHealth => playerHealth;

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

    static Player playerSingleton = null;

    private float oldMoveSpeed;
    private Rigidbody rb;
    private bool isShiftClicked = false;
    private bool moving;
    private float xRotation = 0f;
    private Transform playerCamera;

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
    }

    // Used for physics based movement.
    void FixedUpdate()
    {
        PlayerRun();

        PlayerMove();
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

    private void OnDestroy()
    {
        playerSingleton = null;
    }
}
