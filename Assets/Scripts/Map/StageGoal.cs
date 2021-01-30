using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StageGoal : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log(true);
        if (col.gameObject.CompareTag("Player")) {
            SceneManager.LoadScene("GoalScene");
        }
    }
}
