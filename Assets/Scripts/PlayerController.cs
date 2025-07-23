using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float xClamp = 3f;
    [SerializeField] float zClamp = 3f;
    Vector2 movement;
    Rigidbody myRigidbody;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void FixedUpdate()
    {
        HandleMovement();
    }
    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }
    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();

    }

    void HandleMovement()
    {
        Vector3 currentPosition = myRigidbody.position;
        Vector3 moveDirection = new Vector3(movement.x, 0f, movement.y) * (moveSpeed * Time.fixedDeltaTime);
        
        Vector3 newPosition = currentPosition + moveDirection * (moveSpeed * Time.fixedDeltaTime);
        newPosition.x = Mathf.Clamp(newPosition.x, -xClamp, xClamp);
        newPosition.z = Mathf.Clamp(newPosition.z, -zClamp, zClamp);

        myRigidbody.MovePosition(newPosition);
    }
    
}
