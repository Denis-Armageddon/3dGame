using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tradeNPC : MonoBehaviour
{
    public int moneyToAdd = 10; // ���������� �����, ������� ����� ��������� ��� �����
    public Inventory inventory;

    public void Awake()
    {
        inventory = GetComponent<Inventory>();
    }
    void OnMouseDown()
    {

        bool success = Inventory.Instance.DeleteInventory(1);
        if (success)
        {
            MoneyManager.Instance.AddMoney(moneyToAdd);
        }

    }
}
