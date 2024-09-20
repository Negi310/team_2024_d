using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; 

    public Rigidbody rb;

    private BoxCollider pbc;

    public int jumpCount = 2;

    public float force;

    public float forcez;

    public Transform firePosition;

    public Transform firePosition2;

    public Transform firePosition3;

    public GameObject bullet;

    public GameObject bulletSP0;

    public GameObject bulletSP1;

    public GameObject bulletSP2;

    public int magazineSP0 = 99;

    public int magazineSP1 = 99;

    public int magazineSP2 = 99;

    public TextMeshProUGUI SP0text;

    public TextMeshProUGUI SP1text;

    public TextMeshProUGUI SP2text;

    public TextMeshProUGUI Chara0text;

    public TextMeshProUGUI Chara1text;

    public TextMeshProUGUI Chara2text;

    public float health = 20f;

    public float damage = 5f;

    public float wallDamage = 20f;

    public CanvasGroup canvasGroup;  // フェードアウトさせるパネルにアタッチされている CanvasGroup

    public float fadeDuration = 1f;  // フェードアウトにかかる時間

    public AudioSource audioSource;

    public AudioSource bgmSource;
    
    public AudioClip fireSe;

    public AudioClip jumpSe;

    public AudioClip changeSe;

    public AudioClip damageSe;

    public AudioClip stageBGM;

    public float charaChange = 0f;

    public string SceneName;

    public float collideForcey;

    public float collideForcez;

    public bool canSmash = false;

    public Image smashIcon;

    public Image charaIcon0;

    public Image charaIcon1;

    public Image charaIcon2;

    public Vector3 dive;

    public GameObject player1;

    public GameObject player2;

    public GameObject player3;

    public GameObject playerStoop;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pbc = GetComponent<BoxCollider>();
        canvasGroup.alpha = 0;
        smashIcon.enabled = false;
        dive.y = 5;
        player1.SetActive(true);
        player2.SetActive(false);
        player3.SetActive(false);
        playerStoop.SetActive(false);
        InvokeRepeating("PlayBGM", 1f, 109.714f);
    }

    void FixedUpdate()
    {
        Vector3 movement = Vector3.forward * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {    
            rb.AddForce(0f,force,forcez);
            jumpCount -= 1;
            audioSource.PlayOneShot(jumpSe);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            charaChange += 1f;
            audioSource.PlayOneShot(changeSe);
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            charaChange -= 1f;
            audioSource.PlayOneShot(changeSe);
        }
        if(Input.GetKeyDown(KeyCode.S) && rb.velocity.y != 0)
        {
            rb.velocity -= dive;
        }
        if(Input.GetKey(KeyCode.S) && rb.velocity.y == 0)
        {
            player1.SetActive(false);
            player2.SetActive(false);
            player3.SetActive(false);
            playerStoop.SetActive(true);
            pbc.size = new Vector3(1f, 1f, 1f);
            pbc.center = new Vector3(0f, 0.12f, 0f);
            moveSpeed = 10f;
        }
        if(health <= 0f)
        {
            SceneManager.LoadScene("ResultScene");
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
            playerStoop.SetActive(false);
            pbc.size = new Vector3(1f, 2.25f, 1f);
            pbc.center = new Vector3(0f, 0.75f, 0f);
            moveSpeed = 5f;
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
            if(Input.GetKeyDown(KeyCode.Mouse1) && magazineSP0 > 0 && !(Input.GetKey(KeyCode.S)))
            {
                magazineSP0 -= 1;
                Instantiate(bulletSP0,firePosition);
            }
        }
        if(charaChange == 1f && !(Input.GetKey(KeyCode.S)))
        {   
            if(Input.GetKeyUp(KeyCode.S))
            {
            player1.SetActive(false);
            player2.SetActive(true);
            player3.SetActive(false);
            playerStoop.SetActive(false);
            pbc.size = new Vector3(1f, 2.25f, 1f);
            pbc.center = new Vector3(0f, 0.75f, 0f);
            moveSpeed = 5f;
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
                Instantiate(bulletSP1,firePosition2);
            }
        }
        if(charaChange == 2f && !(Input.GetKey(KeyCode.S)))
        {   
            if(Input.GetKeyUp(KeyCode.S))
            {
            player1.SetActive(true);
            player2.SetActive(false);
            player3.SetActive(true);
            playerStoop.SetActive(false);
            pbc.size = new Vector3(1f, 2.25f, 1f);
            pbc.center = new Vector3(0f, 0.75f, 0f);
            moveSpeed = 5f;
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
                magazineSP2 -= 1;
                Instantiate(bulletSP2,firePosition3);
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
        if(collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 2;
        }
        if(collision.gameObject.CompareTag("EnemyBullet"))
        {
            health -= damage;
            audioSource.PlayOneShot(damageSe);
            canvasGroup.alpha = 1;
            // パネルがアクティブになったときにフェードアウトを開始
            StartCoroutine(FadeOut(canvasGroup, fadeDuration));
        }
        if(collision.gameObject.CompareTag("Wall"))
        {
            health -= wallDamage;
            audioSource.PlayOneShot(damageSe);
            rb.AddForce(0f,collideForcey,collideForcez);
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            health -= damage;
            rb.AddForce(0f,collideForcey,collideForcez);
        }
        if(collision.gameObject.CompareTag("CourseClear1"))
        {
            SceneManager.LoadScene(SceneName);
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
    public void PlayBGM()
    {
        audioSource.clip = stageBGM;
        audioSource.Play();
    }
}
