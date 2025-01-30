using UnityEngine;
using UnityEngine.UI;

public class BatteryIndicator : MonoBehaviour
{
    // Références assignables via l'inspecteur
    [SerializeField] private Slider batterySlider;
    [SerializeField] private Image sliderFill;

    // Couleurs pour les différents niveaux de batterie
    [SerializeField] private Color greenColor = Color.green;
    [SerializeField] private Color yellowColor = Color.yellow;
    [SerializeField] private Color orangeColor = new Color(1f, 0.65f, 0f); // Orange
    [SerializeField] private Color redColor = Color.red;

    void Start()
    {
        // Vérifier si les références sont bien assignées
        if (batterySlider == null || sliderFill == null)
        {
            Debug.LogError("Assigne le Slider et l'Image (Fill) via l'inspecteur !");
            return;
        }

        // Initialisation du slider
        UpdateBatteryLevel();
    }

    void Update()
    {
        // Mettre à jour la batterie à chaque frame
        UpdateBatteryLevel();
    }

    private void UpdateBatteryLevel()
    {
        // Récupérer le niveau de batterie (0-100)
        float batteryLevel = SystemInfo.batteryLevel * 100;

        // Si la batterie est inconnue, ne rien faire
        if (batteryLevel < 0)
        {
            Debug.LogWarning("Niveau de batterie non détecté !");
            return;
        }

        // Mettre à jour la valeur du slider
        batterySlider.value = batteryLevel / 100f;

        // Changer la couleur en fonction du niveau de batterie
        if (batteryLevel >= 75)
        {
            sliderFill.color = greenColor;
        }
        else if (batteryLevel >= 50)
        {
            sliderFill.color = yellowColor;
        }
        else if (batteryLevel >= 25)
        {
            sliderFill.color = orangeColor;
        }
        else
        {
            sliderFill.color = redColor;
        }
    }
}