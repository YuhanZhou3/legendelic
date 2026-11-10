
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float WalkSpeed = 5.5f;
    [SerializeField] private float RunningSpeed = 9.0f;
    [SerializeField] private float JumpForce = 8.0f;
    [SerializeField] private float Gravity = 20.0f;

    [Header("Looking")]
    [SerializeField] private float LookSensitivity = 0.2f;
    [SerializeField] private float LookAngleLimit = 90.0f;

    private Camera mainCamera;
    private CharacterController characterController;

    private InputAction moveInput;
    private InputAction runInput;
    private InputAction jumpInput;

    private float currentMoveSpeed;
    private Vector3 moveDirection = Vector3.zero;
    private float lookAngle = 0f;
    private bool jumped = false;

    // true = mouse interacts with UI
    // false = mouse controls camera
    private bool uiMode = true;

    private void Start()
    {
        mainCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();

        moveInput = InputSystem.actions.FindAction("Move");
        runInput = InputSystem.actions.FindAction("Sprint");
        jumpInput = InputSystem.actions.FindAction("Jump");

        currentMoveSpeed = WalkSpeed;

        jumpInput.started += Jumped;

        SetUIMode(true);
    }

    private void OnDestroy()
    {
        if (jumpInput != null)
            jumpInput.started -= Jumped;
    }

    private void Update()
    {
        // Toggle between UI and exploration modes
        if (Keyboard.current != null)
        {
            if (Keyboard.current.tabKey.wasPressedThisFrame)
                SetUIMode(!uiMode);

            if (Keyboard.current.escapeKey.wasPressedThisFrame)
                SetUIMode(true);
        }

        // Don't control the player while interacting with UI
        if (uiMode)
            return;

        Vector2 moveVector = moveInput.ReadValue<Vector2>();
        Vector2 mouseDelta = Mouse.current != null
            ? Mouse.current.delta.ReadValue()
            : Vector2.zero;

        if (!characterController.isGrounded)
            jumped = false;

        currentMoveSpeed = runInput.IsPressed()
            ? RunningSpeed
            : WalkSpeed;

        HandleMovement(moveVector);
        HandleLooking(mouseDelta);
    }

    private void HandleMovement(Vector2 moveVector)
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float oldY = moveDirection.y;

        Vector2 newSpeed = new Vector2(
            moveVector.y * currentMoveSpeed,
            moveVector.x * currentMoveSpeed
        );

        moveDirection = (forward * newSpeed.x) + (right * newSpeed.y);

        moveDirection.y = (jumped && characterController.isGrounded)
            ? JumpForce
            : oldY;

        if (!characterController.isGrounded)
            moveDirection.y -= Gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void HandleLooking(Vector2 mouseDelta)
    {
        lookAngle += -mouseDelta.y * LookSensitivity;
        lookAngle = Mathf.Clamp(
            lookAngle,
            -LookAngleLimit,
            LookAngleLimit
        );

        mainCamera.transform.localRotation =
            Quaternion.Euler(lookAngle, 0, 0);

        transform.rotation *=
            Quaternion.Euler(0, mouseDelta.x * LookSensitivity, 0);
    }

    private void Jumped(InputAction.CallbackContext _)
    {
        jumped = true;
    }

    public void SetUIMode(bool enabled)
    {
        uiMode = enabled;

        Cursor.lockState = uiMode
            ? CursorLockMode.None
            : CursorLockMode.Locked;

        Cursor.visible = uiMode;
    }
}
