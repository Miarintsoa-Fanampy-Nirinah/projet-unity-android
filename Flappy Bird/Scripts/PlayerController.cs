using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool isAlive = true;

    [Header("Game Over Settings")]
    public AudioClip collisionClip;
    public AudioClip jumpClip;

    [Header("Rotation Settings")]
    public float rotationSpeed = 5f; // Vitesse de la rotation, modifiable depuis l'Inspector
    public float maxUpRotation = 70f; // Rotation maximale lors du saut (vers le haut)
    public float maxDownRotation = -70f; // Rotation maximale lors de la chute (vers le bas)

    private AudioSource audioSource;
    private GameManager gameManager;
    private Camera mainCamera;

    private Animator animator; // Référence à l'Animator

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        mainCamera = Camera.main;
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>(); // Récupérer l'Animator du joueur
    }

    private void Update()
    {
        if (isAlive && IsInCameraView() && Input.GetMouseButtonDown(0))
        {
            Jump();
        }

        // Ajuster la rotation en fonction de la vitesse verticale
        if (isAlive)
        {
            AdjustRotation();
        }
    }

    private bool IsInCameraView()
    {
        if (mainCamera == null) return false;

        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);
        return viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1;
    }

    private void Jump()
    {
        rb.linearVelocity = Vector2.up * jumpForce; // Correction : rb.velocity au lieu de rb.linearVelocity
        if (jumpClip != null) audioSource.PlayOneShot(jumpClip);
    }

    private void AdjustRotation()
    {
        // Calcule une rotation basée sur la vitesse verticale
        float angle = Mathf.Lerp(maxDownRotation, maxUpRotation, (rb.linearVelocity.y + 10f) / 20f); // -70° à 70°, avec une plage de vitesse entre -10 et 10
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * rotationSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Ground"))
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        if (!isAlive) return;

        isAlive = false;
        if (collisionClip != null) audioSource.PlayOneShot(collisionClip);

        // Activer le paramètre 'Dead' dans l'Animator
        if (animator != null)
        {
            animator.SetBool("Dead", true); // Change le paramètre Dead en vrai
        }

        StartCoroutine(RotatePlayer());
        StopGame();
    }

    private void StopGame()
    {
        enabled = false;
        gameManager?.GameOver();
    }

    private IEnumerator RotatePlayer()
    {
        float targetRotation = -90f;
        while (Mathf.Abs(transform.eulerAngles.z - targetRotation) > 0.1f)
        {
            float newZRotation = Mathf.LerpAngle(transform.eulerAngles.z, targetRotation, Time.deltaTime * rotationSpeed);
            transform.rotation = Quaternion.Euler(0, 0, newZRotation);
            yield return null;
        }
    }
}