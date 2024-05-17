using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] public Transform playerCamera;

    [Header("Mouse Settings")]
    [SerializeField][Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] float mouseSensitivity = 3.5f;

    [Header("Cursor Settings")]
    [SerializeField] bool cursorLock = true;

    [Header("Movement Settings")]
    [SerializeField] float Speed = 6.0f;
    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] float gravity = -30f;

    [Header("Lives Settings")]
    [SerializeField] public int heartAmount = 3;
    [SerializeField] float timer = 5f;
    [SerializeField] GameObject image1;
    [SerializeField] GameObject image2;
    [SerializeField] GameObject image3;
    [SerializeField] GameObject GO;
    [SerializeField] CommandManager CM;
    [SerializeField] SpeechInput SI;


    [Header("Ground Check")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] bool isGrounded;

    public float jumpHeight = 6f;
    float velocityY;


    float cameraCap;
    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;

    CharacterController controller;
    Vector2 currentDir;
    Vector2 currentDirVelocity;
    Vector3 velocity;


    //=============================================================
    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Automatically find the camera if not assigned in the Inspector
        if (playerCamera == null)
        {
            playerCamera = GetComponentInChildren<Camera>()?.transform;
            if (playerCamera == null)
            {
                Debug.LogError("MouseLook: No playerCamera assigned or found in children.");
            }
        }


        //Locks Cursor & Makesa Cursor Visible
        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
    }

    void Update()
    {
        UpdateMouse();
        UpdateMove();

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

    }

    void UpdateMouse()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraCap -= currentMouseDelta.y * mouseSensitivity;

        cameraCap = Mathf.Clamp(cameraCap, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraCap;

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMove()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.5f, ground);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (isGrounded! && controller.velocity.y < -1f)
        {
            velocityY = -8f;
        }


        //Get Player Input - WASD - NEEDS TO BE CHANGED
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        //Gravity 
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);
        velocityY += gravity * 2f * Time.deltaTime;

        //Use Controller to Move
        velocity = (transform.forward * currentDir.y) * Speed + (transform.right * currentDir.x) * Speed + (Vector3.up * velocityY);
        controller.Move(velocity * Time.deltaTime);

        
    }



    public void LoseLife()
    {


        //Check Invc-timer
        if (timer <= 0)
        {
            heartAmount -= 1;
            UpdateLivesDisplay();

            if (heartAmount <= 0)
            {
                heartAmount = 0; // Ensure it doesn't go below zero
                GameExit();
            }
            timer = 5f;
        }
    }

    void UpdateLivesDisplay()
    {
        image1.SetActive(heartAmount > 0);
        image2.SetActive(heartAmount > 1);
        image3.SetActive(heartAmount > 2);
    }

    void GameExit()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        controller.enabled = false; // Disable the controller or player movement
        GO.SetActive(true); // Assuming there's a GameOver screen GameObject
        CM.disable(false);
        SI.isOn = false;
    }


}
