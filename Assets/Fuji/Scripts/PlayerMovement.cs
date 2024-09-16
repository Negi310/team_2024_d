using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; 

    private Rigidbody rb;

    int jumpCount = 2;

    public float force;

    public float forcez;

    public Transform firePosition;

    public GameObject bullet;

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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        canvasGroup.alpha = 0;
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
            if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                Debug.Log("Fire0");
            }
        }
        if(charaChange == 1f)
        {
            if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                Debug.Log("Fire1");
            }
        }
        if(charaChange == 2f)
        {
            if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                Debug.Log("Fire2");
            }
        }
        if(charaChange > 2f)
        {
            charaChange = 0f;
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
            SceneManager.LoadScene("CourseScene2");
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
