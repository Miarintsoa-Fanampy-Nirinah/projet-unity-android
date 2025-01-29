using UnityEngine;
using UnityEngine.UI;

public class OpenWebsite : MonoBehaviour
{
    public Button targetButton;
    [SerializeField] private string url = "https://miarintsoa-fanampy-nirinah.github.io/";

    void Start()
    {
        if (targetButton != null)
        {
            targetButton.onClick.AddListener(OpenURL);
        }
    }

    private void OpenURL()
    {
        Application.OpenURL(url);
    }
}
