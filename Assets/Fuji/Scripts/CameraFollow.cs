using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float X = 8f;
    [SerializeField] private float Y = 4f;
    [SerializeField] private float CamaraPosition = 4f;

    void LateUpdate()
    {
        Vector3 newPosition = new Vector3(X, Y, playerTransform.position.z + CamaraPosition);
        transform.position = newPosition;
    }
} 