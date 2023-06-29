using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public string skinsTag;
    private GameObject handle;
    private GameObject[] skins;
    private Button[] buttons;
    [SerializeField] GameObject buttonPrefab;

    void Start()
    {
        GetSkins();
        CreateButtons();
    }

    private void GetSkins()
    {
        skins = GameObject.FindGameObjectsWithTag(skinsTag);

        if(skins == null)
        {
            Debug.Log("skins do not found");
        }
    }

    private Vector3 AssignPosition()
    {
        if (buttons != null)
        {
            return new Vector3(100 + buttons[-1].transform.position.x, buttons[-1].transform.position.y, buttons[-1].transform.position.z); 
        }
        else
        {
            return new Vector3(0, 0, 0);
        }
    }

    private void CreateButtons()
    {
        int n = skins.Length;
        for (int i = 0; i < n; i++)
        {
            Vector3 pos = new Vector3();
            pos = AssignPosition();
            GameObject button = Instantiate(buttonPrefab, pos, Quaternion.identity);
        }
    }

}
