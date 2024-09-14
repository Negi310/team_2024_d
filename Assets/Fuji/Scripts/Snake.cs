using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Enemy
{   
    public float moveSpeed = 5f; 

    public float steerSpeed = 180f;

    public float bodySpeed = 5f;
    
    public int gap = 10;

    public GameObject snakeBody;

    public GameObject snakeBody2;

    public List<GameObject> bodyParts = new List<GameObject>();

    public List<Vector3> bodyLogs = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0 ; i < 4 ; i++)
        {
            GrowSnake2();
            GrowSnake();
        }
    }

    void Update()
    {
        float steerDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.left * steerDirection * steerSpeed * Time.deltaTime);

        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        bodyLogs.Insert(0, transform.position);
        int index = 0;
        foreach (var body in bodyParts)
        {
            Vector3 point = bodyLogs[Mathf.Min(index * gap, bodyLogs.Count-1)];
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * bodySpeed * Time.deltaTime;
            body.transform.LookAt(point);
            index++;
        }
        if (bodyLogs.Count > bodyParts.Count * gap)
        {
            bodyLogs.RemoveAt(bodyLogs.Count - 1);
        }
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
}
