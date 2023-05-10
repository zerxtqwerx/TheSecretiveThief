using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ItemPointer : MonoBehaviour
{


    private void Start()
    {
        PointerManager.Instance.AddToList(this);
    }

    public void Destroy()
    {
        PointerManager.Instance.RemoveFromList(this);
        Destroy(gameObject);
    }

}
