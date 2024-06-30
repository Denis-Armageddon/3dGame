using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{

    public TextMeshProUGUI quatityText;
    public int quatity = 0;
    

    // �������� ��� ������� � ����������
    public static Inventory Instance { get; private set; }

    private void Awake()
    {
        // ���� ��������� ��� ���������� � ��� �� ������� ���������, ���������� ������� ���������
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // ������������� ������� ��������� � ��������� ���� ������ ����� �������
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
        // ������� ����� UI � ����������� �����
        if (quatityText != null)
            {
                quatityText.text = quatity.ToString();
            }
         
    }

}




















