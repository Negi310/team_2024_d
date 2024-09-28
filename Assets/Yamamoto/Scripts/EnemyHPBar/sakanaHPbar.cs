using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sakanaHPbar : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;

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
        if (collision.gameObject.tag == "Bullet")
        {
            hpSlider.value -= 5;
            Debug.Log("通常弾が敵に当たった");
        }
        if (collision.gameObject.tag == "BulletSP(0)")
        {
            hpSlider.value -= 25;
            Debug.Log("効果的な特殊弾が敵に当たった");
        }
        if (collision.gameObject.tag == "BulletSP(1)")
        {
            hpSlider.value -= 3;
            Debug.Log("いまいちな特殊弾が敵に当たった");
        }
        if (collision.gameObject.tag == "Player")
        {
            hpSlider.value -= 15;
            Debug.Log("プレイヤーに当たった");
        }
        if (hpSlider.value <= 0)
         {
            hpSlider.value = 0;
            Debug.Log("敵が撃破された");
            Death();
         }
    }
     private void Death()
    {
        // ゲームオブジェクトを削除する処理
        Destroy(gameObject);
        Debug.Log("敵撃破");
    }
}
