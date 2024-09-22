using UnityEngine;

public class EnemyBulletMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = -7.5f;
    [SerializeField] private float killtime;

    private Rigidbody rb;

    [SerializeField] private float damage = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(this.gameObject,killtime);
    }

    void FixedUpdate()
    {
        Vector3 movement2 = Vector3.forward * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement2);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            playerMovement.health -= damage;
        }
        Destroy(this.gameObject);
    }
}