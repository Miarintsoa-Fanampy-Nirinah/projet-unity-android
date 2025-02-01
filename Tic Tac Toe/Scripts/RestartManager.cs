using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class RestartManager : MonoBehaviour
{
    public List<Button> restartButtons; // Liste de boutons pour le redémarrage

    private void Start()
    {
        // Vérifie si la liste n'est pas vide et ajoute l'écouteur à chaque bouton
        if (restartButtons != null && restartButtons.Count > 0)
        {
            foreach (Button button in restartButtons)
            {
                if (button != null)
                {
                    button.onClick.AddListener(RestartGame);
                }
            }
        }
    }

    public void RestartGame()
    {
        // Réinitialise le Time.timeScale et recharge la scène actuelle
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
