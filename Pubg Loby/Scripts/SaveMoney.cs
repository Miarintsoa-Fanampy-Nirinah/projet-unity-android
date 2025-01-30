using UnityEngine;
using TMPro;

public class SaveMoney : MonoBehaviour
{
    public TMP_Text tmpOriginal;

    void Start()
    {
        // Charger l'argent sauvegardé au démarrage
        int savedMoney = PlayerPrefs.GetInt("Money", 0);  // Valeur par défaut = 0 si aucune donnée sauvegardée
        tmpOriginal.text = savedMoney.ToString();
    }

    public void SaveCurrentMoney()
    {
        // Sauvegarder l'argent actuel dans PlayerPrefs
        int currentMoney = int.Parse(tmpOriginal.text);
        PlayerPrefs.SetInt("Money", currentMoney);
        PlayerPrefs.Save();
    }
}
