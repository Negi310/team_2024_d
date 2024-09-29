using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBarCS2 : MonoBehaviour
{
    public Slider hpSliderScene2;

    void Start()
    {
        // シングルトンから値を取得してスライダーに設定
        if (SliderValueManager.Instance != null)
        {
            hpSliderScene2.value = SliderValueManager.Instance.sliderValue;
            hpSliderScene2.maxValue = 100f; // 最大HPを設定
        }
        else
        {
            Debug.LogError("SliderValueManagerインスタンスが見つかりません。");
        }

        // スライダーの値が変更されたときに呼ばれるリスナーを設定
        hpSliderScene2.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ChangeHP(-20);
            Debug.Log("敵に当たった");
        }
        else if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            ChangeHP(-5);
            Debug.Log("敵の攻撃に当たった");
        }
        else if (collision.gameObject.CompareTag("StageOut"))
        {
            ChangeHP(-9999);
            Debug.Log("ステージアウト");
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("HPItem"))
        {
            ChangeHP(30);
            Debug.Log("回復した");
        }
    }

    private void ChangeHP(float amount)
    {
        hpSliderScene2.value += amount;

        if (hpSliderScene2.value <= 0)
        {
            hpSliderScene2.value = 0;
            Debug.Log("ゲームオーバー");
            Death();
        }
    }

    private void Death()
    {
        // プレイヤーの死亡処理
        Destroy(gameObject);
        Debug.Log("死亡");
    }

    private void OnSliderValueChanged(float value)
    {
        if (SliderValueManager.Instance != null)
        {
            SliderValueManager.Instance.sliderValue = value;
        }
        else
        {
            Debug.LogError("SliderValueManagerインスタンスが見つかりません。");
        }
    }
}
