using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float moveSpeed = 2f;    // Vitesse de déplacement
    public float lifetime = 5f;    // Temps avant destruction

    private void Start()
    {
        // Détruit l'obstacle après un certain temps
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Déplace l'obstacle vers la gauche
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }
}
