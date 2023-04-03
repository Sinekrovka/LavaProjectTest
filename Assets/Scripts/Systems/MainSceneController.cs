using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneController : MonoBehaviour
{
    private bool _tutorialActive;

    private void Start()
    {
        _tutorialActive = true;
        PlayerPrefs.SetInt("Tutorial", 0);
        PlayerPrefs.Save();
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
            PlayerPrefs.SetInt("Tutorial", 0);
        }
        else
        {
            PlayerPrefs.DeleteKey("Tutorial");
        }
        PlayerPrefs.Save();
    }
}
