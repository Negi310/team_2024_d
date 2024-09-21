using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    [SerializeField] private float health = 20f;

    [SerializeField] private float damage = 5f;

    [SerializeField] private float fireInterval = 5f;

    [SerializeField] private float fireCount;

    [SerializeField] private Transform enemyFirePosition;

    [SerializeField] private GameObject enemyBullet;

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
            GameObject newBullet = Instantiate(enemyBullet, enemyFirePosition.position, enemyFirePosition.rotation);
            // 親子関係を解除
            newBullet.transform.SetParent(null);
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
