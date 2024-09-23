using UnityEngine;
using UnityEngine.SceneManagement;

public class TopButton : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("StartScene");
    }
}
