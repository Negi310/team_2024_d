using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float moveSpeed = 20f; 
    public float killtime;
    private Rigidbody rb;

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
        Destroy(this.gameObject);
    }
}