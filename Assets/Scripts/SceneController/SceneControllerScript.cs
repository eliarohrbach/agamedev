using UnityEngine;
using UnityEngine.SceneManagement;

// written by Severin Landolt

/// <summary>
/// Class <c>SceneControllerScript</c> contains all methods for loading different scenes
/// </summary>
public class SceneControllerScript : MonoBehaviour
{
    /// <summary>
    /// Method <c>MainMenuScene</c> loads the MainMenu Scene
    /// </summary>
    public void MainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Method <c>Restart</c> reloads the current scene
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Method <c>TestingScene</c> loads the TestingScene Scene
    /// Is only used in the editor
    /// </summary>
    public void TestingScene()
    {
        SceneManager.LoadScene("TestingScene");
    }

    /// <summary>
    /// Method <c>TutorialLevelScene</c> loads the TutorialLevel Scene
    /// </summary>
    public void TutorialLevelScene()
    {
        SceneManager.LoadScene("TutorialLevel");
    }

    /// <summary>
    /// Method <c>Level01Scene</c> loads the Supercold_Level-01 Scene
    /// </summary>
    public void Level01Scene()
    {
        SceneManager.LoadScene("Supercold_Level-01");
    }

    /// <summary>
    /// Method <c>Level02Scene</c> loads the Supercold_Level-02 Scene
    /// </summary>
    public void Level02Scene()
    {
        SceneManager.LoadScene("Supercold_Level-02");
    }

    /// <summary>
    /// Method <c>QuitGame</c> closes the game 
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
