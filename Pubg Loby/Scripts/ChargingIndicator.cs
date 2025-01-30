using UnityEngine;
using UnityEngine.UI;

public class ChargingIndicator : MonoBehaviour
{
    // Références assignables dans l'inspecteur
    [SerializeField] private RawImage chargingIcon;

    private void Start()
    {
        // Vérification si la RawImage est assignée
        if (chargingIcon == null)
        {
            Debug.LogError("L'icône de chargement (Raw Image) n'est pas assignée !");
            return;
        }

        // Initialiser l'état de l'icône
        UpdateChargingStatus();
    }

    private void Update()
    {
        // Mettre à jour l'état de l'icône en continu
        UpdateChargingStatus();
    }

    private void UpdateChargingStatus()
    {
        // Vérifier l'état de la batterie
        BatteryStatus batteryStatus = SystemInfo.batteryStatus;

        // Si le téléphone est branché à un chargeur
        if (batteryStatus == BatteryStatus.Charging || batteryStatus == BatteryStatus.Full)
        {
            chargingIcon.enabled = true; // Afficher l'icône
        }
        else
        {
            chargingIcon.enabled = false; // Masquer l'icône
        }
    }
}