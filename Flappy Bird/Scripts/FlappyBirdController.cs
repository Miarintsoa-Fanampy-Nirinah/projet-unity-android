using System.Collections;
using UnityEngine;

public class FlappyBirdController : MonoBehaviour
{
    public float jumpForce = 5f;             
    private Rigidbody2D rb;                 
    private bool isAlive = true;            

    [Header("Game Over Settings")]
    public AudioClip collisionClip;         
    public AudioClip jumpClip;         
    public float rotationSpeed = 2f;     
    private AudioSource audioSource;     
    private GameManager gameManager;        

    private Camera mainCamera;              

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        mainCamera = Camera.main; // Récupérer la caméra principale
        
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (isAlive && IsInCameraView() && Input.GetMouseButtonDown(0)) // Saut sur clic
        {
            Jump();
        }
    }

    private bool IsInCameraView()
    {
        if (mainCamera != null)
        {
            Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);

            // Vérifier si le joueur est dans le champ de vision de la caméra
            return viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1;
        }
        return false;
    }

    private void Jump()
    {
        rb.linearVelocity = Vector2.up * jumpForce;

        if (jumpClip != null)
        {
            audioSource.PlayOneShot(jumpClip);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameOver();
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        isAlive = false;

        if (collisionClip != null)
        {
            audioSource.PlayOneShot(collisionClip);
        }

        StartCoroutine(RotatePlayer());

        StopGame();
    }

    private void StopGame()
    {
        this.enabled = false; 

        if (gameManager != null)
        {
            gameManager.GameOver();
        }
    }

    private IEnumerator RotatePlayer()
    {
        float targetRotation = -90f; 
        while (Mathf.Abs(transform.rotation.eulerAngles.z - targetRotation) > 0.1f)
        {
            float newZRotation = Mathf.LerpAngle(transform.rotation.eulerAngles.z, targetRotation, Time.deltaTime * rotationSpeed);
            transform.rotation = Quaternion.Euler(0, 0, newZRotation);
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y); // Maintient la gravité active
        }
    }
}
