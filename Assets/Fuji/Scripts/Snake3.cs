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
    StateI,
    StateJ,
    StateK,
    StateL,
    StateM,
    StateN,
    StateO,
    StateP,
    StateQ,
    StateR,
    StateS
}

public class Snake3 : MonoBehaviour
{
    public float health = 100f;
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float returnSpeed = 30f; // 突進時の速度倍率
    [SerializeField] private float rushSpeed = 100f;

    [SerializeField] private float decelSpeed = 7.5f;
    [SerializeField] private float steerSpeed = 500f;

    [SerializeField] private float spinSpeed = 5f;
    [SerializeField] private float bodySpeed = 10f;
    [SerializeField] private float gap = 5f;
    [SerializeField] private float returnGapSection = -2f;
    [SerializeField] private float rushGapSection = -4f;
    [SerializeField] private float decelGapSection = 5f;
    [SerializeField] private float nextGapSection = -0.5f;
    [SerializeField] private float next2GapSection = 1f;
    [SerializeField] private int bodyLength = 5;

    [SerializeField] private float returnSpotZ = -16f;

    [SerializeField] private float returnMinSpotY = 1.7f;

    [SerializeField] private float returnMaxSpotY = 2f;

    [SerializeField] private float prepareCount;

    [SerializeField] private float prepareInterval = 2f;

    [SerializeField] private float returnCount;

    [SerializeField] private float returnInterval = 10f;

    [SerializeField] private float prepareSpotY = 8f;

    [SerializeField] private float prepareSpotZ;

    [SerializeField] private float prepare2SpotY = 8f;

    [SerializeField] private float prepare2SpotZ;

    [SerializeField] private bool prepareFlag = false;

    [SerializeField] private bool prepare2Flag = false;

    [SerializeField] private GameObject snakeBody0;
    [SerializeField] private GameObject snakeBody;
    [SerializeField] private GameObject snakeBody2;
    [SerializeField] private GameObject snakeBody3;

    [SerializeField] private List<GameObject> bodyParts = new List<GameObject>();
    [SerializeField] private List<Vector3> bodyLogs = new List<Vector3>();

    [SerializeField] private float timeCounter = 0f;

    [SerializeField] private float frequency = 5f; // 周期の速さ
    [SerializeField] private float amplitude = 0.1f; // うねりの大きさ
    [SerializeField] private bool returnFlag = false;

    // ステート管理
    [SerializeField] private SnakeState2 currentState = SnakeState2.StateA;
    [SerializeField] private float stateTimer = 0f;

    [SerializeField] private float durationStandby = 1f;    // 突進の持続時間
    [SerializeField] private float durationChase = 10f; // 回復期間の持続時間
    [SerializeField] private float durationRush = 3f;
    [SerializeField] private float durationRush2 = 2f;
    [SerializeField] private float durationDecel = 3f;

    [SerializeField] private Transform player; // プレイヤーのTransformをアサイン

    [SerializeField] private float angle;

    [SerializeField] private float fireInterval = 3f;

    [SerializeField] private float fireCount;

    [SerializeField] private Transform enemyFirePosition;

    [SerializeField] private GameObject enemyBullet;

    public PlayerMovement playerMovement;


    // Start is called before the first frame update
    void Start()
    {
        GrowSnake0();
        for (int i = 0; i < bodyLength; i++)
        {
            GrowSnake();
            GrowSnake2();
            GrowSnake();
            GrowSnake3();
            GrowSnake();
            GrowSnake2();
        }
    }

