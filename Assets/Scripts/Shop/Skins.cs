using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skins : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private ShopView shopView;
    [SerializeField] private GameObject prefab;

    [Header("Settings")]
    [SerializeField] private string title;
    [SerializeField] private List<Sprite> image;
    [Space]
    [SerializeField] int countSkins;

    private void Awake()
    {
        for(int i = 0; i < countSkins; i++)
        {
            GameObject element = this.shopView.Add(this.prefab);
            ShopElements elementMeta = element.GetComponent<ShopElements>();

            elementMeta.SetTitle(this.title + i);
            elementMeta.SetImage(this.image[i]);
        }
    }
}
