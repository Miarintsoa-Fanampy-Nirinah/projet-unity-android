using UnityEngine;
using UnityEngine.UI;  // Pour gérer le Canvas UI
using System.Collections;
public class GameStartManager : MonoBehaviour
{
    public GameObject player;                // Le GameObject du player
    public GameObject obstacle;              // Le GameObject des obstacles
    public GameObject uiCanvas;              // Le Canvas du UI (le GameObject complet)
    public float gravityScale = 1f;          // Gravity scale modifiable dans l'inspecteur
    private Rigidbody2D playerRb;            // Le Rigidbody2D du player

    private bool gameStarted = false;

    private void Start()
    {
        playerRb = player.GetComponent<Rigidbody2D>();

        // Initialement, désactiver la gravité du player et les obstacles
        playerRb.isKinematic = true;  // Désactive la physique du player
        obstacle.SetActive(false);    // Désactive les obstacles
        uiCanvas.SetActive(true);     // Affiche le UI
    }

    private void Update()
    {
        if (!gameStarted && Input.touchCount > 0)  // Si l'écran est touché
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        // Démarrer le jeu
        gameStarted = true;

        // Désactiver l'UI
        uiCanvas.SetActive(false);

        // Réactiver la gravité du player et les obstacles
        playerRb.isKinematic = false;   // Réactive la physique du player
        playerRb.gravityScale = gravityScale;  // Applique la gravity scale modifiée dans l'inspecteur
        obstacle.SetActive(true);       // Active les obstacles
    }
}
