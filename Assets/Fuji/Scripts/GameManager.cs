using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerRoot;

    [SerializeField] private Canvas loadingCanvas;
    
    [SerializeField] private Canvas canvas;

    [SerializeField] private Canvas clearCanvas;

    [SerializeField] private AudioClip stageBGM;

    [SerializeField] private float bgmLoop = 109.714f;

    [SerializeField] private AudioSource bgmSource;

    // Start is called before the first frame update
    void Start()
    {
        playerRoot.SetActive(false);
        canvas.enabled = false;
        loadingCanvas.enabled = true;
        clearCanvas.enabled = false;
        Invoke("Loading", 1f);
        InvokeRepeating("PlayBGM", 1f, bgmLoop);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerRoot == null || !playerRoot.activeInHierarchy)
        {
            GameOver();
        }
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
    private void GameOver()
    {
        SceneManager.LoadScene("ResultScene"); // ゲームオーバーシーンの名前を指定
        Debug.Log("リザルト画面への移行");
    }
}
