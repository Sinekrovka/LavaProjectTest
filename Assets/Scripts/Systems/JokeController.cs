using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokeController : MonoBehaviour
{
    [SerializeField] private GameObject _jokeScreen;
    public static JokeController Instance;
    

    private void Awake()
    {
        Instance = this;
    }

    public void StartJoke()
    {
        SoundsController.Instance.StopAllMusic();
        _jokeScreen.SetActive(true);
    }
}
