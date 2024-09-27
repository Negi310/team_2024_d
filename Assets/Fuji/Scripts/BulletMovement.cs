using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20f; 
    [SerializeField] private float killtime;

    private Rigidbody rb;

    [SerializeField] private float damage;

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
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.health -= damage;
            Destroy(this.gameObject);
        }
        if(collision.gameObject.CompareTag("SnakeBody"))
        {
            SnakeBody snakeBody = collision.gameObject.GetComponent<SnakeBody>();
            snakeBody.bodyDamageFlag = true;
            snakeBody.bodyDamage += damage;
            Destroy(this.gameObject);
        }
        Destroy(this.gameObject);
    }
}