using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCheck : MonoBehaviour
{
    public float triggerDistanceSasori = 30f; // 動作する距離
    public float cooldownTimeSasori = 2f;      // クールダウン時間
    public float actionIntervalSasori = 1f;    // アクション間隔
    private GameObject player;
    private Rigidbody rb;
    private bool canPerformAction = true; // 動作が可能かどうかのフラグ

    private void Start()
    {
        // プレイヤーオブジェクトを取得
        player = GameObject.FindGameObjectWithTag("Player");

        // Rigidbodyを取得
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (player != null)
        {
            // プレイヤーとの距離を測定
            float distance = Vector3.Distance(transform.position, player.transform.position);

            // 一定の距離に近づいた場合の処理
            if (distance <= triggerDistanceSasori && canPerformAction)
            {
                StartCoroutine(PerformAction()); // アクションをコルーチンで実行
            }
        }
    }

    private IEnumerator PerformAction()
    {
        // プレイヤーが近づいたときの動作を定義
        Debug.Log("跳ねる");

        // 上方向 (Y軸) と前方向 (Z軸) に力を加える
        if (rb != null)
        {
            Vector3 force = new Vector3(0, 8, -4); // 上方向と前方向
            rb.AddForce(force, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("Rigidbodyが取得できていません。");
        }

        // アクション後のクールダウン処理
        canPerformAction = false; // 動作を無効化
        yield return new WaitForSeconds(actionIntervalSasori); // インターバル待機
        canPerformAction = true; // 動作を再度有効化
    }
}
