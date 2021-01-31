using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitching : MonoBehaviour
{
    public void GameScene()
    {
        Debug.Log("mapData");
        //Scene should be whatever the game is played in
        SceneManager.LoadScene("mapData");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
