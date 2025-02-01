using UnityEngine;

public class PersistentAudio : MonoBehaviour
{
    private void Awake()
    {
        // Vérifie s'il existe déjà un objet de ce type dans la scène
        if (FindObjectsOfType<PersistentAudio>().Length > 1)
        {
            Destroy(gameObject); // Si un autre existe déjà, détruire cet objet
            return;
        }

        // Rend cet objet persistant entre les scènes
        DontDestroyOnLoad(gameObject);
    }
}