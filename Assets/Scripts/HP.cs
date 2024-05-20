using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class HP : MonoBehaviour
{
    [SerializeField] private Image[] lives;
    int hp = 2;

    void Start()
    {
        //lives = gameObject.GetComponentsInChildren<Image>();
    }
    
    public void MinusHP()
    {
        DeleteHeart(hp);
        --hp;
    }

    private void DeleteHeart(int hp)
    {
        if (lives.Length > hp && hp >= 0)
        {
            lives[hp].enabled = false;
        }
    }

    public void RestoreHp()
    {
        hp = 2;
        foreach (var item in lives)
        {
            item.enabled = true;
        }
    }
}
