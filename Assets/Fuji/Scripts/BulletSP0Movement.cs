using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSP0Movement : MonoBehaviour
{
    [SerializeField] private Transform firePositionTransform;
    [SerializeField] private float damage;
    [SerializeField] private float magicNumz;
    [SerializeField] private Collider bc;

    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = new Vector3(firePositionTransform.position.x, firePositionTransform.position.y, firePositionTransform.position.z + magicNumz);
        transform.position = newPosition;
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
    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            bc.isTrigger = false;
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.health -= damage;
        }
        if(collision.gameObject.CompareTag("SnakeBody"))
        {
            bc.isTrigger = false;
            SnakeBody snakeBody = collision.gameObject.GetComponent<SnakeBody>();
            snakeBody.bodyDamageFlag = true;
            snakeBody.bodyDamage += damage;
        }
        if(collision.gameObject.CompareTag("SnakeHead"))
        {
            bc.isTrigger = false;
            Snake3 snake3 = collision.gameObject.GetComponent<Snake3>();
            snake3.health -= damage;
        }
        bc.isTrigger = false;
    }
}
