using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingSkins : MonoBehaviour
{
    [SerializeField] string characterLink;
    [SerializeField] List<int> buyingSkins;
    GameObject character;

    public void Start()
    {
        character = GameObject.FindWithTag(characterLink);

    }

    public void BuySkin(int skinNumber)
    {
        buyingSkins.Add(skinNumber);
    }

    public bool SkinPurchased(int skinNumber)
    {
        return buyingSkins.Contains(skinNumber);
    }
}
