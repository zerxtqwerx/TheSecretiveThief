using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    [SerializeField] private Image[] lives;
    int hp = 2;

    /*void Start()
    {
        lives = gameObject.GetComponentsInChildren<Image>();
    }*/
    
    public void MinusHP()
    {
        DeleteHeart(hp);
        --hp;
    }

    private void DeleteHeart(int hp)
    {
        lives[hp].enabled = false;
    }
}
