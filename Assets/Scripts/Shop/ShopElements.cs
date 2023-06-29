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

    public float Width() => this.rectTransform.rect.width;
    public float Height() => rectTransform.rect.height;

    public void SetTitle(string title_) => title.text = title_;

    public void SetImage(Sprite image_) => image.sprite = image_;
}
