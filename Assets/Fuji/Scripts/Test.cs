using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private Transform player; // プレイヤーのTransformをアサイン

    [SerializeField] private float steerSpeed = 150f;

    [SerializeField] private float chargeSpeedMultiplier = 2f; // 突進時の速度倍率

    [SerializeField] private float angle;

    [SerializeField] private float frequency = 1f; // 周期の速さ
    [SerializeField] private float amplitude = 1f; // うねりの大きさ

    [SerializeField] private float timeCounter = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player == null)
            return;

        // プレイヤーとの位置差を計算 (yz平面のみ)
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
    
        // x成分を0にして、yz平面上の方向ベクトルを作成
        directionToPlayer.x = 0f;

        // ボスのx軸をプレイヤーに向けるための角度を計算
        angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.z) * Mathf.Rad2Deg;

        float nangle = -1 * angle;

        // 現在のボスの回転を取得し、x軸のみ回転を変更
        Quaternion targetRotation = Quaternion.Euler(nangle, transform.rotation.y, transform.rotation.z);

        // ボスの回転をx軸方向にのみターンさせる
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, steerSpeed * Time.fixedDeltaTime);

        // 突進速度を適用
        float currentMoveSpeed = moveSpeed * chargeSpeedMultiplier;
        transform.position += transform.forward * currentMoveSpeed * Time.fixedDeltaTime;
    }
}
