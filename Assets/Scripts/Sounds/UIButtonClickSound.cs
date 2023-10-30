using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonClickSound : MonoBehaviour
{
    private Button currentButton;

    private void Awake()
    {
        currentButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        currentButton.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        currentButton.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        SoundManager.PlayUiButtonSound();
    }
}
