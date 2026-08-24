using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference lookAction;

    [Header("Look Settings")]
    [SerializeField] private Transform playerBody;
    [SerializeField] private float mouseSensitivity = 0.1f;
    [SerializeField] private float maxLookAngle = 85f;

    private float verticalRotation;

    private void OnEnable()
    {
        lookAction.action.Enable();
    }

    private void OnDisable()
    {
        lookAction.action.Disable();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleLook();
    }

    private void HandleLook()
    {
        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();

        float yaw = lookInput.x * mouseSensitivity;
        float pitch = lookInput.y * mouseSensitivity;

        verticalRotation -= pitch;

        verticalRotation = Mathf.Clamp(
            verticalRotation,
            -maxLookAngle,
            maxLookAngle
        );

        transform.localRotation =
            Quaternion.Euler(verticalRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * yaw);
    }
}
