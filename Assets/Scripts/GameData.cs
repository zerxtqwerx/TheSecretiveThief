using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//doesnt use

public class GameData : MonoBehaviour
{
    private int money;

    public void ChangeAmountOfMoney(int amount)
    {
        money += amount;
    }
}
