using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] float moveSpeed;

    private Vector3 playerVelocity;
    private float gravityValue = -9.81f;
    private bool groundedPlayer;
    private float targetAngle;
    private Vector3 startPosition;
    public Transform tr;

    public bool isMove = true;

    private ManagerTimer managerTimer;
    void Start()
    {
        managerTimer = FindObjectOfType<ManagerTimer>();
        characterController = GetComponent<CharacterController>();
        startPosition = tr.position;
        Debug.Log(startPosition);
    }

    void Update()
    {
        groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }


        if (isMove)
        {
            if (joystick.Horizontal != 0 && joystick.Vertical != 0)
            {
                targetAngle = Mathf.Atan2(playerVelocity.x, playerVelocity.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0.0f, targetAngle, 0.0f);
            }

            playerVelocity.x = joystick.Horizontal * moveSpeed;
            playerVelocity.y += gravityValue * Time.deltaTime;
            playerVelocity.z = joystick.Vertical * moveSpeed;

            characterController.Move(playerVelocity * Time.deltaTime); //new Vector3(joystick.Horizontal * moveSpeed, 0, joystick.Vertical * moveSpeed) * Time.deltaTime);
        }
    }

    public void MovePlayerToStartLevel()
    {
        tr.position = startPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        managerTimer.SetTime(other.gameObject);
    }

    public void MoveController()
    {
        isMove = !isMove;
    }
}
