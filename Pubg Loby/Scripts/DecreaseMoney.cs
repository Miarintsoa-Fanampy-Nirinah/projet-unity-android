using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DecreaseMoney : MonoBehaviour
{
    public TMP_Text tmpOriginal; // Référence au TMP original (argent)
    public TMP_Text[] tmpAmounts; // Tableau des TMP qui contiennent les valeurs à diminuer
    public Button[] decreaseButtons; // Tableau des boutons pour activer la transaction

    private int amountToDecrease; // Valeur à diminuer (extraction des TMP)

    void Start()
    {
        // Vérifiez que le nombre de boutons et de valeurs correspond
        if (decreaseButtons.Length == tmpAmounts.Length)
        {
            // Ajoutez les écouteurs de clic pour chaque bouton
            for (int i = 0; i < decreaseButtons.Length; i++)
            {
                int index = i; // Variable locale pour éviter les problèmes de fermeture dans le listener
                decreaseButtons[i].onClick.AddListener(() => OnDecreaseButtonClick(index));
            }
        }
        else
        {
            Debug.LogError("Le nombre de boutons et de TMPAmounts ne correspond pas !");
        }
    }

    void OnDecreaseButtonClick(int index)
    {
        // Extraire la valeur du TMPAmount correspondant au bouton
        if (int.TryParse(tmpAmounts[index].text, out amountToDecrease))
        {
            // Assurez-vous que l'argent ne devient pas négatif
            int newMoneyValue = Mathf.Max(0, int.Parse(tmpOriginal.text) - amountToDecrease);
            tmpOriginal.text = newMoneyValue.ToString();
        }
    }
}
