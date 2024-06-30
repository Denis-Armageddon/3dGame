using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject inventoryPanel; // Ссылка на панель инвентаря

    private void Start()
    {
        // Скрыть панель инвентаря при старте
        inventoryPanel.SetActive(false);
    }

    private void Update()
    {
        // Проверка нажатия клавиши I
        if (Input.GetKeyDown(KeyCode.I))
        {
            // Переключение видимости панели
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }
}