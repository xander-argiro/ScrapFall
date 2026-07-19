using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Transform cameraTransform;

    public bool gameOver;

    public float MAX_SPEED_X = 10f;
    public float ACCELERATION = 5f;
    public float DECELERATION = 5f;
    public float MIN_SPEED_Y = -15f;
    public float GRAVITY = 9.81f;
    public float JUMP_VELOCITY = 5f;

    public float velocityY;
    public float velocityX;

    public int Life_Current = 100;
    public int Life_Max = 100;

    private CharacterController controller;
    private Vector2 moveInput;

    void Awake()
    {
        gameOver = false;
        
        controller = GetComponent<CharacterController>();

        velocityY = 0f;
        velocityX = 0f;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the player to face the camera's forward direction, ignoring vertical rotation
        Vector3 lookDirection = cameraTransform.forward;
        lookDirection.y = 0f;
        transform.forward = lookDirection.normalized;

        if (Life_Current <= 0 && !gameOver)
        {
            GameManager gameManager = FindAnyObjectByType<GameManager>();
            gameManager.GameOver();
        }

        if (!gameOver)
        {
            // Get camera direction vectors

            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            Vector3 inputDirection = (forward * moveInput.y + right * moveInput.x).normalized;

            // Apply small negative vertical velocity to keep the player grounded
            if (controller.isGrounded && velocityY < 0f)
            {
                velocityY = -2f;
            }

            velocityY -= GRAVITY * Time.deltaTime;

            // Apply movement

            Vector3 movement = inputDirection * MAX_SPEED_X;
            movement.y = velocityY;

            controller.Move(movement * Time.deltaTime);
        }
    }

    void OnJump()
    {
        if (controller.isGrounded)
        {
            velocityY = JUMP_VELOCITY;
        }
    }
}
