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
        _jokeScreen.SetActive(true);
    }
}
