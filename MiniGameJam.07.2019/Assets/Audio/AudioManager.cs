using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    private AudioSource source;

    private void Awake()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    public void PlayAudio (AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

}
