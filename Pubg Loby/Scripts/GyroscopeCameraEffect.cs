using UnityEngine;

public class GyroscopeCameraEffect : MonoBehaviour
{
    [SerializeField]
    private Camera targetCamera; // La caméra à affecter (assignable dans l'Inspector).

    private Vector3 initialPosition;
    public float movementRange = 0.01f; // La plage de mouvement maximale (en unités Unity).
    public float smoothness = 10f; // Pour adoucir le mouvement.
    public float keyboardSensitivity = 1f; // Sensibilité pour les touches directionnelles.

    private bool isGyroSupported = false;

    private void Start()
    {
        // Vérifier si le gyroscope est supporté
        isGyroSupported = SystemInfo.supportsGyroscope;
        if (isGyroSupported)
        {
            Input.gyro.enabled = true;
            Debug.Log("Gyroscope activé.");
        }
        else
        {
            Debug.LogWarning("Gyroscope non supporté sur cet appareil.");
        }

        // Vérifier si une caméra est assignée
        if (targetCamera == null)
        {
            Debug.LogError("Aucune caméra assignée ! Veuillez assigner une caméra dans l'Inspector.");
            enabled = false;
            return;
        }

        // Enregistrer la position initiale de la caméra
        initialPosition = targetCamera.transform.localPosition;
    }

    private void Update()
    {
        Vector3 gyroData = Vector3.zero;

        if (Application.isEditor) // Mode simulation dans l'éditeur.
        {
            // Simuler la rotation du gyroscope avec les touches directionnelles
            float xInput = Input.GetKey(KeyCode.UpArrow) ? 1 : Input.GetKey(KeyCode.DownArrow) ? -1 : 0;
            float yInput = Input.GetKey(KeyCode.RightArrow) ? 1 : Input.GetKey(KeyCode.LeftArrow) ? -1 : 0;

            gyroData = new Vector3(xInput, yInput, 0) * keyboardSensitivity;
        }
        else if (isGyroSupported) // Mode appareil réel avec gyroscope activé.
        {
            gyroData = Input.gyro.rotationRateUnbiased;
        }
        else
        {
            return; // Aucun gyroscope et pas en mode éditeur, ne rien faire.
        }

        // Convertir les données en mouvement (le multiplier pour ajuster la sensibilité).
        float xMovement = Mathf.Clamp(gyroData.y * movementRange, -movementRange, movementRange);
        float yMovement = Mathf.Clamp(-gyroData.x * movementRange, -movementRange, movementRange);

        // Calculer la nouvelle position avec un effet de "smooth".
        Vector3 targetPosition = initialPosition + new Vector3(xMovement, yMovement, 0);
        targetCamera.transform.localPosition = Vector3.Lerp(targetCamera.transform.localPosition, targetPosition, Time.deltaTime * smoothness);
    }
}
