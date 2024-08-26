using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; 

    private Rigidbody rb;

    int jumpCount = 2;

    public float force;

    public Transform firePosition;

    public GameObject bullet;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            rb.AddForce(0f,force,0f);
            jumpCount -= 1;
        }        
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(bullet,firePosition);
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 2;
        }
    }
}
