using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartManager : MonoBehaviour
{
    public Button restartButton;

    private void Start()
    {
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}