using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]

    [SerializeField]
    [Range(0f, 100f)]
    private float moveSpeed = 0;

    [SerializeField]
    [Range(0f, 50f)]
    private float moveSpeedAddOnShift = 0;

    [SerializeField]
    private string horizontalAxis = "Horizontal";
    [SerializeField]
    private string verticalAxis = "Vertical";

    [Header("Camera Movement")]

    [SerializeField]
    [Range(0, 10)]
    private int mouseSensitivity = 10;

    [SerializeField]
    private Transform playerCamera;

    public static Player GetPlayer()
    {
        return playerSingleton;
    }

    static Player playerSingleton = null;

    private float oldMoveSpeed;
    private Rigidbody rb;
    private bool isShiftClicked = false;
    private float xRotation = 0f;

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
        rb = GetComponent<Rigidbody>();
        mouseSensitivity *= 100;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

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
        float moveHorizontal = Input.GetAxis(horizontalAxis);
        float moveVertical = Input.GetAxis(verticalAxis);

        Vector3 move = transform.right * moveHorizontal + transform.forward * moveVertical;

        rb.AddForce(move * moveSpeed);
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
