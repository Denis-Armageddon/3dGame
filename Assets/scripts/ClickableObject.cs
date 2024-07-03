using UnityEngine;
using System.Collections;

public class ClickableObject : MonoBehaviour
{
    public CoroutineManager coroutineManager;
    public int disappearDuration; // задержка
    private bool Height = true;

    public float interactionDistance = 3f;
    public Camera cameraPlayer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(cameraPlayer.transform.position, cameraPlayer.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                if (hit.collider.gameObject == gameObject) // Проверка, что луч попал в этот объект
                {
                    Interact();
                }
            }
        }
    }

    void Interact()
    {
        Debug.Log("Interact called.");
        if (Height)
        {
            Inventory.Instance.AddInventory(1);

            if (MoneyManager.Instance != null)
            {
                if (coroutineManager != null)
                {
                    coroutineManager.StartCoroutine(DisappearAndReappear(this, disappearDuration));
                }
                else
                {
                    Debug.LogError("CoroutineManager is not assigned.");
                }
            }
            else
            {
                Debug.LogError("MoneyManager instance is null.");
            }
        }
    }

    public IEnumerator DisappearAndReappear(ClickableObject clickableObject, int delay)
    {
        Height = false;

        // Делаем объект невидимым
        clickableObject.gameObject.SetActive(false);

        yield return new WaitForSeconds(delay);

        clickableObject.gameObject.SetActive(true);
        clickableObject.gameObject.transform.localScale = new Vector3(92, 92, 92);

        yield return new WaitForSeconds(delay);

        clickableObject.gameObject.transform.localScale = new Vector3(184, 184, 184);

        yield return new WaitForSeconds(delay);

        clickableObject.gameObject.transform.localScale = new Vector3(276.91f, 276.91f, 276.91f);

        Height = true;
    }
}