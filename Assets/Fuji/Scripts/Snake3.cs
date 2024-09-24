using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SnakeState2
{
    StateA,
    StateB,
    StateC,
    StateD,
    StateE,
    StateF,
    StateG,
    StateH,
    StateI

}

public class Snake3 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private float rushSpeed = 10f;

    [SerializeField] private float decelSpeed = 5f;
    [SerializeField] private float steerSpeed = 150f;

    [SerializeField] private float spinSpeed;
    [SerializeField] private float bodySpeed = 10f;
    [SerializeField] private int gap = 100;
    [SerializeField] private int bodyLength = 4;

    [SerializeField] private float returnSpotZ;

    [SerializeField] private float returnSpotY;

    [SerializeField] private float prepareCount;

    [SerializeField] private float prepareInterval;

    [SerializeField] private float prepareSpotY;

    [SerializeField] private float prepareSpotZ;

    [SerializeField] private bool prepareFlag = false;

    [SerializeField] private GameObject snakeBody0;
    [SerializeField] private GameObject snakeBody;
    [SerializeField] private GameObject snakeBody2;
    [SerializeField] private GameObject snakeBody3;

    [SerializeField] private List<GameObject> bodyParts = new List<GameObject>();
    [SerializeField] private List<Vector3> bodyLogs = new List<Vector3>();

    [SerializeField] private float timeCounter = 0f;

    [SerializeField] private float frequency = 1f; // 周期の速さ
    [SerializeField] private float amplitude = 1f; // うねりの大きさ
    [SerializeField] private bool returnFlag = false;

    // ステート管理
    [SerializeField] private SnakeState2 currentState = SnakeState2.StateA;
    [SerializeField] private float stateTimer = 0f;

    [SerializeField] private float durationA = 2f;    // 突進の持続時間
    [SerializeField] private float durationB = 1.5f; // 回復期間の持続時間
    [SerializeField] private float chargeSpeedMultiplier = 2f; // 突進時の速度倍率

    [SerializeField] private Transform player; // プレイヤーのTransformをアサイン

    [SerializeField] private float angle;

    [SerializeField] private float fireInterval = 5f;

    [SerializeField] private float fireCount;

    [SerializeField] private Transform enemyFirePosition;

    [SerializeField] private GameObject enemyBullet;


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

        switch (currentState)
        {
            case SnakeState2.StateA:
                // 一定時間後に突進状態へ移行
                if (stateTimer >= durationA)
                {
                    ChangeState(SnakeState2.StateB);
                }
                break;

            case SnakeState2.StateB:
                // 突進中の動作
                Return();
                if (returnFlag)
                {
                    returnFlag = false;
                    ChangeState(SnakeState2.StateC);
                }
                break;

            case SnakeState2.StateC:
                // 回復中の動作（例えば、うねりながら前進）
                Chase();
                if (stateTimer >= durationB)
                {
                    ChangeState(SnakeState2.StateD);
                }
                break;

            case SnakeState2.StateD:
                // 回復中の動作（例えば、うねりながら前進）
                Decel();
                if (stateTimer >= durationA)
                {
                    ChangeState(SnakeState2.StateE);
                }
                break;
            
            case SnakeState2.StateE:
                // 回復中の動作（例えば、うねりながら前進）
                Rush();
                if (stateTimer >= durationA)
                {
                    ChangeState(SnakeState2.StateF);
                }
                break;
            
            case SnakeState2.StateF:
                // 突進中の動作
                Return();
                if (returnFlag)
                {
                    returnFlag = false;
                    ChangeState(SnakeState2.StateG);
                }
                break;

            case SnakeState2.StateG:
                // 突進中の動作
                Chase();
                if (stateTimer >= durationB)
                {
                    ChangeState(SnakeState2.StateH);
                }
                break;

            case SnakeState2.StateH:
                // 突進中の動作
                Prepare();
                if (prepareFlag)
                {
                    prepareFlag = false;
                    ChangeState(SnakeState2.StateI);
                }
                break;

            case SnakeState2.StateI:
                // 回復中の動作（例えば、うねりながら前進）
                Rush();
                if (stateTimer >= durationA)
                {
                    ChangeState(SnakeState2.StateB);
                }
                break; 
        }

        // Body movementの更新
        UpdateBodyParts();
    }

    private void ChangeState(SnakeState2 newState)
    {
        currentState = newState;
        stateTimer = 0f;

        switch (newState)
        {
            case SnakeState2.StateA:
                // Idle状態の初期化（必要なら）
                break;

            case SnakeState2.StateB:
                // 突進時の初期設定（必要なら）
                break;

            case SnakeState2.StateC:
                // 回復時の初期設定（必要なら）
                break;

            case SnakeState2.StateD:
                // 回復時の初期設定（必要なら）
                break;

            case SnakeState2.StateE:
                // 回復時の初期設定（必要なら）
                break;

            case SnakeState2.StateF:
                // 回復時の初期設定（必要なら）
                break;

            case SnakeState2.StateG:
                // 回復時の初期設定（必要なら）
                break;

            case SnakeState2.StateH:
                // 回復時の初期設定（必要なら）
                break;

            case SnakeState2.StateI:
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
        timeCounter += Time.fixedDeltaTime * frequency;
        float steerDirection = Mathf.Cos(timeCounter) * amplitude; // コサイン波でうねりを生成

        transform.Rotate(Vector3.up * steerDirection * steerSpeed * Time.fixedDeltaTime);
        // 通常の移動速度で前進
        transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;
    }

    private void Chase()
    {
        timeCounter += Time.fixedDeltaTime * frequency;
        Transform headPos = this.transform;
        Vector3 sinPos = headPos.position;
        sinPos.y += Mathf.Sin(timeCounter) * amplitude;
        headPos.position = sinPos;
        // 通常の移動速度で前進
        transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;
        fireCount += Time.fixedDeltaTime;
        if (fireCount >= fireInterval)
        {
            GameObject newBullet = Instantiate(enemyBullet, enemyFirePosition.position, enemyFirePosition.rotation);
            // 親子関係を解除
            newBullet.transform.SetParent(null);
            fireCount = 0f;
        }
    }

    private void Rush()
    {
        transform.position += transform.forward * rushSpeed * Time.fixedDeltaTime;
        transform.Rotate(Vector3.forward * spinSpeed * Time.fixedDeltaTime);
    }

    private void Decel()
    {
        transform.position += transform.forward * decelSpeed * Time.fixedDeltaTime;
    }

    private void Return()
    {
        Transform headPos = this.transform;
        Vector3 returnPos = headPos.position;
        returnPos.x = -5f;
        headPos.position = returnPos;
        // プレイヤーとの位置差を計算 (yz平面のみ)
        Vector3 directionToPlayer = (player.position + Vector3.forward * returnSpotZ - transform.position).normalized;
    
        // x成分を0にして、yz平面上の方向ベクトルを作成
        directionToPlayer.x = 0f;

        // ボスのx軸をプレイヤーに向けるための角度を計算
        angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.z) * Mathf.Rad2Deg;

        float nangle = -1f * angle;

        // 現在のボスの回転を取得し、x軸のみ回転を変更
        Quaternion targetRotation = Quaternion.Euler(nangle, transform.rotation.y, transform.rotation.z);

        // ボスの回転をx軸方向にのみターンさせる
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, steerSpeed * Time.fixedDeltaTime);

        // 突進速度を適用
        float currentMoveSpeed = moveSpeed * chargeSpeedMultiplier;
        
        transform.position += transform.forward * currentMoveSpeed * Time.fixedDeltaTime;
        Vector3 currentPosition = transform.position;
        bool isAtTargetPosition = Mathf.Abs(currentPosition.y - returnSpotY) < 0.25f && Mathf.Abs(currentPosition.z - player.position.z - returnSpotZ) < 2f;
        if(isAtTargetPosition)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            returnPos.x = 0f;
            headPos.position = returnPos;
            returnFlag = true;
        }
    }

    private void Prepare()
    {
        prepareCount += Time.fixedDeltaTime;
        // プレイヤーとの位置差を計算 (yz平面のみ)
        Vector3 directionToPlayer = (player.position + Vector3.forward * prepareSpotZ + Vector3.up * prepareSpotY - transform.position).normalized;
    
        // x成分を0にして、yz平面上の方向ベクトルを作成
        directionToPlayer.x = 0f;

        // ボスのx軸をプレイヤーに向けるための角度を計算
        angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.z) * Mathf.Rad2Deg;

        float nangle = -1f * angle;

        // 現在のボスの回転を取得し、x軸のみ回転を変更
        Quaternion targetRotation = Quaternion.Euler(nangle, transform.rotation.y, transform.rotation.z);

        // ボスの回転をx軸方向にのみターンさせる
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, steerSpeed * Time.fixedDeltaTime);

        // 突進速度を適用
        float currentMoveSpeed = moveSpeed * chargeSpeedMultiplier;
        transform.position += transform.forward * currentMoveSpeed * Time.fixedDeltaTime;
        Vector3 currentPosition = transform.position;
        bool isAtTargetPosition = Mathf.Abs(currentPosition.y - player.position.y - prepareSpotY) < 0.5f && Mathf.Abs(currentPosition.z - player.position.z - prepareSpotZ) < 0.5f;
        if(isAtTargetPosition && prepareCount >= prepareInterval)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            transform.rotation = Quaternion.Euler(90f, 0f, 0f); 
            prepareFlag = true;
        }
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
