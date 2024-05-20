using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyBlack : MonoBehaviour
{
    [SerializeField] private float maxDistanceToFind = 7f;
    [SerializeField] private float timeToFindAfterDisappearance = 3f;
    public LayerMask layerWithoutPeople;

    private UnityEngine.AI.NavMeshAgent agent;
    private List<Vector3> posMovement = new List<Vector3>();
    private int nowIndexPosition = 0;
    private GameObject player;
    public float time;
    private float normalDistance = 1f;

    private bool isTime;
    public bool IsTime => isTime;
    private bool isPlayerFind;
    public bool IsPlayerFind => isPlayerFind;

    private ManagerTimer managerTimer;
    public bool isPlayerBehindTheWall = true;
    public string patrolTag;

    LightOnPlayer lop;
    GameObject[] enemies;

    void Start()
    {
        managerTimer = FindObjectOfType<ManagerTimer>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        GameObject patrolPoints = GameObject.FindGameObjectWithTag(patrolTag);
        for (int i = 0; i < patrolPoints.transform.childCount; i++)
        {
            posMovement.Add(patrolPoints.transform.GetChild(i).transform.position);
        }
        player = GameObject.FindGameObjectWithTag("player");
        lop = GetComponent<LightOnPlayer>();

        enemies = GameObject.FindGameObjectsWithTag("enemy");
    }


    void Update()
    {
        if (isTime) Timer();

        FindPlayer();


        if (isPlayerFind && time == timeToFindAfterDisappearance && !isPlayerBehindTheWall)
            managerTimer.RageAddition();
    }

    private bool IsNormalDistance()
    {
        if (CalculateDistance() >= normalDistance)
            return true;
        return false;
    }

    private float CalculateDistance()
    {
        float distance = Vector3.Distance(player.transform.position, agent.transform.position);
        return distance;
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
                GoToPlayer();
                lop.TurnOnLight();
                UniversalPlayerSearch();
            }
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
        if (time <= 0)
        {
            isTime = false;
            isPlayerFind = false;

            if (normalDistance > 1)
            {
                lop.TurnOffLight();
                StopUniversalSearchController();
            }
        }
    }

    private void UniversalPlayerSearch()
    {
        foreach (GameObject enemy in enemies)
        {
            var em = enemy.GetComponent<EnemyMovement>();
            if(em != null)
            {
                em.UniversalSearch();
            }
            else
            {
                enemy.GetComponent<EnemyBlack>().UniversalSearch();
            }
        }
    }

    private void StopUniversalSearchController()
    {
        foreach (GameObject enemy in enemies)
        {
            var em = enemy.GetComponent<EnemyMovement>();
            if (em != null)
            {
                em.StopUniversalSearch();
            }
            else
            {
                enemy.GetComponent<EnemyBlack>().StopUniversalSearch();
            }
        }
    }

    public void UniversalSearch()
    {
        ////не работает
        normalDistance = 1.25f;
        isPlayerFind = true;
    }

    public void StopUniversalSearch()
    {
        normalDistance = 1f;
        isPlayerFind = false;
    }
}
   
