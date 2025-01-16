using UnityEngine;
using UnityEngine.SceneManagement; // Nécessaire pour gérer les scènes

public class RestartButton : MonoBehaviour
{
    public void RestartGame()
    {
        // Réactive le temps avant de recharger la scène
        Time.timeScale = 1f;

        // Recharge la scène active
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Debug.Log("Jeu redémarré !");
    }
}
