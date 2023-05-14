using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public Text amountOfMoney;
    private int currentMoney;
    
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
}
