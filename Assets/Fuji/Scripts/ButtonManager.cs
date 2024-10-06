using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject guidePanel;
    [SerializeField] private CanvasGroup startButton;
    [SerializeField] private CanvasGroup guideButton;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip selectSe;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void StartOnClick()
    {
        Invoke("LoadStage", 0.15f);
        audioSource.PlayOneShot(selectSe);
        startButton.alpha = 1f;
        StartCoroutine(FadeOut(startButton, 0.1f));
    }
    public void StartOnPoint()
    {
        startButton.alpha = 0.2f;
    }
    public void GuideOnPoint()
    {   
        guideButton.alpha = 0.2f;
    }
    public void StartOutPoint()
    {
        startButton.alpha = 0f;
    }
    public void GuideOutPoint()
    {   
        guideButton.alpha = 0f;
    }
    public void GuideOnClick()
    {   
        Invoke("PanelTrue", 0.15f);
        audioSource.PlayOneShot(selectSe);
        guideButton.alpha = 1f;
        StartCoroutine(FadeOut(guideButton, 0.1f));
    }
    public void GuideOutOnClick()
    {
        audioSource.PlayOneShot(selectSe);
        guidePanel.SetActive(false);
    }
    IEnumerator FadeOut(CanvasGroup canvasGroup, float duration)
    {
        float elapsedTime = 0f;
        // フェードアウトの開始
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);  // アルファ値を徐々に0に近づける
            yield return null;  // フレームごとに待機
        }

        canvasGroup.alpha = 0f;  // 完全に透明にする
    }
    void LoadStage()
    {
        SceneManager.LoadScene("Stage1Scene");
    }
    void PanelTrue()
    {
        guidePanel.SetActive(true);
    }

}
