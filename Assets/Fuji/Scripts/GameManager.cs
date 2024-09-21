using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerRoot;

    [SerializeField] private Canvas loadingCanvas;
    
    [SerializeField] private Canvas canvas;

    [SerializeField] private AudioClip stageBGM;

    [SerializeField] private AudioSource bgmSource;

    // Start is called before the first frame update
    void Start()
    {
        playerRoot.SetActive(false);
        canvas.enabled = false;
        loadingCanvas.enabled = true;
        Invoke("Loading", 1f);
        InvokeRepeating("PlayBGM", 1f, 109.714f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Loading()
    {
        playerRoot.SetActive(true);
        loadingCanvas.enabled = false;
        canvas.enabled = true;
    }
    public void PlayBGM()
    {
        bgmSource.clip = stageBGM;
        bgmSource.Play();
    }
}
