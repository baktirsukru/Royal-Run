using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float xClamp = 5f;
    [SerializeField] float zClamp = 5f;

    Vector2 movement;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        HandleMovement();
    }

    public void Move(InputAction.CallbackContext context)
    {
            movement = context.ReadValue<Vector2>();
    }

    void HandleMovement()
    {
        Vector3 currentPosition = rb.position;
        Vector3 moveDirection = new Vector3(movement.x, 0f, movement.y);
        Vector3 newPosition = currentPosition + moveDirection * (moveSpeed * Time.fixedDeltaTime);

        newPosition.x = Mathf.Clamp(newPosition.x, -xClamp, xClamp);
        newPosition.z = Mathf.Clamp(newPosition.z, -zClamp, zClamp);
        

        rb.MovePosition(newPosition);
    }

}
