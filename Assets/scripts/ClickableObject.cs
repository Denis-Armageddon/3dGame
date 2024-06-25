using UnityEngine;
using System.Collections;

public class ClickableObject : MonoBehaviour
{
    public int moneyToAdd = 10; // Количество денег, которое будет начислено при клике
    public CoroutineManager coroutineManager;
    public int disappearDuration; //задержка

    void OnMouseDown()
    {
        Debug.Log("OnMouseDown called.");

        // Проверяем, что экземпляр MoneyManager существует
        if (MoneyManager.Instance != null)
        {
            // Начисляем деньги
            MoneyManager.Instance.AddMoney(moneyToAdd);
            Debug.Log("Added " + moneyToAdd + " money on click.");

            // Начинаем процесс исчезновения и появления объекта через CoroutineManager
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

    public static IEnumerator DisappearAndReappear(ClickableObject clickableObject, int delay)
    {

        // Делаем объект невидимым
        clickableObject.gameObject.SetActive(false);

        // Ждем 10 секунд
        yield return new WaitForSeconds(delay);
        // Проверяем, не был ли объект уничтожен

        if (clickableObject != null)
        {
            clickableObject.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Object was destroyed before reappearing.");
        }
    }
}