using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBarCS2 : MonoBehaviour
{
    public Slider hpSliderScene2;

    // Start is called before the first frame update
    void Start()
    {
         // シングルトンから値を取得してスライダーに設定
        hpSliderScene2.value = SliderValueManager.Instance.sliderValue;

        // スライダーの値が変更されたときに呼ばれるリスナーを設定
        hpSliderScene2.onValueChanged.AddListener(OnSliderValueChanged);
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
            hpSliderScene2.value -= 20;
            Debug.Log("敵に当たった");
        }
        if (collision.gameObject.tag == "EnemyBullet")
        {
            hpSliderScene2.value -= 5;
            Debug.Log("敵の攻撃に当たった");
        }
        if (collision.gameObject.tag == "StageOut")
        {
            hpSliderScene2.value -= 9999;
            Debug.Log("ステージアウト");
        }
        if (hpSliderScene2.value <= 0)
         {
            hpSliderScene2.value = 0;
            Debug.Log("ゲームオーバー");
            Death();
         }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "HPItem")
        {
            hpSliderScene2.value += 30;
            Debug.Log("回復した");
        }
    }
     private void Death()
    {
        // ゲームオブジェクトを削除する処理
        Destroy(gameObject);
        Debug.Log("死亡");
    }

    private void OnSliderValueChanged(float value)
    {
        // シングルトンに新しい値を保存
        SliderValueManager.Instance.sliderValue = value;
    }
}
