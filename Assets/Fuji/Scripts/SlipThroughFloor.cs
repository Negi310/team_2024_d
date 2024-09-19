using UnityEngine;

public class SlipThroughFloor : MonoBehaviour
{
    private Collider bc;

    public PlayerMovement playerMovement;

    public GameObject player;

    void Start()
    {
        // 床の通常のColliderを取得
        bc = GetComponent<BoxCollider>();

        playerMovement.rb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(playerMovement.rb.velocity.y > 0 || Input.GetKey(KeyCode.S))
        {
            bc.isTrigger = true;
        }
        else
        {
            bc.isTrigger = false;
        }
    }
}