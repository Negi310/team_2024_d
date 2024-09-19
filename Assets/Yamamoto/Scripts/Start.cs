using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour // クラス名を変更
{
    private bool firstPush = false;

    // Startが押されると動作する
    public void PressStart()
    {
        Debug.Log("Press Start");
        if (!firstPush)
        {
            SceneManager.LoadScene("CourseScene");
            Debug.Log("Next Scene");
            firstPush = true;
        }
    }
}
