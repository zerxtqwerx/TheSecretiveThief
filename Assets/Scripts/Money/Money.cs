using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour, IDataSave
{
    public Text amountOfMoney;
    public Text collectedLevelMoney;

    private int currentMoney;
    private int collectedMoneyOnThisLevel;

    public int GetMoney() { return currentMoney; }
    public void SetMoney(int n) { currentMoney = n; }

    private void Start()
    {
        currentMoney = 1000;
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
            return true;
        }
        return false;
        //return currentMoney >= price;
    }
    public void LoadData(GameData data)
    {
        this.currentMoney = data.money;
    }

    public void SaveData(ref GameData data)
    {
        data.money = this.currentMoney;
    }
}
