using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public enum PlayerState
{
    StateGame,
    StateClear
}
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public float nomalSpeed = 5f;

    public float accelSpeed = 5f;
    [SerializeField] private PlayerState currentState = PlayerState.StateGame;
    public Rigidbody rb;

    private BoxCollider pbc;

    public int jumpCount = 2;

    [SerializeField] private float force = 2500f;

    [SerializeField] private float forcez;

    [SerializeField] private Transform firePosition;

    [SerializeField] private Transform firePosition2;

    [SerializeField] private Transform firePosition3;

    [SerializeField] private GameObject bullet;

    [SerializeField] private GameObject sp0Object;

    [SerializeField] private GameObject bulletSP1a;
    [SerializeField] private GameObject bulletSP1b;
    [SerializeField] private GameObject bulletSP1c;

    [SerializeField] private GameObject bulletSP2;

    [SerializeField] private int magazineSP0 = 99;

    [SerializeField] private int magazineSP1 = 99;

    [SerializeField] private int magazineSP2 = 99;

    [SerializeField] private TextMeshProUGUI SP0text;

    [SerializeField] private TextMeshProUGUI SP1text;

    [SerializeField] private TextMeshProUGUI SP2text;

    [SerializeField] private TextMeshProUGUI Chara0text;

    [SerializeField] private TextMeshProUGUI Chara1text;

    [SerializeField] private TextMeshProUGUI Chara2text;

    public float health = 20f;

    public float damage = 5f;

    public float wallDamage = 20f;

    public float snakeHeadDamage = 20f;

    public float snakeDamage = 0.25f;

    [SerializeField] private CanvasGroup canvasGroup;  // フェードアウトさせるパネルにアタッチされている CanvasGroup

    [SerializeField] private float fadeDuration = 1f;  // フェードアウトにかかる時間

    public AudioSource audioSource;
    
    [SerializeField] private AudioClip fireSe;

    [SerializeField] private AudioClip sp0Se;

    [SerializeField] private AudioClip sp1Se;

    [SerializeField] private AudioClip sp2Se;

    [SerializeField] private AudioClip jumpSe;

    public AudioClip itemSe;

    [SerializeField] private AudioClip changeSe;

    [SerializeField] private AudioClip damageSe;

    [SerializeField] private AudioClip dieSe;

    public float charaChange = 0f;

    public string SceneName;

    public string SceneName2;

    public string ResultScene;

    [SerializeField] private float collideForcey;

    [SerializeField] private float collideForcez;

    public bool canSmash = false;

    public Image smashIcon;

    public bool canJet = false;

    [SerializeField] private Image jetIcon;

    [SerializeField] private float jetInterval = 5f;

    [SerializeField] private float jetCount;

    public Image charaIcon0;

    public Image charaIcon1;

    public Image charaIcon2;

    [SerializeField] private Vector3 dive;

    public GameObject player1;

    public GameObject player2;

    public GameObject player3;

    public GameObject playerStoop1;

    public GameObject playerStoop2;

    public GameObject playerStoop3;

    public bool sp0Flag;

    public bool charaChangeFlag;

    public float sp0Count;

    public float sp2Count;

    public float charaChangeCount;

    public float sp0KillTime = 1f;

    public float charaChangeInterval = 0.5f;

    [SerializeField] private ParticleSystem sp0particle;
    public bool clearFlag;
    [SerializeField] private float clearCount;
    [SerializeField] private float clearInterval;
    [SerializeField] private TextMeshProUGUI courseClearText;
    [SerializeField] private Canvas clearCanvas;
    [SerializeField] private float clearFader;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pbc = GetComponent<BoxCollider>();
        canvasGroup.alpha = 0;
        smashIcon.enabled = false;
        jetIcon.enabled = false;
        dive.y = 15;
        player1.SetActive(true);
        player2.SetActive(false);
        player3.SetActive(false);
        playerStoop1.SetActive(false);
        playerStoop2.SetActive(false);
        playerStoop3.SetActive(false);
    }

    void FixedUpdate()
    {
        Vector3 movement = Vector3.forward * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
        if(sp0Flag)
        {
            sp0Count += Time.fixedDeltaTime;
            if(sp0Count >= sp0KillTime)
            {
                sp0Object.SetActive(false);
                sp0Count = 0f;
                sp0Flag = false;
            }
        }
        if(charaChangeFlag)
        {
            charaChangeCount += Time.fixedDeltaTime;
            if(charaChangeCount >= charaChangeInterval)
            {
                charaChangeCount = 0f;
                charaChangeFlag = false;
            }
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case PlayerState.StateGame:
                Game();
                if (clearFlag)
                {
                    ChangeState(PlayerState.StateClear);
                }
                break;

            case PlayerState.StateClear:
                clearCanvas.enabled = true;
                clearCount += Time.deltaTime;
                moveSpeed += 15f * Time.deltaTime;
                if(clearCount < clearInterval * 0.015f)
                {
                    courseClearText.transform.localScale += 3f * (Vector3.right + Vector3.up) * Time.deltaTime;
                    StartCoroutine(FadeIn());
                }
                if(clearCount > clearInterval)
                {
                    SceneManager.LoadScene(SceneName);
                }
                break;
        }
    }

    void Game()
    {
        if(jetIcon.enabled)
        {
            jetCount += Time.deltaTime;
            if(jetCount >= jetInterval)
            {
                jetIcon.enabled = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {    
            rb.velocity = Vector3.zero;
            rb.AddForce(0f,force,forcez);
            jumpCount -= 1;
            audioSource.PlayOneShot(jumpSe);
        }
        
        if(Input.GetKeyDown(KeyCode.D) && !(charaChangeFlag))
        {
            charaChange += 1f;
            audioSource.PlayOneShot(changeSe);
            charaChangeFlag = true;
        }
        if(Input.GetKeyDown(KeyCode.A) && !(charaChangeFlag))
        {
            charaChange -= 1f;
            audioSource.PlayOneShot(changeSe);
            charaChangeFlag = true;
        }
        if(Input.GetKeyDown(KeyCode.S) && rb.velocity.y != 0)
        {
            rb.velocity -= dive;
        }
        if(Input.GetKey(KeyCode.S) && rb.velocity.y == 0)
        {   
            if(charaChange == 0f)
            {
                player1.SetActive(false);
                player2.SetActive(false);
                player3.SetActive(false);
                playerStoop1.SetActive(true);
                playerStoop2.SetActive(false);
                playerStoop3.SetActive(false);
                pbc.size = new Vector3(0.4f, 1f, 0.4f);
                pbc.center = new Vector3(0f, 0.12f, 0f);
                moveSpeed = accelSpeed;
            }
            if(charaChange == 1f)
            {
                player1.SetActive(false);
                player2.SetActive(false);
                player3.SetActive(false);
                playerStoop1.SetActive(false);
                playerStoop2.SetActive(true);
                playerStoop3.SetActive(false);
                pbc.size = new Vector3(0.4f, 1f, 0.4f);
                pbc.center = new Vector3(0f, 0.12f, 0f);
                moveSpeed = accelSpeed;
            }
            if(charaChange == 2f)
            {
                player1.SetActive(false);
                player2.SetActive(false);
                player3.SetActive(false);
                playerStoop1.SetActive(false);
                playerStoop2.SetActive(false);
                playerStoop3.SetActive(true);
                pbc.size = new Vector3(0.4f, 1f, 0.4f);
                pbc.center = new Vector3(0f, 0.12f, 0f);
                moveSpeed = accelSpeed;
            }
        }
        if(health <= 0f)
        {
            audioSource.PlayOneShot(dieSe);
            SceneManager.LoadScene(ResultScene);
        }
        if(health > 200f)
        {
            health = 200f;
        }
        if(charaChange < 0f)
        {
            charaChange = 2f;
        }
        if(charaChange == 0f && !(Input.GetKey(KeyCode.S)))
        {   
            if(Input.GetKeyUp(KeyCode.S))
            {
            player1.SetActive(true);
            player2.SetActive(false);
            player3.SetActive(false);
            playerStoop1.SetActive(false);
            playerStoop2.SetActive(false);
            playerStoop3.SetActive(false);
            pbc.size = new Vector3(0.4f, 2.25f, 0.4f);
            pbc.center = new Vector3(0f, 0.75f, 0f);
            moveSpeed = nomalSpeed;
            }
            player1.SetActive(true);
            player2.SetActive(false);
            player3.SetActive(false);
            SP0text.enabled = true;
            SP1text.enabled = false;
            SP2text.enabled = false;
            charaIcon0.enabled = true;
            charaIcon1.enabled = false;
            charaIcon2.enabled = false;
            Chara0text.enabled = true;
            Chara1text.enabled = false;
            Chara2text.enabled = false;
            SP0text.text = "Bullet1 x" + magazineSP0.ToString();
            if(Input.GetKeyDown(KeyCode.Mouse0) && !(Input.GetKey(KeyCode.S)))
            {
                Instantiate(bullet,firePosition);
                audioSource.PlayOneShot(fireSe);
            }
            if(Input.GetKeyDown(KeyCode.Mouse1) && magazineSP0 > 0 && !(Input.GetKey(KeyCode.S)) && !(sp0Flag))
            {
                magazineSP0 -= 1;
                sp0Object.SetActive(true);
                sp0Flag = true;
                sp0particle.Play();
                audioSource.PlayOneShot(sp0Se);
            }
        }
        if(charaChange == 1f && !(Input.GetKey(KeyCode.S)))
        {   
            if(Input.GetKeyUp(KeyCode.S))
            {
            player1.SetActive(false);
            player2.SetActive(true);
            player3.SetActive(false);
            playerStoop1.SetActive(false);
            playerStoop2.SetActive(false);
            playerStoop3.SetActive(false);
            pbc.size = new Vector3(0.4f, 2.25f, 0.4f);
            pbc.center = new Vector3(0f, 0.75f, 0f);
            moveSpeed = nomalSpeed;
            }
            player1.SetActive(false);
            player2.SetActive(true);
            player3.SetActive(false);
            SP0text.enabled = false;
            SP1text.enabled = true;
            SP2text.enabled = false;
            charaIcon0.enabled = false;
            charaIcon1.enabled = true;
            charaIcon2.enabled = false;
            Chara0text.enabled = false;
            Chara1text.enabled = true;
            Chara2text.enabled = false;
            SP1text.text = "Bullet2 x" + magazineSP1.ToString();
            if(Input.GetKeyDown(KeyCode.Mouse0) && !(Input.GetKey(KeyCode.S)))
            {
                Instantiate(bullet,firePosition2);
                audioSource.PlayOneShot(fireSe);
            }
            if(Input.GetKeyDown(KeyCode.Mouse1) && magazineSP1 > 0 && !(Input.GetKey(KeyCode.S)))
            {
                magazineSP1 -= 1;
                Instantiate(bulletSP1a,firePosition2);
                Instantiate(bulletSP1b,firePosition2);
                Instantiate(bulletSP1c,firePosition2);
                audioSource.PlayOneShot(sp1Se);
            }
        }
        if(charaChange == 2f && !(Input.GetKey(KeyCode.S)))
        {   
            if(Input.GetKeyUp(KeyCode.S))
            {
            player1.SetActive(true);
            player2.SetActive(false);
            player3.SetActive(true);
            playerStoop1.SetActive(false);
            playerStoop2.SetActive(false);
            playerStoop3.SetActive(false);
            pbc.size = new Vector3(0.4f, 2.25f, 0.4f);
            pbc.center = new Vector3(0f, 0.75f, 0f);
            moveSpeed = nomalSpeed;
            }
            player1.SetActive(false);
            player2.SetActive(false);
            player3.SetActive(true);
            SP0text.enabled = false;
            SP1text.enabled = false;
            SP2text.enabled = true;
            charaIcon0.enabled = false;
            charaIcon1.enabled = false;
            charaIcon2.enabled = true;
            Chara0text.enabled = false;
            Chara1text.enabled = false;
            Chara2text.enabled = true;
            SP2text.text = "Bullet3 x" + magazineSP2.ToString();
            if(Input.GetKeyDown(KeyCode.Mouse0) && !(Input.GetKey(KeyCode.S)))
            {
                Instantiate(bullet,firePosition3);
                audioSource.PlayOneShot(fireSe);
            }
            if(Input.GetKeyDown(KeyCode.Mouse1) && magazineSP1 > 0 && !(Input.GetKey(KeyCode.S)))
            {
                sp2Count = 0f;
            }
            if(Input.GetKey(KeyCode.Mouse1) && magazineSP1 > 0 && !(Input.GetKey(KeyCode.S)))
            {
                sp2Count += Time.deltaTime;
            }
            if(Input.GetKeyUp(KeyCode.Mouse1) && magazineSP1 > 0 && !(Input.GetKey(KeyCode.S)))
            {
                magazineSP2 -= 1;
                Instantiate(bulletSP2,firePosition3);
                audioSource.PlayOneShot(sp2Se);
            }
        }
        if(charaChange > 2f)
        {
            charaChange = 0f;
        }
        if(canSmash && Input.GetKeyDown(KeyCode.C))
        {
            canSmash = false;
            smashIcon.enabled = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground") && jetIcon.enabled)
        {
            jumpCount = 3;
        }
        if(collision.gameObject.CompareTag("Ground") && !(jetIcon.enabled))
        {
            jumpCount = 2;
        }
        if(collision.gameObject.CompareTag("EnemyBullet"))
        {
            health -= damage;
            audioSource.PlayOneShot(damageSe);
            rb.AddForce(0f,collideForcey,collideForcez);
            canvasGroup.alpha = 1;
            // パネルがアクティブになったときにフェードアウトを開始
            StartCoroutine(FadeOut(canvasGroup, fadeDuration));
        }
        if(collision.gameObject.CompareTag("Wall"))
        {
            health -= wallDamage;
            audioSource.PlayOneShot(damageSe);
            rb.AddForce(0f,collideForcey,collideForcez);
            canvasGroup.alpha = 1;
            // パネルがアクティブになったときにフェードアウトを開始
            StartCoroutine(FadeOut(canvasGroup, fadeDuration));
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            health -= damage;
            audioSource.PlayOneShot(damageSe);
            rb.AddForce(0f,collideForcey,collideForcez);
            canvasGroup.alpha = 1;
            // パネルがアクティブになったときにフェードアウトを開始
            StartCoroutine(FadeOut(canvasGroup, fadeDuration));
        }
        if(collision.gameObject.CompareTag("SnakeBody"))
        {
            health -= snakeDamage;
            audioSource.PlayOneShot(damageSe);
            rb.AddForce(0f,collideForcey,0f);
            canvasGroup.alpha = 1;
            // パネルがアクティブになったときにフェードアウトを開始
            StartCoroutine(FadeOut(canvasGroup, fadeDuration));
        }
        if(collision.gameObject.CompareTag("SnakeBody2"))
        {
            health -= snakeDamage;
            audioSource.PlayOneShot(damageSe);
            rb.AddForce(0f,collideForcey,0f);
            canvasGroup.alpha = 1;
            // パネルがアクティブになったときにフェードアウトを開始
            StartCoroutine(FadeOut(canvasGroup, fadeDuration));
        }
        if(collision.gameObject.CompareTag("SnakeHead"))
        {
            health -= snakeHeadDamage;
            audioSource.PlayOneShot(damageSe);
            rb.AddForce(0f,collideForcey,collideForcez * -1f);
            canvasGroup.alpha = 1;
            // パネルがアクティブになったときにフェードアウトを開始
            StartCoroutine(FadeOut(canvasGroup, fadeDuration));
        }
        if(collision.gameObject.CompareTag("CourseClear1"))
        {
            clearFlag = true;
        }
        if(collision.gameObject.CompareTag("CourseLoop"))
        {
            SceneManager.LoadScene(SceneName2);
        }
        if(collision.gameObject.CompareTag("JetItem"))
        {
            jumpCount = 3;
            jetIcon.enabled = true;
            audioSource.PlayOneShot(itemSe);
        }
    }
    IEnumerator FadeOut(CanvasGroup canvasGroup, float duration)
    {
        float elapsedTime = 0f;

        // フェードアウトの開始
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);  // アルファ値を徐々に0に近づける
            yield return null;  // フレームごとに待機
        }

        canvasGroup.alpha = 0f;  // 完全に透明にする
    }
    IEnumerator FadeIn()
    {
        Color color = courseClearText.color;
        color.a = 0;
        courseClearText.color = color;
        float elapsedTime = 0f;

        // フェードアウトの開始
        while (elapsedTime < clearInterval * 0.015f)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 100f, elapsedTime / clearInterval * 0.015f);  // アルファ値を徐々に0に近づける
            courseClearText.color = color;
            yield return null;  // フレームごとに待機
        }

        color.a = 1;  // 完全に透明にする
        courseClearText.color = color;
    }
    private void ChangeState(PlayerState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case PlayerState.StateGame:
                // Idle状態の初期化（必要なら）
                break;

            case PlayerState.StateClear:
                // 突進時の初期設定（必要なら）
                break;
        }
    }
}
