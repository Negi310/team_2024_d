using UnityEngine;

public class SliderValueManager : MonoBehaviour
{
    public static SliderValueManager Instance { get; private set; }

    // スライダーの値を保持する変数
    public float sliderValue;

    private void Awake()
    {
        // インスタンスが存在しない場合、新しく作成
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンを跨いでも消えないようにする
        }
        else
        {
            Destroy(gameObject); // 既存のインスタンスがあれば削除
        }
    }
}
