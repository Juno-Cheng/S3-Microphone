using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene management


public class EndScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void ReloadLevel()
    {
        // SceneManager loads the active scene again
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Function to load the main menu scene
    public void LoadMenuScene()
    {
        // Replace "MenuScene" with the exact name of your menu scene
        SceneManager.LoadScene("Menu");
    }
}
