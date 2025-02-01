using UnityEngine;
using UnityEngine.UI;

public class TicTacToeManager : MonoBehaviour
{
    public Sprite crossSprite; // Sprite pour la croix
    public Sprite circleSprite; // Sprite pour le cercle
    public Button[] buttons; // Les boutons du jeu
    private int[] gameState; // 0 = vide, 1 = cercle, 2 = croix
    private bool isPlayerOneTurn = true; // True = Joueur 1 (O), False = Joueur 2 (X)

    public GameObject playerOneTurnObject; // GameObject à afficher pour le tour du joueur 1 (O)
    public GameObject playerTwoTurnObject; // GameObject à afficher pour le tour du joueur 2 (X)

    public GameObject[] playerOneWinObjects; // Objets à activer si Joueur 1 gagne
    public GameObject[] playerTwoWinObjects; // Objets à activer si Joueur 2 gagne
    public GameObject[] drawObjects; // Objets à activer en cas de match nul

    void Start()
    {
        gameState = new int[9]; // Initialise l'état du jeu
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Pour capturer la bonne valeur dans le lambda
            buttons[i].onClick.AddListener(() => OnButtonClicked(index));
        }
        DisableAllResultObjects(); // Désactive tous les objets de résultat
        UpdateTurnIndicator(); // Met à jour l'affichage du tour
    }

    void OnButtonClicked(int index)
    {
        // Vérifie si la case est déjà remplie
        if (gameState[index] != 0)
            return;

        // Met à jour l'état du jeu
        gameState[index] = isPlayerOneTurn ? 1 : 2;

        // Met à jour l'image du bouton
        Image buttonImage = buttons[index].GetComponent<Image>();
        buttonImage.sprite = isPlayerOneTurn ? circleSprite : crossSprite;

        // Change la couleur de l'image
        buttonImage.color = isPlayerOneTurn ? Color.blue : Color.red;

        // Désactive le bouton
        buttons[index].interactable = false;

        // Vérifie si un joueur a gagné
        if (CheckWin())
        {
            if (isPlayerOneTurn)
            {
                Debug.Log("Joueur 1 gagne !");
                ActivateGameObjects(playerOneWinObjects);
            }
            else
            {
                Debug.Log("Joueur 2 gagne !");
                ActivateGameObjects(playerTwoWinObjects);
            }

            EndGame(); // Termine la partie
            return;
        }

        // Vérifie si le jeu est nul
        if (IsDraw())
        {
            Debug.Log("Match nul !");
            ActivateGameObjects(drawObjects);
            EndGame(); // Termine la partie
            return;
        }

        // Change de joueur
        isPlayerOneTurn = !isPlayerOneTurn;
        UpdateTurnIndicator(); // Met à jour l'affichage du tour
    }

    bool CheckWin()
    {
        int[,] winConditions = new int[,] {
            {0, 1, 2}, {3, 4, 5}, {6, 7, 8}, // Lignes
            {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, // Colonnes
            {0, 4, 8}, {2, 4, 6}             // Diagonales
        };

        for (int i = 0; i < winConditions.GetLength(0); i++)
        {
            int a = winConditions[i, 0];
            int b = winConditions[i, 1];
            int c = winConditions[i, 2];

            if (gameState[a] != 0 && gameState[a] == gameState[b] && gameState[b] == gameState[c])
            {
                return true;
            }
        }

        return false;
    }

    bool IsDraw()
    {
        // Vérifie si toutes les cases sont remplies sans vainqueur
        foreach (int state in gameState)
        {
            if (state == 0)
                return false;
        }

        return true;
    }

    void EndGame()
    {
        // Désactive tous les boutons pour empêcher tout clic supplémentaire
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }

        // Désactive les indicateurs de tour
        playerOneTurnObject.SetActive(false);
        playerTwoTurnObject.SetActive(false);
    }

    void ActivateGameObjects(GameObject[] objects)
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(true);
        }
    }

    void DisableAllResultObjects()
    {
        // Désactive tous les objets de résultat
        foreach (GameObject obj in playerOneWinObjects)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in playerTwoWinObjects)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in drawObjects)
        {
            obj.SetActive(false);
        }
    }

    void UpdateTurnIndicator()
    {
        // Active le GameObject correspondant au tour actuel
        playerOneTurnObject.SetActive(isPlayerOneTurn);
        playerTwoTurnObject.SetActive(!isPlayerOneTurn);
    }
}
