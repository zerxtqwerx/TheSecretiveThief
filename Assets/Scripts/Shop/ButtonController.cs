using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public void EditButton(Button button, bool enabled, string sign, string colorName)
    {
        ChangeEnabled(button, enabled);
        ChangeSign(button, sign);
        ChangeColor(button, colorName);
    }

    private void ChangeColor(Button button, string colorName)
    {
        if (colorName == "Blue")
        {
            button.GetComponent<Image>().color = new Color(0, 0, 0);
        }
        else if (colorName == "Red")
        {
            button.GetComponent<Image>().color = new Color(1, 1, 1);
        }
    }

    private void ChangeEnabled(Button button, bool enabled)
    {
        button.enabled = enabled;
    }

    private void ChangeSign(Button button, string sign)
    {
        button.GetComponentInChildren<Text>().text = sign;
    }
}
