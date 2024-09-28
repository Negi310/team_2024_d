using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject Player; // プレイヤーオブジェクト
    [SerializeField] private GameObject GameOverCanvas; // ゲームオーバーキャンバス

    // Startは最初のフレームで呼ばれます
    void Start()
    {
        GameOverCanvas.SetActive(false); // ゲーム開始時にGameOverCanvasを非アクティブにする
    }

    // Updateは毎フレーム呼ばれます
    void Update()
    {
        if (Player == null) // Playerがnullの場合、つまりプレイヤーがいないとき
        {
            HandleGameOver(); // ゲームオーバー処理を呼び出す
        }
    }

    // ゲームオーバー処理
    private void HandleGameOver()
    {
        GameOverCanvas.SetActive(true); // GameOverCanvasをアクティブにする
        Debug.Log("死んだ");
        Time.timeScale = 0; // ゲームを一時停止する
    }

    // ゲームをリスタートするメソッド
    public void GameRestart()
    {
        Time.timeScale = 1; // ゲームを再開する
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 現在のシーンを再読み込み
        Debug.Log("retry");
    }
}
