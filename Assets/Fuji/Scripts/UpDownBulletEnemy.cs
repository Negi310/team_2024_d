using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownBulletEnemy : Enemy
{
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

}
