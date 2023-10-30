using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AnimationSoundPlay : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> audioClips = new List<AudioClip>();
    [SerializeField]
    private bool randomize = false;
    private int index = 0;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (randomize && audioClips.Count > 0)
            index = Random.Range(0, audioClips.Count);
    }
    public void PlaySound()
    {
        if (audioClips.Count > 0)
        {
            audioSource.clip = audioClips[index++];
            if (randomize)
            {
                index = Random.Range(0, audioClips.Count);
            }
            else
            {
                if (index >= audioClips.Count)
                    index = 0;
            }
        }
        audioSource.Play();
    }
}
