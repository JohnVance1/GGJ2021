using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitching : MonoBehaviour
{
    public void GameScene()
    {
        Debug.Log("switching scene");
        //Scene should be whatever the game is played in
        //SceneManager.LoadScene("GoalScene");
    }

    public void ExitGame()
    {
        Debug.Log("exit game");
        //Application.Quit();
    }
}
