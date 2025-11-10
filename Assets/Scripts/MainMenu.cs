using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
    // Method to load the Bedroom Scene when Start button is clicked
    public void StartGame()
    {
        SceneManager.LoadScene("BedroomScene");
    }

    // Method to load the Settings scene
    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    // Method to quit the game when Quit button is clicked
    public void QuitGame()
    {
        Debug.Log("Quit Game");

        #if UNITY_EDITOR
        // Exit play mode in Unity Editor
        EditorApplication.isPlaying = false;
        #else
        // Quit the application when built
        Application.Quit();
        #endif
    }
}
