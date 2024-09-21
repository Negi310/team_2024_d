using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFunction : MonoBehaviour
{
    [SerializeField] private float enemyProceedForce = -5f;

    [SerializeField] private float enemyStopForce = 5f;

    private Rigidbody rb;

    [SerializeField] private float enemyJumpForce = 2500f;

    [SerializeField] private float enemyJumpCount;

    [SerializeField] private float enemyJumpInterval;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        enemyJumpCount += Time.fixedDeltaTime;
        if(enemyJumpCount >= enemyJumpInterval)
        {
            rb.AddForce(0f,enemyJumpForce,enemyProceedForce);
            enemyJumpCount = 0f;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            rb.AddForce(0f,0f,enemyStopForce);
        }
    }

}
