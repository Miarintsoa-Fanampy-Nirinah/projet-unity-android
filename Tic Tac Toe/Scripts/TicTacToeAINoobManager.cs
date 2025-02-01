using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TicTacToeAINoobManager : MonoBehaviour
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
        if (gameState[index] != 0) return;

        gameState[index] = isPlayerOneTurn ? 1 : 2;

        Image buttonImage = buttons[index].GetComponent<Image>();
        buttonImage.sprite = isPlayerOneTurn ? circleSprite : crossSprite;
        buttonImage.color = isPlayerOneTurn ? Color.blue : Color.red;
        buttons[index].interactable = false;

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
            EndGame();
            return;
        }

        if (IsDraw())
        {
            Debug.Log("Match nul !");
            ActivateGameObjects(drawObjects);
            EndGame();
            return;
        }

        isPlayerOneTurn = !isPlayerOneTurn;
        UpdateTurnIndicator();

        if (!isPlayerOneTurn)
        {
            AIMove();
        }
    }

    void AIMove()
    {
        // Probabilité que l'IA fasse un coup aléatoire
        float randomChance = 0.3f; // 30% de chance de jouer aléatoirement
        if (Random.value < randomChance)
        {
            // Choix d'un coup aléatoire
            int randomMove = GetRandomMove();
            OnButtonClicked(randomMove);
        }
        else
        {
            // Sinon, utiliser l'algorithme Minimax
            int bestMove = FindBestMove();
            if (bestMove != -1)
            {
                OnButtonClicked(bestMove);
            }
        }
    }

    int GetRandomMove()
    {
        List<int> availableMoves = new List<int>();
        for (int i = 0; i < gameState.Length; i++)
        {
            if (gameState[i] == 0)
            {
                availableMoves.Add(i);
            }
        }

        if (availableMoves.Count > 0)
        {
            return availableMoves[Random.Range(0, availableMoves.Count)];
        }

        return -1; // Aucun coup possible (normalement jamais atteint)
    }

    int FindBestMove()
    {
        int bestScore = int.MinValue;
        int move = -1;

        for (int i = 0; i < gameState.Length; i++)
        {
            if (gameState[i] == 0)
            {
                gameState[i] = 2;
                int score = Minimax(gameState, false);
                gameState[i] = 0;

                if (score > bestScore)
                {
                    bestScore = score;
                    move = i;
                }
            }
        }

        return move;
    }

    int Minimax(int[] board, bool isMaximizing)
    {
        if (CheckWinForAI(2)) return 10;
        if (CheckWinForAI(1)) return -10;
        if (IsDrawForAI(board)) return 0;

        if (isMaximizing)
        {
            int bestScore = int.MinValue;
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == 0)
                {
                    board[i] = 2;
                    int score = Minimax(board, false);
                    board[i] = 0;
                    bestScore = Mathf.Max(score, bestScore);
                }
            }
            return bestScore;
        }
        else
        {
            int bestScore = int.MaxValue;
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == 0)
                {
                    board[i] = 1;
                    int score = Minimax(board, true);
                    board[i] = 0;
                    bestScore = Mathf.Min(score, bestScore);
                }
            }
            return bestScore;
        }
    }

    bool CheckWinForAI(int player)
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

            if (gameState[a] == player && gameState[b] == player && gameState[c] == player)
            {
                return true;
            }
        }

        return false;
    }

    bool IsDrawForAI(int[] board)
    {
        foreach (int state in board)
        {
            if (state == 0) return false;
        }
        return true;
    }

    bool CheckWin()
    {
        return CheckWinForAI(1) || CheckWinForAI(2);
    }

    bool IsDraw()
    {
        return IsDrawForAI(gameState);
    }

    void EndGame()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
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
        playerOneTurnObject.SetActive(isPlayerOneTurn);
        playerTwoTurnObject.SetActive(!isPlayerOneTurn);
    }
}
