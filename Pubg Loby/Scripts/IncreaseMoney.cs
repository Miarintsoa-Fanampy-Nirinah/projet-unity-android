using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IncreaseMoney : MonoBehaviour
{
    public TMP_Text tmpOriginal; // Référence au TMP original (argent)
    public TMP_Text[] tmpAmounts; // Tableau des TMP qui contiennent les valeurs à ajouter
    public Button[] increaseButtons; // Tableau des boutons pour activer la transaction

    private int amountToIncrease; // Valeur à augmenter (extraction des TMP)

    void Start()
    {
        // Vérifiez que le nombre de boutons et de valeurs correspond
        if (increaseButtons.Length == tmpAmounts.Length)
        {
            // Ajoutez les écouteurs de clic pour chaque bouton
            for (int i = 0; i < increaseButtons.Length; i++)
            {
                int index = i; // Variable locale pour éviter les problèmes de fermeture dans le listener
                increaseButtons[i].onClick.AddListener(() => OnIncreaseButtonClick(index));
            }
        }
        else
        {
            Debug.LogError("Le nombre de boutons et de TMPAmounts ne correspond pas !");
        }
    }

    void OnIncreaseButtonClick(int index)
    {
        // Extraire la valeur du TMPAmount correspondant au bouton
        if (int.TryParse(tmpAmounts[index].text, out amountToIncrease))
        {
            int newMoneyValue = int.Parse(tmpOriginal.text) + amountToIncrease;
            tmpOriginal.text = newMoneyValue.ToString();
        }
    }
}
