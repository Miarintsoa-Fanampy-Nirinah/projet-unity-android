using System.Collections; // Nécessaire pour IEnumerator
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonSceneLoad : MonoBehaviour
{
    [Header("Assign the Button")]
    [SerializeField] private Button sceneChangeButton;

    [Header("Name of the Scene to Load")]
    [SerializeField] private string sceneName;

    [Header("GameObject to Activate")]
    [SerializeField] private GameObject objectToActivate;

    [Header("Delay Before Scene Change (in seconds)")]
    [SerializeField] private float delayTime = 2f; // Temps modifiable dans l'inspecteur

    private void Awake()
    {
        if (sceneChangeButton == null || string.IsNullOrEmpty(sceneName))
        {
            return;
        }

        sceneChangeButton.onClick.AddListener(ChangeScene);
    }

    private void ChangeScene()
    {
        // Start the process of activating the object and loading the scene
        if (objectToActivate != null)
        {
            StartCoroutine(ActivateObjectAndLoadScene());
        }
        else
        {
            // If no object is assigned, just load the scene directly
            SceneManager.LoadScene(sceneName);
        }
    }

    private IEnumerator ActivateObjectAndLoadScene()
    {
        // Activate the GameObject
        objectToActivate.SetActive(true);

        // Wait for the specified delay time
        yield return new WaitForSeconds(delayTime);

        // Load the scene
        SceneManager.LoadScene(sceneName);
    }
}
