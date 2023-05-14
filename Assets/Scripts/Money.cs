using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public Text amountOfMoney;
    private int currentMoney;
    public int collectedMoneyOnThisLevel;

    private void Start()
    {
        ShowCurrentAmount();
    }

    private void ShowCurrentAmount()
    {
        amountOfMoney.text = currentMoney + " $";
    }

    public void ChangeAmountOfMoney(int amount)
    {
        currentMoney += amount;
        ShowCurrentAmount();
    }

    public void CollectMoneyOnLevel(int amount)
    {
        collectedMoneyOnThisLevel += amount;
    }

    public void AddMoneyonFinishLevel()
    {
        currentMoney += collectedMoneyOnThisLevel;
        DeleteCollectedMoney();
    }

    public void DeleteCollectedMoney()
    {
        collectedMoneyOnThisLevel = 0;
    }
}
