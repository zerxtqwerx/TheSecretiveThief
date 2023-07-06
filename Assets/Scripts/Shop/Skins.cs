using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skins : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private ShopView shopView;
    [SerializeField] private GameObject prefab;

    [Header("Settings")]
    [SerializeField] private List<Sprite> image;
    [SerializeField] private List<string> title;
    [SerializeField] private List<string> description;
    [Space]
    [SerializeField] int countSkins;
    [SerializeField] string characterLink;

    private void Awake()
    {
        for(int i = 0; i < countSkins; i++)
        {
            GameObject element = this.shopView.Add(this.prefab);
            ShopElements elementMeta = element.GetComponent<ShopElements>();

            elementMeta.SetImage(this.image[i]);
            elementMeta.SetTitle(this.title[i]);
            elementMeta.SetDescription(this.description[i]);
            elementMeta.SetSkinNumber(i);
            elementMeta.SetCharacterLink(this.characterLink);
            elementMeta.ActivatingButton();
        }
    }
}
