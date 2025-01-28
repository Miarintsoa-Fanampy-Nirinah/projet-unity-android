using System.Collections; // Pour IEnumerator
using UnityEngine;

public class StartSynchronization : MonoBehaviour
{
    [Header("GameObject to Synchronize")]
    [SerializeField] private GameObject targetObject;

    [Header("Time to Display the Object (in seconds)")]
    [SerializeField] private float displayTime = 2f;

    private void Start()
    {
        if (targetObject != null)
        {
            StartCoroutine(SynchronizeObject());
        }
        else
        {
            Debug.LogWarning("No GameObject assigned to ObjectSynchronization script!");
        }
    }

    private IEnumerator SynchronizeObject()
    {
        // Activer le GameObject
        targetObject.SetActive(true);

        // Attendre le temps défini
        yield return new WaitForSeconds(displayTime);

        // Désactiver le GameObject
        targetObject.SetActive(false);
    }
}
