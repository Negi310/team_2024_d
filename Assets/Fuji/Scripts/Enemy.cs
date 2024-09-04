using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    public float health = 20f;

    public float damage = 5f;

    public float fireInterval = 5f;

    public float fireCount;

    public Transform enemyFirePosition;

    public GameObject enemyBullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Des();
    }

    void FixedUpdate()
    {
        EnemyFire();
    }


    void OnCollisionEnter(Collision collision)
    {
        EnemyHitPoint(collision);
    }

    public void Des()
    {
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void EnemyFire()
    {
        fireCount += Time.fixedDeltaTime;
        if (fireCount >= fireInterval)
        {
            Instantiate(enemyBullet,enemyFirePosition);
            fireCount = 0f;
        }
    }

    public void EnemyHitPoint(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            health -= damage;
        }
    }
}
