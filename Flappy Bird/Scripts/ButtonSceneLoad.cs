using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonSceneLoad : MonoBehaviour
{
    [Header("Assign the Button")]
    public Button sceneChangeButton;

    [Header("Name of the Scene to Load")]
    public string sceneName;

    void Start()
    {
        if (sceneChangeButton != null)
        {
            sceneChangeButton.onClick.AddListener(ChangeScene);
        }
        else
        {
            Debug.LogWarning("Scene Change Button is not assigned!");
        }
    }

    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is not specified!");
        }
    }
}