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

    // Loads the Supercold_Level-01 Scene
    public void Level01Scene()
    {
        SceneManager.LoadScene("Supercold_Level-01");
    }

    // Loads the Supercold_Level-02 Scene
    public void Level02Scene()
    {
        SceneManager.LoadScene("Supercold_Level-02");
    }

    // Closes the game 
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
