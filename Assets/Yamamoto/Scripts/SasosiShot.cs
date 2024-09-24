using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    [SerializeField] private GameObject player; // プレイヤーオブジェクト
    [SerializeField] private GameObject ball;   // 発射する弾
    [SerializeField] private float ballSpeed = 10.0f; // 弾の速度
    [SerializeField] private float shootInterval = 1.0f; // 発射間隔
    [SerializeField] private float triggerDistance = 10.0f; // 発射する距離
    private float time; // タイマー

    private void Start()
    {
        time = shootInterval; // 初期化
    }

    void Update()
    {
        // プレイヤーとの距離を測定
        float distance = Vector3.Distance(transform.position, player.transform.position);
        
        // プレイヤーとの距離が指定した距離以内の場合
        if (distance <= triggerDistance)
        {
            transform.LookAt(player.transform); // プレイヤーを向く
            time -= Time.deltaTime; // タイマーを減少

            if (time <= 0)
            {
                BallShot(); // 弾を発射
                time = shootInterval; // タイマーをリセット
            }
        }
    }

    void BallShot()
    {
        // 弾を発射位置を調整
        Vector3 spawnPosition = transform.position + transform.forward; // 前方にオフセット
        GameObject shotObj = Instantiate(ball, spawnPosition, Quaternion.LookRotation(transform.forward));
        shotObj.GetComponent<Rigidbody>().velocity = transform.forward * ballSpeed; // 弾の速度設定
        Debug.Log("sasoriが弾を撃った");
    }
}