    void FixedUpdate()
    {
        stateTimer += Time.fixedDeltaTime;
        if(health <= 0)
        {
            playerMovement.clearFlag = true;
            Destroy(this.gameObject);
        }

        switch (currentState)
        {
            case SnakeState2.StateA:
                // 一定時間後に突進状態へ移行
                if (stateTimer >= durationStandby)
                {
                    gap += returnGapSection;
                    ChangeState(SnakeState2.StateB);
                }
                break;

            case SnakeState2.StateB:
                // 突進中の動作
                Return();
                if (returnFlag)
                {
                    gap -= returnGapSection;
                    returnFlag = false;
                    ChangeState(SnakeState2.StateC);
                }
                break;

            case SnakeState2.StateC:
                // 回復中の動作（例えば、うねりながら前進）
                Chase();
                if (stateTimer >= durationChase)
                {
                    gap += decelGapSection;
                    ChangeState(SnakeState2.StateD);
                }
                break;

            case SnakeState2.StateD:
                // 回復中の動作（例えば、うねりながら前進）
                Decel();
                if (stateTimer >= durationDecel)
                {
                    gap -= decelGapSection;
                    gap += rushGapSection;
                    ChangeState(SnakeState2.StateE);
                }
                break;
            
            case SnakeState2.StateE:
                // 回復中の動作（例えば、うねりながら前進）
                Rush();
                if (stateTimer >= durationRush)
                {
                    gap -= rushGapSection;
                    gap += returnGapSection;
                    ChangeState(SnakeState2.StateF);
                }
                break;
            
            case SnakeState2.StateF:
                // 突進中の動作
                Return();
                if (returnFlag)
                {
                    gap -= returnGapSection;
                    returnFlag = false;
                    ChangeState(SnakeState2.StateG);
                }
                break;

            case SnakeState2.StateG:
                // 突進中の動作
                Chase();
                if (stateTimer >= durationChase)
                {
                    gap += returnGapSection;
                    ChangeState(SnakeState2.StateH);
                }
                break;

            case SnakeState2.StateH:
                // 突進中の動作
                Prepare();
                if (prepareFlag)
                {
                    gap -= returnGapSection;
                    gap += nextGapSection;
                    prepareFlag = false;
                    ChangeState(SnakeState2.StateI);
                }
                break;
            
            case SnakeState2.StateI:
                PrepareNext();
                // 一定時間後に突進状態へ移行
                if (stateTimer >= durationStandby)
                {
                    gap += rushGapSection;
                    gap -= nextGapSection;
                    ChangeState(SnakeState2.StateJ);
                }
                break;

            case SnakeState2.StateJ:
                // 回復中の動作（例えば、うねりながら前進）
                Rush();
                if (stateTimer >= durationRush2)
                {
                    gap -= rushGapSection;
                    gap += returnGapSection;
                    ChangeState(SnakeState2.StateK);
                }
                break; 
            
            case SnakeState2.StateK:
                // 突進中の動作
                Return();
                if (returnFlag)
                {
                    gap -= returnGapSection;
                    returnFlag = false;
                    ChangeState(SnakeState2.StateL);
                }
                break;

            case SnakeState2.StateL:
                // 突進中の動作
                Chase();
                if (stateTimer >= durationChase)
                {
                    gap += returnGapSection;
                    ChangeState(SnakeState2.StateM);
                }
                break;

            case SnakeState2.StateM:
                // 突進中の動作
                Prepare2();
                if (prepare2Flag)
                {
                    gap -= returnGapSection;
                    gap += next2GapSection;
                    prepare2Flag = false;
                    ChangeState(SnakeState2.StateN);
                }
                break;

            case SnakeState2.StateN:
                // 一定時間後に突進状態へ移行
                PrepareNext2();
                if (stateTimer >= durationStandby)
                {
                    gap += rushGapSection;
                    gap -= next2GapSection;
                    ChangeState(SnakeState2.StateO);
                }
                break;
            
            case SnakeState2.StateO:
                // 回復中の動作（例えば、うねりながら前進）
                Rush();
                if (stateTimer >= durationRush2)
                {
                    gap += returnGapSection;
                    gap -= rushGapSection;
                    ChangeState(SnakeState2.StateP);
                }
                break;

            case SnakeState2.StateP:
                // 突進中の動作
                Return();
                if (returnFlag)
                {
                    gap -= returnGapSection;
                    returnFlag = false;
                    ChangeState(SnakeState2.StateQ);
                }
                break;

            case SnakeState2.StateQ:
                // 回復中の動作（例えば、うねりながら前進）
                Chase();
                if (stateTimer >= durationChase)
                {
                    gap += decelGapSection;
                    ChangeState(SnakeState2.StateR);
                }
                break;

            case SnakeState2.StateR:
                // 回復中の動作（例えば、うねりながら前進）
                Decel();
                if (stateTimer >= durationDecel)
                {
                    gap -= decelGapSection;
                    gap += rushGapSection;
                    ChangeState(SnakeState2.StateS);
                }
                break;
            
            case SnakeState2.StateS:
                // 回復中の動作（例えば、うねりながら前進）
                Rush();
                if (stateTimer >= durationRush)
                {
                    gap -= rushGapSection;
                    gap += returnGapSection;
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
            
            case SnakeState2.StateJ:
                // Idle状態の初期化（必要なら）
                break;

            case SnakeState2.StateK:
                // 突進時の初期設定（必要なら）
                break;

            case SnakeState2.StateL:
                // 回復時の初期設定（必要なら）
                break;

            case SnakeState2.StateM:
                // 回復時の初期設定（必要なら）
                break;

            case SnakeState2.StateN:
                // 回復時の初期設定（必要なら）
                break;

            case SnakeState2.StateO:
                // 回復時の初期設定（必要なら）
                break;

            case SnakeState2.StateP:
                // 回復時の初期設定（必要なら）
                break;

            case SnakeState2.StateQ:
                // 回復時の初期設定（必要なら）
                break;
            
            case SnakeState2.StateR:
                // 回復時の初期設定（必要なら）
                break;

            case SnakeState2.StateS:
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
        transform.position += transform.forward * returnSpeed * Time.fixedDeltaTime;
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
        OnLand();
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
        OnLand();
        transform.position += transform.forward * rushSpeed * Time.fixedDeltaTime;
        transform.Rotate(Vector3.forward * spinSpeed * Time.fixedDeltaTime);
    }

    private void Decel()
    {
        OnLand2();
        transform.position += transform.forward * decelSpeed * Time.fixedDeltaTime;
    }

    private void Return()
    {
        returnCount += Time.fixedDeltaTime;
        Transform headPos = this.transform;
        Vector3 returnPos = headPos.position;
        if(returnPos.x > -5f)
        {
            returnPos.x -= Time.fixedDeltaTime * 3f;
            headPos.position = returnPos;
        }
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
        
        transform.position += transform.forward * returnSpeed * Time.fixedDeltaTime;
        Vector3 currentPosition = transform.position;
        bool isAtTargetPosition = currentPosition.y > returnMinSpotY && currentPosition.y < returnMaxSpotY && Mathf.Abs(currentPosition.z - player.position.z - returnSpotZ) < 4f;
        if(isAtTargetPosition || returnCount >= returnInterval)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            returnPos.x = 0f;
            headPos.position = returnPos;
            returnCount = 0f;
            returnFlag = true;
        }
    }

    private void Prepare()
    {
        prepareCount += Time.fixedDeltaTime;
        Transform headPos = this.transform;
        Vector3 returnPos = headPos.position;
        if(returnPos.x > -5f)
        {
            returnPos.x -= Time.fixedDeltaTime * 2f;
            headPos.position = returnPos;
        }
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
        transform.position += transform.forward * returnSpeed * Time.fixedDeltaTime;
        Vector3 currentPosition = transform.position;
        bool isAtTargetPosition = Mathf.Abs(currentPosition.y - player.position.y - prepareSpotY) < 0.5f && Mathf.Abs(currentPosition.z - player.position.z - prepareSpotZ) < 0.5f;
        if(isAtTargetPosition && prepareCount >= prepareInterval)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            returnPos.x = 0f;
            headPos.position = returnPos;
            prepareCount = 0f;
            prepareFlag = true;
        }
    }

