using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{
    public Slider hpSliderScene1;

    // Start is called before the first frame update
    void Start()
    {
         // シングルトンから値を取得してスライダーに設定
        hpSliderScene1.value = SliderValueManager.Instance.sliderValue;

        // スライダーの値が変更されたときに呼ばれるリスナーを設定
        hpSliderScene1.onValueChanged.AddListener(OnSliderValueChanged);
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
            hpSliderScene1.value -= 20;
            Debug.Log("敵に当たった");
        }
        if (collision.gameObject.tag == "EnemyBullet")
        {
            hpSliderScene1.value -= 5;
            Debug.Log("敵の攻撃に当たった");
        }
        if (collision.gameObject.tag == "StageOut")
        {
            hpSliderScene1.value -= 9999;
            Debug.Log("ステージアウト");
        }
        if (hpSliderScene1.value <= 0)
         {
            hpSliderScene1.value = 0;
            Debug.Log("ゲームオーバー");
            Death();
         }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "HPItem")
        {
            hpSliderScene1.value += 30;
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
