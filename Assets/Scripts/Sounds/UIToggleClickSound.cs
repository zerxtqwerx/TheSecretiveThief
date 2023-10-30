using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class UIToggleClickSound : MonoBehaviour
{
    private Toggle currentButton;

    private void Awake()
    {
        currentButton = GetComponent<Toggle>();
    }

    private void OnEnable()
    {
        currentButton.onValueChanged.AddListener(OnClick);
    }

    private void OnDisable()
    {
        currentButton.onValueChanged.RemoveListener(OnClick);
    }

    private void OnClick(bool b)
    {
        SoundManager.PlayUiButtonSound();
    }
}
