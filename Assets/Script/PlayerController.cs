using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Movement Variables")]
    public float walkSpeed = 6f;
    public float jumpPower = 7f;
    public float gravity = 20f; // Increased gravity for more "realistic" jump
    public bool canMove = true;
    [Header("Player View Variables")]
    public Camera playerCamera;
    public float lookSpeed = 2f;
    public float lookYLimit = 45f;

    Vector3 moveDirection = Vector3.zero;
    float viewRotation = 0;
    CharacterController characterController;

    public bool isPlayerDead { get; private set; } = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float curSpeedX = canMove ? walkSpeed * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? walkSpeed * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Player and Camera rotation
        if (canMove)
        {
            viewRotation += -Input.GetAxis("Mouse Y") * lookSpeed;
            viewRotation = Mathf.Clamp(viewRotation, -lookYLimit, lookYLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(viewRotation, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            isPlayerDead = true;
            Debug.LogWarning("Game Over: Player has been hit");
        }
    }
}