using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTest : MonoBehaviour
{   
    public float moveSpeed = 10f; 

    public float steerSpeed = 150f;

    public float bodySpeed = 10f;

    public float gap = 100;

    public int bodyLength = 4;

    public GameObject snakeBody0;

    public GameObject snakeBody;

    public GameObject snakeBody2;

    public GameObject snakeBody3;

    public List<GameObject> bodyParts = new List<GameObject>();

    public List<Vector3> bodyLogs = new List<Vector3>();

    

    



    [SerializeField] private float preparation;

    // Start is called before the first frame update
    void Start()
    {
        GrowSnake0();
        for(int i = 0 ; i < bodyLength ; i++)
        {
            GrowSnake();
            GrowSnake2();
            GrowSnake();
            GrowSnake2();
            GrowSnake();
            GrowSnake3();
        }
    }

    void FixedUpdate()
    {
        float steerDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerDirection * steerSpeed * Time.fixedDeltaTime);

        transform.position -= transform.right * moveSpeed * Time.fixedDeltaTime;
        bodyLogs.Insert(0, transform.position);
        int index = 0;
        foreach (var body in bodyParts)
        {
            int bodyLogIndex = Mathf.Min(Mathf.FloorToInt(index * gap), bodyLogs.Count - 1); // gap が float になったため、インデックスを int に変換
            Vector3 point = bodyLogs[bodyLogIndex];
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * bodySpeed * Time.fixedDeltaTime;
            body.transform.LookAt(point);
            index++;
        }
        if (bodyLogs.Count > bodyParts.Count * gap)
        {
            bodyLogs.RemoveAt(bodyLogs.Count - 1);
        }
    }

    private void GrowSnake0()
    {
        GameObject body = Instantiate(snakeBody0);
        bodyParts.Add(body);
    } 
    private void GrowSnake()
    {
        GameObject body = Instantiate(snakeBody);
        bodyParts.Add(body);
    } 
    private void GrowSnake2()
    {
        GameObject body = Instantiate(snakeBody2);
        bodyParts.Add(body);
    }
    private void GrowSnake3()
    {
        GameObject body = Instantiate(snakeBody3);
        bodyParts.Add(body);
    }
}
