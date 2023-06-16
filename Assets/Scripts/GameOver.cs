using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private Text time;
    private Image heart1;
    private Image heart2;
    private Image heart3;
    private ManagerTimer mt;

    void Start()
    {
        mt = FindObjectOfType<ManagerTimer>();
    }

    void Awake()
    {
        time.text = mt.GameTime() + "c";
    }
}
