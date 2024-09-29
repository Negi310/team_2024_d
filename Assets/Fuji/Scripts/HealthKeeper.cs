using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKeeper : MonoBehaviour
{
    public static HealthKeeper Instance;

    public PlayerMovement playerMovement;

    public GameManager gameManager;

    public float healthKeep;
    public GameObject playerObj;

    // Start is called before the first frame update
    void Awake()
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
    void Start()
    {
        
    }
}
