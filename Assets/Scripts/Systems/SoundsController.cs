using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsController : MonoBehaviour
{
    private AudioSource _audio;
    [SerializeField] private AudioSource _musicBackground;
    public static SoundsController Instance;
    private void Awake()
    {
        Instance = this;
        _audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        SpawnSystem.Instance.spawnItemAction += PlaySound;
        SpawnSystem.Instance.up += PlaySound;
        SpawnSystem.Instance.remove += PlaySound;
    }

    private void PlaySound()
    {
        _audio.Play();
    }

    public void StopAllMusic()
    {
        _musicBackground.gameObject.SetActive(false);
        _audio.gameObject.SetActive(false);
        
    }
}
