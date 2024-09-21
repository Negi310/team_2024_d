using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownFunction : MonoBehaviour
{   
    // 振動の振幅 (上下に移動する範囲)
    [SerializeField] private float amplitude = 1.0f;

    // 振動の速さ
    [SerializeField] private float frequency = 1.0f;

    // 初期位置
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        // オブジェクトの初期位置を保存
        startPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        // y軸方向に単振動を計算
        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;

        // 新しい位置を設定
        transform.position = new Vector3(startPos.x, newY, startPos.z);

    }
}
