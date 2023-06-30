using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopElements : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private RectTransform rectTransform;
    [Space]
    [SerializeField] private Image image;
    [SerializeField] private Text title;
    [SerializeField] private Text description;
    private int skinNumber;
    private string characterLink;

    public float Width() => this.rectTransform.rect.width;
    public float Height() => rectTransform.rect.height;

    public void SetImage(Sprite image_) => image.sprite = image_;
    public void SetTitle(string title_) => title.text = title_;
    public void SetDescription(string description_) => description.text = description_;
    public void SetSkinNumber(int skinNumber_) => skinNumber = skinNumber_;
    public void SetCharacterLink(string characterLink_) => characterLink = characterLink_;

    public void BuySkin()
    {
        GameObject character = GameObject.FindWithTag(characterLink);

        for (int i = 0; i < character.transform.childCount; i++)
        {
            if (character.transform.GetChild(i).gameObject.activeSelf == true)
            {
                GameObject currentSkin = character.transform.GetChild(i).gameObject;
                currentSkin.SetActive(false);
            }
            if(i == skinNumber)
            {
                GameObject skin = character.transform.GetChild(skinNumber).gameObject;
                skin.SetActive(true);
            }
        }
    }
}
