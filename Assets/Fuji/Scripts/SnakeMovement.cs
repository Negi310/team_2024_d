using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    private float timeCounter = 0f;
    [SerializeField] private float frequency = 1f; // 周期の速さ
    [SerializeField] private float amplitude = 1f; // うねりの大きさ

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeCounter += Time.deltaTime * frequency;
        float steerDirection = Mathf.Sin(timeCounter) * amplitude; // サイン波でうねりを生成
    }

}
