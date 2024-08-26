using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float X = 8f;
    public float Y = 4f;
    public float CamaraPosition = 4f;

    void LateUpdate()
    {
        Vector3 newPosition = new Vector3(X, Y, playerTransform.position.z + CamaraPosition);
        transform.position = newPosition;
    }
} 