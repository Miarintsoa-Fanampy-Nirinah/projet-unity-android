using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText1; // Premier TMP pour afficher le score
    public TextMeshProUGUI scoreText2; // Deuxième TMP pour afficher le même score
    public TextMeshProUGUI bestScoreText; // Affiche le meilleur score

    public RawImage rawImage; // RawImage à mettre à jour
    public Texture bronzeTexture; // Texture par défaut (Bronze)
    public Texture silverTexture; // Texture pour 25
    public Texture goldTexture; // Texture pour 50
    public Texture platinumTexture; // Texture pour 75
    public Texture diamondTexture; // Texture pour 100

    private int score = 0;
    private int bestScore = 0;

    void Start()
    {
        // Charger le meilleur score sauvegardé
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        UpdateScoreTexts();
        UpdateBestScoreText();
        UpdateImage(); // Mettre à jour la texture en fonction du meilleur score
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Score"))
        {
            score++;
            UpdateScoreTexts();

            // Mettre à jour le meilleur score si nécessaire
            if (score > bestScore)
            {
                bestScore = score;
                PlayerPrefs.SetInt("BestScore", bestScore); // Sauvegarder le meilleur score
                UpdateBestScoreText();
                UpdateImage(); // Mettre à jour la texture en fonction du meilleur score
            }
        }
    }

    void UpdateScoreTexts()
    {
        // Affiche le même score dans les deux TMP
        string scoreString = score.ToString();
        scoreText1.text = scoreString;
        scoreText2.text = scoreString;
    }

    void UpdateBestScoreText()
    {
        // Affiche uniquement le meilleur score
        bestScoreText.text = bestScore.ToString();
    }

    void UpdateImage()
    {
        // Changer la texture en fonction du meilleur score
        if (bestScore >= 100)
        {
            rawImage.texture = diamondTexture;
        }
        else if (bestScore >= 75)
        {
            rawImage.texture = platinumTexture;
        }
        else if (bestScore >= 50)
        {
            rawImage.texture = goldTexture;
        }
        else if (bestScore >= 25)
        {
            rawImage.texture = silverTexture;
        }
        else
        {
            rawImage.texture = bronzeTexture; // Par défaut
        }
    }
}
