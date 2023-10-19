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
    ButtonController bc;
    int price;
    ISwitchSkin iss;
    GameObject currentSkin;
    GameObject skin;

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

    void Update()
    {
        if (!character.transform.GetChild(skinNumber).GetComponent<IsSkinBuyed>().IsBuyed())
        {
            if (!money.PurchasingPermission(price))
            {
                bc.EditButton(button, false, "Inficient funds", "red");
            }
            else
            {
                bc.EditButton(button, true, "Buy", "blue");
            }
        }
        else
        {
            if (character.transform.GetChild(skinNumber).gameObject.activeSelf == true)
            {
                bc.EditButton(button, false, "Current skin", "blue");
            }
            else
            {
                bc.EditButton(button, true, "Apply skin", "blue");
            }
        }
    }

    public void ActivatingButton()
    {
        bc = new ButtonController();
        iss = new ISwitchSkin();

        character = GameObject.FindWithTag(characterLink);
        character.transform.GetChild(skinNumber).GetComponent<IsSkinBuyed>().IsBuyed();
        GameObject moneyObject = GameObject.FindWithTag("money");
        money = moneyObject.GetComponent<Money>();

        price = 0;
        int.TryParse(description.text, out price);

        if (!character.transform.GetChild(skinNumber).GetComponent<IsSkinBuyed>().IsBuyed())
        {
            if (!money.PurchasingPermission(price)) 
            {
                bc.EditButton(button, false, "Inficient funds", "red");
            }
            else
            {
                bc.EditButton(button, true, "Buy", "blue");
            }
        }
        else
        {
            if (character.transform.GetChild(skinNumber).gameObject.activeSelf == true)
            {
                bc.EditButton(button, false, "Current skin", "blue");
            }
            else
            {
                bc.EditButton(button, true, "Apply skin", "blue");
            }
        }
    }

    public void BuySkin()
    {
        for (int i = 0; i < character.transform.childCount; i++)
        {
            if (character.transform.GetChild(i).gameObject.activeSelf == true)
            {
                currentSkin = character.transform.GetChild(i).gameObject;
                currentSkin.SetActive(false);
                break;
            }
        }
        for (int i = 0; i < character.transform.childCount; i++)
        {
            if (i == skinNumber)
            {
                skin = character.transform.GetChild(skinNumber).gameObject;
                if (!skin.GetComponent<IsSkinBuyed>().IsBuyed())
                {
                    skin.GetComponent<IsSkinBuyed>().BuySkin();
                    money.ChangeAmountOfMoney(-price);
                }
                skin.SetActive(true);
                break;
            }
        }
    }
}