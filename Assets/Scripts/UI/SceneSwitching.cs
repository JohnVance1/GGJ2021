using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitching : MonoBehaviour
{
    public void GameScene()
    {
        Debug.Log("MapFinal");
        //Scene should be whatever the game is played in
        SceneManager.LoadScene("MapFinal");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
