using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListnerLoockScript : MonoBehaviour
{
    void LateUpdate()
    {
        if (Camera.main != null)
            transform.rotation = Camera.main.transform.rotation;
    }
}
