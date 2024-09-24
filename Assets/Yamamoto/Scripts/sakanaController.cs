using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private GameObject player; // プレイヤーオブジェクト
    [SerializeField] private float moveSpeed = 5f; // 移動速度
    [SerializeField] private float stoppingDistance = 1.5f; // 停止距離
    [SerializeField] private float triggerDistance = 30f; // 追従を開始する距離

    void Update()
    {
        // プレイヤーとの距離を測定
        float distance = Vector3.Distance(transform.position, player.transform.position);

        // プレイヤーが一定の距離にいる場合、追従する
        if (distance <= triggerDistance)
        {
            FollowPlayer(distance);
        }
    }

    private void FollowPlayer(float distance)
    {
        // プレイヤーとの距離が停止距離より大きい場合のみ移動
        if (distance > stoppingDistance)
        {
            // プレイヤーの方向に移動
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            // プレイヤーを向く
            transform.LookAt(player.transform);
        }
        else
        {
            // Z座標を合わせる処理
            float zDifference = transform.position.z - player.transform.position.z;
            Debug.Log($"Z座標の差: {zDifference}");

            // Z座標を合わせる
            if (Mathf.Abs(zDifference) < 0.5f) // Z座標の差をチェック
            {
                Debug.Log("敵が消滅します");
                Destroy(gameObject); // 敵を消滅
            }
            else
            {
                // Z座標をプレイヤーのZ座標に合わせる
                transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z);
            }
        }
    }
}
