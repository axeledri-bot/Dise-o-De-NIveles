using UnityEngine;

public class FlyCamera : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public float sprintMultiplier = 3f;
    public float verticalSpeed = 1f;

[Header("Mouse Look")]
    public float mouseSensitivity = 1f;
    public float smoothTime = 0.05f;

    [Header("Control")]
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode upKey = KeyCode.Q;
    public KeyCode downKey = KeyCode.E;
    public KeyCode toggleCursorKey = KeyCode.Escape;

    private float rotationX;
    private float rotationY;

    private Vector2 currentMouseDelta;
    private Vector2 currentMouseDeltaVelocity;

    private bool cursorLocked = true;

    void Start()
    {
        LockCursor(true);
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
        HandleCursorToggle();
    }

    void HandleMouseLook()
    {
        if (!cursorLocked) return;

        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        rotationY += mouseX * mouseSensitivity * 100f * Time.deltaTime;
        rotationX -= mouseY * mouseSensitivity * 100f * Time.deltaTime;

        rotationX = Mathf.Clamp(rotationX, -89f, 89f);

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);

    }

    void HandleMovement()
    {
        float speed = moveSpeed;

        if (Input.GetKey(sprintKey))
            speed *= sprintMultiplier;

        Vector3 direction = Vector3.zero;

        direction += transform.forward * Input.GetAxis("Vertical");
        direction += transform.right * Input.GetAxis("Horizontal");

        if (Input.GetKey(upKey))
            direction += Vector3.up * verticalSpeed;

        if (Input.GetKey(downKey))
            direction += Vector3.down * verticalSpeed;

        transform.position += direction * speed * Time.deltaTime;
    }

    void HandleCursorToggle()
    {
        if (Input.GetKeyDown(toggleCursorKey))
        {
            cursorLocked = !cursorLocked;
            LockCursor(cursorLocked);
        }
    }

    void LockCursor(bool locked)
    {
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }

}
