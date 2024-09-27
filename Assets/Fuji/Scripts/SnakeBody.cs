using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    [SerializeField] private GameObject snakeHead;
    public Snake3 snake3;
    public float bodyDamage;
    public bool bodyDamageFlag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(bodyDamageFlag)
        {
            snake3.health -= bodyDamage;
            bodyDamageFlag = false;
            bodyDamage = 0f;
        }
        
    }
}
