using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSkinBuyed : MonoBehaviour, IDataSave
{
    [SerializeField] bool isSkinBuyed;
    [SerializeField] int index;

    public bool IsBuyed()
    {
        return isSkinBuyed;
    }

    public void BuySkin()
    {
        isSkinBuyed = true;
    }
    public void LoadData(GameData data)
    {
        this.isSkinBuyed = data.skins[index];
    }

    public void SaveData(ref GameData data)
    {
        data.skins[index] = this.isSkinBuyed;
    }
}
