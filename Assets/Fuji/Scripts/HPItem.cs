using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPItem : MonoBehaviour
{   
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip itemSe;

    [SerializeField] private float heal = 5f;

    public PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        ItemGet(collision);
    }
    public void ItemGet(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerMovement.health += heal;
            audioSource.PlayOneShot(itemSe);
            Destroy(this.gameObject);
        }
    }
}
