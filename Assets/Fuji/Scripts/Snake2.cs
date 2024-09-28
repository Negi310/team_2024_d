using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SnakeState
{
    Idle,
    Charging,
    Recovering
}

public class Snake2 : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float steerSpeed = 150f;
    public float bodySpeed = 10f;
    public int gap = 100;
    public int bodyLength = 4;

    public GameObject snakeBody0;
    public GameObject snakeBody;
    public GameObject snakeBody2;
    public GameObject snakeBody3;

    public List<GameObject> bodyParts = new List<GameObject>();
    public List<Vector3> bodyLogs = new List<Vector3>();

    private float timeCounter = 0f;

    [SerializeField] private float frequency = 1f; // 周期の速さ
    [SerializeField] private float amplitude = 1f; // うねりの大きさ
    [SerializeField] private float preparation;

    // ステート管理
    private SnakeState currentState = SnakeState.Idle;
    private float stateTimer = 0f;

    [SerializeField] private float chargeDuration = 2f;    // 突進の持続時間
    [SerializeField] private float recoverDuration = 1.5f; // 回復期間の持続時間
    [SerializeField] private float chargeSpeedMultiplier = 2f; // 突進時の速度倍率

    public Transform player; // プレイヤーのTransformをアサイン

    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        GrowSnake0();
        for (int i = 0; i < bodyLength; i++)
        {
            GrowSnake();
            GrowSnake2();
            GrowSnake();
            GrowSnake2();
            GrowSnake();
            GrowSnake3();
        }
    }

    void FixedUpdate()
    {
        stateTimer += Time.fixedDeltaTime;

        Transform thisTransform = this.transform;

        Vector3 posLock = thisTransform.position;

        posLock.x = 0f;

        thisTransform.position = posLock;

        switch (currentState)
        {
            case SnakeState.Idle:
                // 一定時間後に突進状態へ移行
                if (stateTimer >= recoverDuration)
                {
                    ChangeState(SnakeState.Charging);
                }
                break;

            case SnakeState.Charging:
                // 突進中の動作
                ChargeTowardsPlayer();
                if (stateTimer >= chargeDuration)
                {
                    ChangeState(SnakeState.Recovering);
                }
                break;

            case SnakeState.Recovering:
                // 回復中の動作（例えば、うねりながら前進）
                RecoverFromCharge();
                if (stateTimer >= recoverDuration)
                {
                    ChangeState(SnakeState.Charging);
                }
                break;
        }

        // Body movementの更新
        UpdateBodyParts();
    }

    private void ChangeState(SnakeState newState)
    {
        currentState = newState;
        stateTimer = 0f;

        switch (newState)
        {
            case SnakeState.Idle:
                // Idle状態の初期化（必要なら）
                break;

            case SnakeState.Charging:
                // 突進時の初期設定（必要なら）
                break;

            case SnakeState.Recovering:
                // 回復時の初期設定（必要なら）
                break;
        }
    }

    private void ChargeTowardsPlayer()
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

    private void RecoverFromCharge()
    {
        // 通常のうねり動作を維持

        // 通常の移動速度で前進
        transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;
    }

    private void UpdateBodyParts()
    {
        bodyLogs.Insert(0, transform.position);
        int index = 0;
        foreach (var body in bodyParts)
        {
            if (bodyLogs.Count > index * gap)
            {
                Vector3 point = bodyLogs[Mathf.Min(index * gap, bodyLogs.Count - 1)];
                Vector3 moveDirection = point - body.transform.position;
                body.transform.position += moveDirection * bodySpeed * Time.fixedDeltaTime;
                body.transform.LookAt(point);
                index++;
            }
        }
        if (bodyLogs.Count > bodyParts.Count * gap)
        {
            bodyLogs.RemoveAt(bodyLogs.Count - 1);
        }
    }

    private void GrowSnake0()
    {
        GameObject body = Instantiate(snakeBody0);
        bodyParts.Add(body);
    }

    private void GrowSnake()
    {
        GameObject body = Instantiate(snakeBody);
        bodyParts.Add(body);
    }

    private void GrowSnake2()
    {
        GameObject body = Instantiate(snakeBody2);
        bodyParts.Add(body);
    }

    private void GrowSnake3()
    {
        GameObject body = Instantiate(snakeBody3);
        bodyParts.Add(body);
    }
}
