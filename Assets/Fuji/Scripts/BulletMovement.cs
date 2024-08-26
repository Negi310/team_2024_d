using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float moveSpeed = 20f; 

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 movement2 = Vector3.forward * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement2);
    }
}