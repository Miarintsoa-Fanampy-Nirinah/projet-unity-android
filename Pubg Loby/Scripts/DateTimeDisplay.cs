using UnityEngine;
using TMPro;

public class DateTimeDisplayAuto : MonoBehaviour
{
    [Header("Assign TextMeshPro Object")]
    [SerializeField] private TextMeshProUGUI textMeshPro;

    private void Update()
    {
        if (textMeshPro != null)
        {
            // Récupérer la date et l'heure locales
            System.DateTime localTime = System.DateTime.Now;

            // Récupérer le fuseau horaire actuel
            System.TimeZoneInfo localTimeZone = System.TimeZoneInfo.Local;

            // Formater la date et l'heure avec le fuseau horaire
            string formattedDateTime = localTime.ToString("dd/MM/yyyy HH:mm") +
                                       $" (UTC{localTimeZone.BaseUtcOffset.Hours:+0;-0}:{Mathf.Abs(localTimeZone.BaseUtcOffset.Minutes):00})";

            // Mettre à jour le texte de TextMeshPro
            textMeshPro.text = formattedDateTime;
        }
        else
        {
            Debug.LogWarning("TextMeshProUGUI n'est pas assigné dans le script DateTimeDisplayAuto.");
        }
    }
}