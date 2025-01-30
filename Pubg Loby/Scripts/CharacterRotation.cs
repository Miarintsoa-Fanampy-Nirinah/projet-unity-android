using UnityEngine;
using UnityEngine.UI;

public class CharacterRotation : MonoBehaviour
{
    public GameObject targetObject; // Object to rotate
    public RawImage rotationArea; // UI element for the rotation area
    public float rotationSpeed = 100f; // Vitesse de rotation

    private bool isDragging = false; // Pour détecter le drag
    private Vector3 lastMousePosition; // Dernière position de la souris
    private Quaternion targetRotation; // Target rotation

    void Start()
    {
        targetRotation = targetObject.transform.rotation; // Initialize target rotation
    }

    void Update()
    {
        // Check if mouse is over the rotation area
        if (RectTransformUtility.RectangleContainsScreenPoint(rotationArea.rectTransform, Input.mousePosition))
        {
            if (Input.GetMouseButtonDown(0)) // Quand on commence à cliquer
            {
                isDragging = true;
                lastMousePosition = Input.mousePosition; // Stocke la position actuelle
            }
        }

        if (Input.GetMouseButtonUp(0)) // Quand on relâche
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 delta = Input.mousePosition - lastMousePosition; // Différence de mouvement
            float rotationAmount = delta.x * rotationSpeed * Time.deltaTime; // Calcule la rotation
            targetRotation *= Quaternion.Euler(0, -rotationAmount, 0); // Update target rotation
            lastMousePosition = Input.mousePosition; // Met à jour la dernière position
        }

        // Smoothly rotate towards target rotation
        targetObject.transform.rotation = Quaternion.Slerp(targetObject.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
