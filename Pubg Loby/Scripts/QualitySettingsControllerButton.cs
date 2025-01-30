using UnityEngine;
using UnityEngine.UI;

public class QualitySettingsController : MonoBehaviour
{
    public Button[] qualityButtons;

    void Start()
    {
        for (int i = 0; i < qualityButtons.Length; i++)
        {
            int qualityIndex = i;
            qualityButtons[i].onClick.AddListener(() => SetQuality(qualityIndex));
        }
    }

    void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        Debug.Log("Quality Level Set to: " + QualitySettings.names[index]);
    }
}
