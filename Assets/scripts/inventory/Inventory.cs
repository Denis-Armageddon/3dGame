using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{

    public TextMeshProUGUI quatityText;
    public int quatity = 0;
    

    // Свойство для доступа к экземпляру
    public static Inventory Instance { get; private set; }

    private void Awake()
    {
        // Если экземпляр уже существует и это не текущий экземпляр, уничтожаем текущий экземпляр
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // Устанавливаем текущий экземпляр и сохраняем этот объект между сценами
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void AddInventory(int amount)
    {
        quatity += amount;
        UpdateInventoryUI();
    }

    public bool DeleteInventory(int amount)
    {
        if (quatity > 0) 
        { 
        quatity -= amount;
        UpdateInventoryUI();
            return true;
        }
        return false;
    }
    private void UpdateInventoryUI()
    {
        if (quatity == 0)
        {
        }
        // Обновим текст UI с количеством денег
        if (quatityText != null)
            {
                quatityText.text = quatity.ToString();
            }
         
    }

}




















