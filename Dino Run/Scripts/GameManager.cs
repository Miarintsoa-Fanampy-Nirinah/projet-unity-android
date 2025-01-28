using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject scoreHide;

    public void GameOver()
    {
        gameOverPanel?.SetActive(true);
        scoreHide?.SetActive(false);
        Time.timeScale = 0f;
    }
}
