using System.Collections;
using UnityEngine;

public class BounceOnApproach : MonoBehaviour
{
    public float triggerDistance = 30f; // 跳ねるトリガー距離
    public float bounceForceY = 8f;      // Y軸方向の跳ねる力
    public float bounceForceZ = -4f;     // Z軸方向の跳ねる力
    public float actionInterval = 1f;     // アクション間隔
    [SerializeField] private GameObject player; // プレイヤーオブジェクト
    private Rigidbody rb;                 // Rigidbodyコンポーネント
    private bool canBounce = true;        // 跳ねるフラグ

    private void Start()
    {
        // Rigidbodyを取得
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogWarning("Rigidbodyが取得できていません。");
        }

        if (player != null)
        {
            Debug.Log("プレイヤーオブジェクトが設定されています。");
        }
        else
        {
            Debug.LogWarning("プレイヤーオブジェクトが設定されていません。");
        }
    }

    private void Update()
    {
        if (player != null)
        {
            // プレイヤーとの距離を測定
            float distance = Vector3.Distance(transform.position, player.transform.position);
            Debug.Log($"Distance to player: {distance}, Can bounce: {canBounce}");

            if (distance <= triggerDistance && canBounce)
            {
                Debug.Log("跳ねる");
                StartCoroutine(Bounce());
            }
        }
    }

    private IEnumerator Bounce()
    {
        // 上方向と前方向に力を加える
        if (rb != null)
        {
            Vector3 force = new Vector3(0, bounceForceY, bounceForceZ);
            rb.AddForce(force, ForceMode.Impulse);
        }

        // アクション後のクールダウン処理
        canBounce = false; // 跳ねるのを無効化
        yield return new WaitForSeconds(actionInterval); // インターバル待機
        canBounce = true; // 再度跳ねるのを有効化
    }
}
