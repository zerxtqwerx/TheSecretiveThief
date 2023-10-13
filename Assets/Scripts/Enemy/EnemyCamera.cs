using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

public class EnemyCamera : MonoBehaviour
{
    public float speed = 20f;
    public float rightBorder = 50f;
    public float leftBorder = 24f;

    [SerializeField] private float maxDistanceToFind = 7f;
    [SerializeField] private float timeToFindAfterDisappearance = 3f;
    public LayerMask layerWithoutPeople;

    bool rot = false;
    private GameObject player;
    public float time;
    private float normalDistance = 1f;

    private bool isTime;
    public bool IsTime => isTime;
    private bool isPlayerFind;
    public bool IsPlayerFind => isPlayerFind;

    private ManagerTimer managerTimer;
    public bool isPlayerBehindTheWall = true;

    void Start()
    {
        managerTimer = FindObjectOfType<ManagerTimer>();
        player = GameObject.FindGameObjectWithTag("player");
    }

    void Update()
    {
        if (isTime) Timer();
        FindPlayer();


        if (isPlayerFind && time == timeToFindAfterDisappearance && !isPlayerBehindTheWall)
            managerTimer.RageAddition();

        Rotation();
    }

    private void FindPlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, layerWithoutPeople))
        {
            if (hit.transform.GetComponent<PlayerMovement>()) isPlayerBehindTheWall = false;
            else isPlayerBehindTheWall = true;
        }


        if (isPlayerFind || isTime)
        {
            if (IsNormalDistance() == true)
            {
                //GoToPlayer();
                Debug.Log("find player");
            }
        }
        
        Debug.DrawRay(transform.position, (transform.right * 3 + -transform.right * 2) * 2f);
        Debug.DrawRay(transform.position, (transform.right * 3 + -transform.forward * 2) * 2f);
        Debug.DrawRay(transform.position, player.transform.position - transform.position);


        if (Vector3.Angle(player.transform.position - transform.position, transform.forward) < 54f && Vector3.Distance(transform.position, player.transform.position) < maxDistanceToFind && !isPlayerBehindTheWall)
        {
            isPlayerFind = true;
            isTime = true;
            time = timeToFindAfterDisappearance;
        }
    }

    private bool IsNormalDistance()
    {
        if (CalculateDistance() >= normalDistance)
            return true;
        return false;
    }

    private float CalculateDistance()
    {
        float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
        return distance;
    }

    private void Timer()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            isTime = false;
            isPlayerFind = false;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void Rotation()
    {
        float y = gameObject.transform.rotation.y * 100;
        if (!rot) 
        {
            gameObject.transform.Rotate(new Vector3(0f, speed, 0f) * Time.deltaTime);
            if (y > rightBorder)
                rot = true;
        }

        else if (rot)
        { 
            gameObject.transform.Rotate(new Vector3(0f, -speed, 0f) * Time.deltaTime);
            if (y < leftBorder)
                rot = false;
        }
    }
}
