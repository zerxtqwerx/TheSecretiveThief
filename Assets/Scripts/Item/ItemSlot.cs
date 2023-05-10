using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Sprite icon;
    public int index; //

    public void SetItem(Sprite _sprite, int _index)
    {
        icon = _sprite;
        index = _index;
        transform.GetComponent<Image>().sprite = icon;
    }
}
