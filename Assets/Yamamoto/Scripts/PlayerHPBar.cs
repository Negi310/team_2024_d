using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{
    public Slider hpSlider;

    // Start is called before the first frame update
    void Start()
    {
        hpSlider.value = hpSlider.maxValue; // 初期HPをスライダーの最大値に設定
    }

    // Update is called once per frame
    void Update()
    {
        // 必要な場合はここに処理を追加
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            hpSlider.value -= 20;
            Debug.Log("敵に当たった");
        }
        if (collision.gameObject.tag == "Enemy's Bullet")
        {
            hpSlider.value -= 5;
            Debug.Log("敵の攻撃に当たった");
        }
        if (collision.gameObject.tag == "StageOut")
        {
            hpSlider.value -= 9999;
            Debug.Log("ステージアウト");
        }
        if (hpSlider.value <= 0)
         {
            hpSlider.value = 0;
            Debug.Log("ゲームオーバー");
            Death();
         }
    }
     private void Death()
    {
        // ゲームオブジェクトを削除する処理
        Destroy(gameObject);
        Debug.Log("死亡");
    }
}
