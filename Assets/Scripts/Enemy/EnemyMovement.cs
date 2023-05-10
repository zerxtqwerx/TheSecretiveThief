using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float maxDistanceToFind = 7f;
    [SerializeField] private float timeToFindAfterDisappearance = 3f;
    public LayerMask layerWithoutPeople;
    //d

    private NavMeshAgent agent;
    private List<Vector3> posMovement = new List<Vector3>();
    private int nowIndexPosition = 0;
    private GameObject player;
    public float time;
    private bool isTime;
    private bool isPlayerFind;
    private ManagerTimer managerTimer;
    public bool isPlayerBehindTheWall = true;

    void Start()
    {
        managerTimer = FindObjectOfType<ManagerTimer>();
        agent = GetComponent<NavMeshAgent>();
        for (int i = 0; i < transform.childCount; i++)
        {
            posMovement.Add(transform.GetChild(i).transform.position);
        }
        player = GameObject.FindGameObjectWithTag("player");
    }


    void Update()
    {
        if (isTime) Timer();

        FindPlayer();

        if(isPlayerFind && time == timeToFindAfterDisappearance && !isPlayerBehindTheWall)
        managerTimer.RageAddition();
    }


    
    private void FindPlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, player.transform.position - transform.position,out hit, layerWithoutPeople))
        {
            if(hit.transform.GetComponent<PlayerMovement>()) isPlayerBehindTheWall = false;
            else isPlayerBehindTheWall = true;
        }


            if (isPlayerFind || isTime)
        {
            GoToPlayer();
           
        }
        else
        {
            agent.SetDestination(posMovement[nowIndexPosition]);

            if (Vector3.Distance(transform.position, posMovement[nowIndexPosition]) < 1f)
            {
                nowIndexPosition += 1;
                if (nowIndexPosition >= posMovement.Count) nowIndexPosition = 0;
            }

        }



        Debug.DrawRay(transform.position, (transform.forward * 3 + transform.right * 2) * 2f);
        Debug.DrawRay(transform.position, (transform.forward * 3 + -transform.right * 2) * 2f);
        Debug.DrawRay(transform.position, player.transform.position - transform.position);


        if (Vector3.Angle(player.transform.position - transform.position, transform.forward) < 54f && Vector3.Distance(transform.position, player.transform.position) < maxDistanceToFind && !isPlayerBehindTheWall)
        {
                isPlayerFind = true;
                isTime = true;
                time = timeToFindAfterDisappearance;
        }
    }


    private void GoToPlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    private void Timer()
    {
        time -= Time.deltaTime;
        if(time <= 0 )
        {
            isTime = false;
            isPlayerFind = false;
        }
    }
}
