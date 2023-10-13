using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSkinBuyed : MonoBehaviour
{
    [SerializeField] bool isSkinBuyed;

    public bool IsBuyed()
    {
        return isSkinBuyed;
    }

    public void BuySkin()
    {
        isSkinBuyed = true;
    }
}
