using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControllerScript : MonoBehaviour
{
    // Loads the MainMenu Scene
    public void MainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Reloads the current scene
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Loads the TestingScene Scene
    public void TestingScene()
    {
        SceneManager.LoadScene("TestingScene");
    }

    // Loads the TutorialLevel Scene
    public void TutorialLevelScene()
    {
        SceneManager.LoadScene("TutorialLevel");
    }

    // Loads the Level01 Scene
    public void Level01Scene()
    {
        SceneManager.LoadScene("Level01");
    }

    // Closes the game 
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
