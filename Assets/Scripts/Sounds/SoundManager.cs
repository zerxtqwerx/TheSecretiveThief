using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance => _instance;

    [SerializeField]
    private float volume = 1;
    [SerializeField]
    private AudioSource UiButtonSound;
    [SerializeField]
    private AudioSource PickUpSound;

    public static float Volume { get { return _instance.volume; } set { _instance.volume = Volume; OnVolumeUpdated(); } }

    public static Action<float> OnVolumeUpdatedEvent;
    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
            Destroy(gameObject);
        }
    }

    private static void OnVolumeUpdated()
    {
        _instance.UiButtonSound.volume = Volume;
        _instance.PickUpSound.volume = Volume;
        OnVolumeUpdatedEvent?.Invoke(Volume);
    }

    public static void PlayUiButtonSound()
    {
        _instance.UiButtonSound.Play();
    }

    public static void PlayPickUpSound()
    {
        _instance.PickUpSound.Play();
    }
}
