using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel; // Assurez-vous d'avoir un panneau pour Game Over dans la scène

    public void GameOver()
    {
        // Affiche le panneau de Game Over
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Vous pouvez aussi arrêter d'autres éléments ici, comme les obstacles, la musique, etc.
        Time.timeScale = 0f; // Cela arrête tout le jeu (cela met en pause la physique, les entrées et les animations)
    }
}
