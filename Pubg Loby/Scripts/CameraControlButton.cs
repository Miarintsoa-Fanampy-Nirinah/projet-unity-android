using UnityEngine;
using UnityEngine.UI;
using System.Collections; // Pour IEnumerator

public class CameraControl : MonoBehaviour
{
    public Camera mainCamera;
    public Transform moveToPosition;
    public Transform originalPosition;
    public Button moveButton;
    public Button returnButton;

    public MonoBehaviour[] scriptsToDisable;

    public float moveDuration = 0.4f;

    private void Start()
    {
        moveButton.onClick.AddListener(() => StartCoroutine(MoveCamera(moveToPosition.position, false)));
        returnButton.onClick.AddListener(() => StartCoroutine(MoveCamera(originalPosition.position, true)));

        if (mainCamera != null && originalPosition != null)
        {
            mainCamera.transform.position = originalPosition.position;
        }
    }

    private IEnumerator MoveCamera(Vector3 targetPosition, bool reenableScripts)
    {
        Vector3 startPosition = mainCamera.transform.position;
        float elapsedTime = 0f;

        if (!reenableScripts)
        {
            foreach (var script in scriptsToDisable)
            {
                if (script != null)
                {
                    script.enabled = false;
                }
            }
        }

        while (elapsedTime < moveDuration)
        {
            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = targetPosition;

        if (reenableScripts)
        {
            foreach (var script in scriptsToDisable)
            {
                if (script != null)
                {
                    script.enabled = true;
                }
            }
        }
    }
}
