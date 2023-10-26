using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightOnPlayer : MonoBehaviour
{
    Light light;
    GameObject player;
    bool lightTurnOn = false;
    public bool LightTurnOn => lightTurnOn;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        light = player.transform.GetChild(3).GetComponent<Light>();
        light.enabled = false;
    }

    public void TurnOnLight()
    {
        light.enabled = true;
        lightTurnOn = true;
    }

    public void TurnOffLight()
    {
        light.enabled = false;
        lightTurnOn = false;
    }
}

