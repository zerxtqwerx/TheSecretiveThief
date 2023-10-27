using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class openSafe : MonoBehaviour
{
    public GameObject safe;
    public Button but;
    bool flag = false;

    private void Update()
    {
        if (flag)
        {
            if (safe.transform.rotation.z < 0.5f)
            {
                safe.transform.Rotate(new Vector3(0f, 0f, 10f) * Time.deltaTime);
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
