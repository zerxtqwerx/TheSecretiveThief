using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] float moveSpeed;

    public bool isMove = true;

    private ManagerTimer managerTimer;
    void Start()
    {
        managerTimer = FindObjectOfType<ManagerTimer>();
        characterController = GetComponent<CharacterController>();
    }


    void Update()
    {
        if(isMove)
            characterController.Move(new Vector3(joystick.Horizontal * moveSpeed, 0, joystick.Vertical * moveSpeed) * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        managerTimer.SetTime(other.gameObject);
    }

    public void MoveController()
    {
        if (isMove) isMove = false;
        else isMove = true;
    }
}
