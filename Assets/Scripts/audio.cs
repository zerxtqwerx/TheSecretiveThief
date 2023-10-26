using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class audio : MonoBehaviour
{
    public Sprite stopSprite;
    public Sprite playSprite;
    bool isPlaying;

    public void Mute(bool muted)
    {
        if (muted)
        {
            AudioListener.volume = 0;
            gameObject.GetComponent<Image>().sprite = playSprite;
        }
        else
        {
            AudioListener.volume = 1;
            gameObject.GetComponent<Image>().sprite = stopSprite;
        }
    }
}
