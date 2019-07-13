using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    [SerializeField]
    private AudioClip musicLoop = null;
    [SerializeField]
    private AudioSource musicSource = null;

    [SerializeField]
    private AudioSource voiceOverSource = null;

    private AudioSource source;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(instance);

        source = gameObject.GetComponent<AudioSource>();

        //play the music loop for the level
        musicSource.clip = musicLoop;
        musicSource.Play();
    }

    public void PlayAudio (AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }

    public void UpdateVoiceOver (bool play, AudioClip clip = null)
    {
        if (!play)
            voiceOverSource.Stop();
        else
        {
            voiceOverSource.clip = clip;
            voiceOverSource.Play();
        }
    }

}
