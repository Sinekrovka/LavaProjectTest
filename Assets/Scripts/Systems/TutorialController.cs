using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Tutorial"))
        {
            Debug.LogError("!!!");
            StartTutorial();
        }
    }

    private void StartTutorial()
    {
        
    }
}
