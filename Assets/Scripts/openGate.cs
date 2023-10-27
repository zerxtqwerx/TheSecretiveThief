using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class openGate : MonoBehaviour
{
    public GameObject gate;
    public Button but;
    bool flag = false;

    private void Update()
    {
        if (flag)
        {
            if (gate.transform.position.x < 25f)
            {
                gate.transform.Translate(new Vector3(0f, 0f, -1f) * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            but.gameObject.SetActive(true);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        but.gameObject.SetActive(false);
    }

    public void ButClick()
    {
        but.gameObject.SetActive(false);
        flag = true;

    }
}
