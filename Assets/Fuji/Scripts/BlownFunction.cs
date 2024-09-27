using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlownFunction : MonoBehaviour
{
    [SerializeField] private float blowForcey;
    [SerializeField] private float blowForcez;
    [SerializeField] private float spinSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private bool collisionFlag;
    [SerializeField] private float killtime = 20f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(collisionFlag)
        {
            transform.Rotate(Vector3.forward * spinSpeed * Time.fixedDeltaTime);
            transform.Rotate(Vector3.up * spinSpeed * Time.fixedDeltaTime);
            Destroy(this.gameObject,killtime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("SnakeHead"))
        {
            rb.AddForce(0f,blowForcey,blowForcez);
            collisionFlag = true;
        }
        if(collision.gameObject.CompareTag("SnakeBody"))
        {
            rb.AddForce(0f,blowForcey,blowForcez);
            collisionFlag = true;
        }
        if(collision.gameObject.CompareTag("SnakeBody2"))
        {
            rb.AddForce(0f,blowForcey,blowForcez);
            collisionFlag = true;
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("SnakeHead"))
        {
            rb.AddForce(0f,blowForcey,blowForcez);
            collisionFlag = true;
        }
        if(collision.gameObject.CompareTag("SnakeBody"))
        {
            rb.AddForce(0f,blowForcey,blowForcez);
            collisionFlag = true;
        }
        if(collision.gameObject.CompareTag("SnakeBody2"))
        {
            rb.AddForce(0f,blowForcey,blowForcez);
            collisionFlag = true;
        }
    }
}
