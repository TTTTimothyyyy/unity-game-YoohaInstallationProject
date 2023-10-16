using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource _audioSource;
    public AudioClip audioClipOne, audioClipTwo, audioClipThree;

    public void PlayButtonClickClip()
    {
        _audioSource.clip = audioClipOne;
        _audioSource.Play();
    }

    public void PlaySwipeClip()
    {
        _audioSource.clip = audioClipTwo;
        _audioSource.Play();
    }

    public void PlayMessageToneClip()
    {
        _audioSource.clip = audioClipThree;
        _audioSource.Play();
    }
}
