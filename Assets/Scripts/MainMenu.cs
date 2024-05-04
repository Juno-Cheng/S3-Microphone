using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject micCheckmark;
    public GameObject micCross;

    void Start()
    {
        // Check if microphone is available
        if (Microphone.devices.Length > 0)
        {
            Debug.Log("Microphone is available.");
            // Enable the mic checkmark UI
            micCheckmark.SetActive(true);
            // Disable the mic cross UI
            micCross.SetActive(false);
        }
        else
        {
            Debug.Log("No microphone detected.");
            // Enable the mic cross UI
            micCross.SetActive(true);
            // Disable the mic checkmark UI
            micCheckmark.SetActive(false);
        }
    }

    public void Play()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
