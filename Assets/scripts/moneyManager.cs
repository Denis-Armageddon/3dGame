using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    public TextMeshProUGUI moneyText; 
    private int money;

    void Awake()
    {
        // ”бедимс€, что у нас есть только один экземпл€р MoneyManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // —охран€ем объект между сценами
        }
        else
        {
            Destroy(gameObject); // ”ничтожаем дублирующий экземпл€р
        }
    }

    void Start()
    {
        // »значальное количество денег
        money = 0;
        UpdateMoneyUI();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyUI();
    }
    public void deleteMoney(int amount)
    {
        money -= amount;
        UpdateMoneyUI();
    }

    public void SubtractMoney(int amount)
    {
        money = Mathf.Max(money - amount, 0); // ”бедимс€, что количество денег не уходит в минус
        UpdateMoneyUI();
    }

    public int GetMoney()
    {
        return money;
    }

    private void UpdateMoneyUI()
    {
        // ќбновим текст UI с количеством денег
        if (moneyText != null)
        {
            moneyText.text = "Money: " + money.ToString();
        }
    }
}