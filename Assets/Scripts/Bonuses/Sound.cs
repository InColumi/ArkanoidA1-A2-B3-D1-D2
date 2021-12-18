using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] private AudioClip _bonusAppeared;
    private AudioSource _audioSrc;
    
    void Start()
    {
        _audioSrc = GameObject.FindGameObjectWithTag("Audio Sourse").GetComponent<AudioSource>();
        _audioSrc.PlayOneShot(_bonusAppeared, 0.5f);
    }
}
