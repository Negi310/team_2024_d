using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; 

    private Rigidbody rb;

    public int jumpCount = 2;

    public float force;

    public float forcez;

    public Transform firePosition;

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

    public float health = 20f;

    public float damage = 5f;

    public float wallDamage = 20f;

    public CanvasGroup canvasGroup;  // フェードアウトさせるパネルにアタッチされている CanvasGroup

    public float fadeDuration = 1f;  // フェードアウトにかかる時間

    public AudioSource audioSource;

    public AudioClip fireSe;

    public AudioClip jumpSe;

    public AudioClip changeSe;

    public float charaChange = 0f;

    public string SceneName;

    public float collideForcey;

    public float collideForcez;

    public bool canSmash = false;

    public Image smashIcon;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        canvasGroup.alpha = 0;
        smashIcon.enabled = false;
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
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(bullet,firePosition);
            audioSource.PlayOneShot(fireSe);
        }
        if(health <= 0f)
        {
            SceneManager.LoadScene("ResultScene");
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
        if(charaChange < 0f)
        {
            charaChange = 2f;
        }
        if(charaChange == 0f)
        {   
            SP0text.enabled = true;
            SP1text.enabled = false;
            SP2text.enabled = false;
            SP0text.text = magazineSP0.ToString();
            if(Input.GetKeyDown(KeyCode.Mouse1) && magazineSP0 > 0)
            {
                magazineSP0 -= 1;
                Instantiate(bulletSP0,firePosition);
                Debug.Log("Fire0");
            }
        }
        if(charaChange == 1f)
        {   
            SP0text.enabled = false;
            SP1text.enabled = true;
            SP2text.enabled = false;
            SP1text.text = magazineSP1.ToString();
            if(Input.GetKeyDown(KeyCode.Mouse1) && magazineSP1 > 0)
            {
                magazineSP1 -= 1;
                Instantiate(bulletSP1,firePosition);
                Debug.Log("Fire1");
            }
        }
        if(charaChange == 2f)
        {   
            SP0text.enabled = false;
            SP1text.enabled = false;
            SP2text.enabled = true;
            SP2text.text = magazineSP2.ToString();
            if(Input.GetKeyDown(KeyCode.Mouse1) && magazineSP1 > 0)
            {
                magazineSP2 -= 1;
                Instantiate(bulletSP2,firePosition);
                Debug.Log("Fire2");
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
            canvasGroup.alpha = 1;
            // パネルがアクティブになったときにフェードアウトを開始
            StartCoroutine(FadeOut(canvasGroup, fadeDuration));
        }
        if(collision.gameObject.CompareTag("Wall"))
        {
            health -= wallDamage;
        }
        if(collision.gameObject.CompareTag("CourseClear1"))
        {
            SceneManager.LoadScene(SceneName);
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            health -= damage;
            rb.AddForce(0f,collideForcey,collideForcez);
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
}
