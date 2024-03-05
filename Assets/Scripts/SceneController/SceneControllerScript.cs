using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControllerScript : MonoBehaviour
{

    public void Level01Scene()
    {
        SceneManager.LoadScene("Level01");
    }

    public void TestingScene()
    {
        SceneManager.LoadScene("TestingScene");
    }
}
