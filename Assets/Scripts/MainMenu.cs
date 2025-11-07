using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Method to load the Bedroom Scene when Start button is clicked
    public void StartGame()
    {
        SceneManager.LoadScene("BedroomScene");
    }

    // Method to quit the game when Quit button is clicked
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
