using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickEffectUI : MonoBehaviour
{
    public GameObject particleUIPrefab;
    public Canvas canvas;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Vérifiez si le clic est sur un élément de l'UI.
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return; // Ne pas créer d'effet si le clic est sur un élément de l'UI.
            }

            Vector2 clickPosition = Input.mousePosition; 

            GameObject particleEffect = Instantiate(particleUIPrefab, canvas.transform);
            particleEffect.GetComponent<RectTransform>().position = clickPosition;

            Destroy(particleEffect, 1f);
        }
    }
}
