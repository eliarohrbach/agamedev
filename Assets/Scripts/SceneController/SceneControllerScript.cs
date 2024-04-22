using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControllerScript : MonoBehaviour
{
    public void MainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TestingScene()
    {
        SceneManager.LoadScene("TestingScene");
    }

    public void TutorialLevelScene()
    {
        SceneManager.LoadScene("TutorialLevel");
    }

    public void Level01Scene()
    {
        SceneManager.LoadScene("Level01");
    }


    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
