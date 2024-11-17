using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tradeNPC : MonoBehaviour
{
    public int moneyToAdd = 10; // Количество денег, которое будет начислено при клике
    public Inventory inventory;

    public float interactionDistance = 3f;
    public Camera cameraPlayer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(cameraPlayer.transform.position, cameraPlayer.transform.forward);
            RaycastHit hit;

            Debug.Log("Клавиша E нажата. Выполняется лучевой запрос...");

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                Debug.Log("Луч попал в объект: " + hit.collider.gameObject.name);

                if (hit.collider.gameObject == gameObject) // Проверка, что луч попал в этот объект
                {
                    Debug.Log("Луч попал в торгового NPC. Начинается взаимодействие...");
                    Interact();
                }
                else
                {
                    Debug.Log("Луч попал в другой объект, не торгового NPC.");
                }
            }
            else
            {
                Debug.Log("Луч не попал ни в один объект в пределах дистанции взаимодействия.");
            }
        }
    }

    public void Awake()
    {
        inventory = GetComponent<Inventory>();
        if (inventory != null)
        {
            Debug.Log("Компонент Inventory найден на торговом NPC.");
        }
        else
        {
            Debug.LogError("Компонент Inventory не найден на торговом NPC.");
        }
    }

    void Interact()
    {
        Debug.Log("Попытка удалить предмет из инвентаря...");

        bool success = Inventory.Instance.DeleteInventory(1);

        if (success)
        {
            Debug.Log("Предмет успешно удален из инвентаря. Добавляем деньги...");
            MoneyManager.Instance.AddMoney(moneyToAdd);
        }
        else
        {
            Debug.LogWarning("Не удалось удалить предмет из инвентаря. Деньги не добавлены.");
        }
    }
}