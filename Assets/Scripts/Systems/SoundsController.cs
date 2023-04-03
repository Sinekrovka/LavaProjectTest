using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsController : MonoBehaviour
{

    private AudioSource _audio;
    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        SpawnSystem.Instance.spawnItem += PlaySound;
        SpawnSystem.Instance.up += PlaySound;
        SpawnSystem.Instance.remove += PlaySound;
    }

    private void PlaySound()
    {
        _audio.Play();
    }
}
