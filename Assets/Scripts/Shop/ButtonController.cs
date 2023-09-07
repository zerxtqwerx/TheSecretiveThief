using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    //private Button button;

    public void EditButton(ref Button button, bool enabled, string sign, string colorName)
    {
        ChangeEnabled(ref button, enabled);
        ChangeSign(ref button, sign);
        ChangeColor(ref button, colorName);
    }

    private void ChangeColor(ref Button button, string colorName)
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

    private void ChangeEnabled(ref Button button, bool enabled)
    {
        button.enabled = enabled;
    }

    private void ChangeSign(ref Button button, string sign)
    {
        button.GetComponent<Text>().text = sign;
    }
}