    private void Prepare2()
    {
        prepareCount += Time.fixedDeltaTime;
        Transform headPos = this.transform;
        Vector3 returnPos = headPos.position;
        if(returnPos.x > -5f)
        {
            returnPos.x -= Time.fixedDeltaTime * 2f;
            headPos.position = returnPos;
        }
        // プレイヤーとの位置差を計算 (yz平面のみ)
        Vector3 directionToPlayer = (player.position + Vector3.forward * prepare2SpotZ + Vector3.up * prepare2SpotY - transform.position).normalized;
    
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
        transform.position += transform.forward * returnSpeed * Time.fixedDeltaTime;
        Vector3 currentPosition = transform.position;
        bool isAtTargetPosition = Mathf.Abs(currentPosition.y - player.position.y - prepare2SpotY) < 0.5f && Mathf.Abs(currentPosition.z - player.position.z - prepare2SpotZ) < 0.5f;
        if(isAtTargetPosition && prepareCount >= prepareInterval)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            returnPos.x = 0f;
            headPos.position = returnPos;
            prepareCount = 0f;
            prepare2Flag = true;
        }
    }
    private void PrepareNext()
    {
        Transform headPos = this.transform;
        Vector3 nextPos = headPos.position;
        nextPos.z += 25f * Time.fixedDeltaTime;
        headPos.position = nextPos;
    }

    private void PrepareNext2()
    {
        Transform headPos = this.transform;
        Vector3 nextPos = headPos.position;
        nextPos.z += 10f * Time.fixedDeltaTime;
        headPos.position = nextPos;
    }

    private void OnLand()
    {
        Transform headPos = this.transform;
        Vector3 onLandPos = headPos.position;
        if(onLandPos.y < 1.5f)
        {
            onLandPos.y += 3f * Time.fixedDeltaTime;
            headPos.position = onLandPos;
        }
        if(onLandPos.y < 1.25f)
        {
            onLandPos.x = -5f;
        }
        if(onLandPos.y >= 1.25f)
        {
            onLandPos.y = 0f;
        }
    }

    private void OnLand2()
    {
        Transform headPos = this.transform;
        Vector3 onLandPos = headPos.position;
        if(onLandPos.y > 1.8f)
        {
            onLandPos.y -= Time.fixedDeltaTime;
            headPos.position = onLandPos;
        }
    }


    private void UpdateBodyParts()
    {
        bodyLogs.Insert(0, transform.position);
        int index = 0;
        foreach (var body in bodyParts)
        {
            int bodyLogIndex = Mathf.Min(Mathf.FloorToInt(index * gap), bodyLogs.Count - 1); // gap が float になったため、インデックスを int に変換
            Vector3 point = bodyLogs[bodyLogIndex];
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * bodySpeed * Time.fixedDeltaTime;
            body.transform.LookAt(point);
            index++;
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
