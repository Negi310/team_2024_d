using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestart : MonoBehaviour // クラス名を変更
{
    private bool firstPush = false;
    private const string sceneName = "CourseScene"; // シーン名を定数として定義

    // Startが押されると動作する
    public void Restart()
    {
        Debug.Log("Press ReStart");
        if (!firstPush)
        {
            SceneManager.LoadScene(sceneName);
            Debug.Log("Next Scene");
            firstPush = true;
        }
    }

    // 必要に応じてフラグをリセットするメソッド
    public void ResetFirstPush()
    {
        firstPush = false;
    }
}
