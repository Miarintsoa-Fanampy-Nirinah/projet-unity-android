using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ButtonSceneLoader : MonoBehaviour
{
    public string sceneName = "NextScene";
    public Button loadSceneButton;
    public GameObject loadingScreen;

    void Start()
    {
        loadSceneButton.onClick.AddListener(() => StartCoroutine(LoadNextSceneAsync()));
    }

    IEnumerator LoadNextSceneAsync()
    {
        loadingScreen.SetActive(true); // Activer l'écran de chargement

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        loadingScreen.SetActive(false); // Désactiver l'écran de chargement après chargement
    }
}