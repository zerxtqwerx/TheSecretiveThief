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
    [SerializeField] private Button button;
    private int skinNumber;
    private string characterLink;
    private GameObject character;
    private Money money;

    public float Width() => this.rectTransform.rect.width;
    public float Height() => rectTransform.rect.height;

    public void SetImage(Sprite image_) => image.sprite = image_;
    public void SetTitle(string title_) => title.text = title_;
    public void SetDescription(string description_)
    {
        description.text = description_;
    }
    public void SetSkinNumber(int skinNumber_) => skinNumber = skinNumber_;
    public void SetCharacterLink(string characterLink_) => characterLink = characterLink_;

    public void ActivatingButton()
    {
        int price = 0;
        int.TryParse(description.text, out price);

        character = GameObject.FindWithTag(characterLink);
        character.transform.GetChild(skinNumber).GetComponent<IsSkinBuyed>().IsBuyed();
        GameObject moneyObject = GameObject.FindWithTag("money");
        money = moneyObject.GetComponent<Money>();

        if (!character.transform.GetChild(skinNumber).GetComponent<IsSkinBuyed>().IsBuyed())
        {
            ChangeColorButton("red");
            if (!money.PurchasingPermission(price)) 
            {
                InsufficientFundsButton();
            }
        }
        else
        {
            ChangeColorButton("blue");
            if (character.transform.GetChild(skinNumber).gameObject.activeSelf == true)
            {
                CurrentSkinButton();
            }
            else
            {
                ApplySkinButton();
            }
        }
    }

    public void BuySkin()
    {
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
                skin.GetComponent<IsSkinBuyed>().BuySkin();
                skin.SetActive(true);
                ActivatingButton();
            }
        }
    }
    
    private void ApplySkinButton()
    {
        button.GetComponentInChildren<Text>().text = "Apply skin";
        button.enabled = true;
    }

    private void InsufficientFundsButton()
    {
        button.enabled = false;
        ChangeColorButton("blue");
        button.GetComponentInChildren<Text>().text = "Insufficient funds";
    }

    private void CurrentSkinButton()
    {
        button.GetComponentInChildren<Text>().text = "Current skin";
        button.enabled = false;
    }

    private void ChangeColorButton(string nameColor)
    {
        Image image = button.GetComponent<Image>();

        if(nameColor == "blue")
        {
            image.color = new Color(0.25f, 0.16f, 0.147f);
        }
        else if(nameColor == "red")
        {
            image.color = new Color(0.157f, 0.5f, 0.67f);
        }
        
    }

    //сделать НОРМАЛЬНЫЙ КОД переделать цвета, считывать деньги
}
