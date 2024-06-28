using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    public TextMeshProUGUI moneyText; 
    private int money;

    void Awake()
    {
        // ��������, ��� � ��� ���� ������ ���� ��������� MoneyManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ��������� ������ ����� �������
        }
        else
        {
            Destroy(gameObject); // ���������� ����������� ���������
        }
    }

    void Start()
    {
        // ����������� ���������� �����
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
        money = Mathf.Max(money - amount, 0); // ��������, ��� ���������� ����� �� ������ � �����
        UpdateMoneyUI();
    }

    public int GetMoney()
    {
        return money;
    }

    private void UpdateMoneyUI()
    {
        // ������� ����� UI � ����������� �����
        if (moneyText != null)
        {
            moneyText.text = "Money: " + money.ToString();
        }
    }
}