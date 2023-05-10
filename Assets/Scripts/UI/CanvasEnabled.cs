using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasEnabled : MonoBehaviour
{
    //dont use now

    public static CanvasEnabled instance;

    private Canvas canvas;
    [SerializeField] public bool enabled;

    private void Awake()
    {
        instance = this;
        canvas = GetComponent<Canvas>();
        instance.canvas.gameObject.SetActive(enabled);
    }

    public void Open()
    {
        instance.canvas.gameObject.SetActive(true);
    }

    public void Close()
    {
        instance.canvas.gameObject.SetActive(false);
    }
}
