using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public Text amountOfMoney;
    public Text collectedLevelMoney;

    static private int currentMoney;
    private int collectedMoneyOnThisLevel;

    public int GetMoney() { return currentMoney; }
    public void SetMoney(int n) { currentMoney = n; }

    private void Start()
    {
        currentMoney = 10000;
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
        collectedLevelMoney.text = collectedMoneyOnThisLevel + " $";
    }

    public void AddMoneyOnFinishLevel()
    {
        currentMoney += collectedMoneyOnThisLevel;
        
        DeleteCollectedMoney();
        ShowCurrentAmount();
    }

    public void DeleteCollectedMoney()
    {
        collectedMoneyOnThisLevel = 0;
    }

    public bool PurchasingPermission(int price)
    {
        if (currentMoney >= price)
        {
            Debug.Log("da");
            return true;
        }
        return false;
        //return currentMoney >= price;
    }
}
