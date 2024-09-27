using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropFloor : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float killtime = 8f;
    [SerializeField] private bool drop;
    public bool tp;
    [SerializeField] private float tpCount;
    [SerializeField] private float tpInterval = 4f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(drop)
        {
            tpCount += Time.fixedDeltaTime;
            Destroy(this.gameObject,killtime);
            if (tpCount >= tpInterval)
            {
                tp = true;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("SnakeHead"))
        {
            rb.useGravity = true;
            drop = true;
        }
    }
}
