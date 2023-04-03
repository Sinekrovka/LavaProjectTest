using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneController : MonoBehaviour
{
    private bool _tutorialActive;

    private void Awake()
    {
        _tutorialActive = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SetTutorial()
    {
        _tutorialActive = !_tutorialActive;
        if (_tutorialActive)
        {
            PlayerPrefs.GetInt("Tutorial", 0);
        }
        else
        {
            PlayerPrefs.DeleteKey("Tutorial");
        }
        PlayerPrefs.Save();
    }
}
