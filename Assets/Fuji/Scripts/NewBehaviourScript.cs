using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private AudioClip stageBGM;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private float bgmLoop = 109.714f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("PlayBGM", 0f, bgmLoop);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayBGM()
    {
        bgmSource.clip = stageBGM;
        bgmSource.Play();
    }
}
