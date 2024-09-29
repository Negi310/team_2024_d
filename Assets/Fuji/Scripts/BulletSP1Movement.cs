using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSP1Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float riseSpeed = 5f;
    [SerializeField] private Collider bc;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float killtime;
    [SerializeField] private float damage;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        Destroy(this.gameObject,killtime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement2 = Vector3.forward * moveSpeed * Time.fixedDeltaTime + Vector3.up * riseSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement2);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.health -= damage;
            bc.isTrigger = true;
        }
        if(collision.gameObject.CompareTag("SnakeBody"))
        {
            SnakeBody snakeBody = collision.gameObject.GetComponent<SnakeBody>();
            snakeBody.bodyDamageFlag = true;
            snakeBody.bodyDamage += damage;
            bc.isTrigger = true;
        }
        if(collision.gameObject.CompareTag("SnakeHead"))
        {
            Snake3 snake3 = collision.gameObject.GetComponent<Snake3>();
            snake3.health -= damage;
            bc.isTrigger = true;
        }
        bc.isTrigger = true;
    }
    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            bc.isTrigger = false;
        }
        if(collision.gameObject.CompareTag("SnakeBody"))
        {
            bc.isTrigger = false;
        }
        if(collision.gameObject.CompareTag("SnakeHead"))
        {
            bc.isTrigger = false;
        }
        if(collision.gameObject.CompareTag("SnakeBody2"))
        {
            bc.isTrigger = false;
        }
        bc.isTrigger = false;
    }
}
