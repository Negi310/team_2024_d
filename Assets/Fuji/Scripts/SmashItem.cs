using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmashItem : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip itemSe;

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
            playerMovement.canSmash = true;
            playerMovement.smashIcon.enabled = true;
            audioSource.PlayOneShot(itemSe);
            Destroy(this.gameObject);
        }
    }
}
