using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText1;
    public TextMeshProUGUI scoreText2;
    public TextMeshProUGUI bestScoreText;

    public RawImage rawImage;
    public Texture bronzeTexture;
    public Texture silverTexture;
    public Texture goldTexture;
    public Texture platinumTexture;
    public Texture diamondTexture;

    public AudioSource audioSource;
    public AudioClip scoreClip;

    private int score = 0;
    private int bestScore = 0;

    void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        UpdateScoreTexts();
        UpdateBestScoreText();
        UpdateImage();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Score"))
        {
            score++;
            UpdateScoreTexts();
            PlayScoreSound();

            if (score > bestScore)
            {
                bestScore = score;
                PlayerPrefs.SetInt("BestScore", bestScore);
                UpdateBestScoreText();
                UpdateImage();
            }
        }
    }

    void UpdateScoreTexts()
    {
        string scoreString = score.ToString();
        scoreText1.text = scoreString;
        scoreText2.text = scoreString;
    }

    void UpdateBestScoreText()
    {
        bestScoreText.text = bestScore.ToString();
    }

    void UpdateImage()
    {
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
            rawImage.texture = bronzeTexture;
        }
    }

    void PlayScoreSound()
    {
        audioSource?.PlayOneShot(scoreClip);
    }
}
