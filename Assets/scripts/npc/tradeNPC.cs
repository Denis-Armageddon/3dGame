using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tradeNPC : MonoBehaviour
{
    public int moneyToAdd = 10; // ���������� �����, ������� ����� ��������� ��� �����
    public Inventory inventory;

    public float interactionDistance = 3f;
    public Camera cameraPlayer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(cameraPlayer.transform.position, cameraPlayer.transform.forward);
            RaycastHit hit;

            Debug.Log("������� E ������. ����������� ������� ������...");

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                Debug.Log("��� ����� � ������: " + hit.collider.gameObject.name);

                if (hit.collider.gameObject == gameObject) // ��������, ��� ��� ����� � ���� ������
                {
                    Debug.Log("��� ����� � ��������� NPC. ���������� ��������������...");
                    Interact();
                }
                else
                {
                    Debug.Log("��� ����� � ������ ������, �� ��������� NPC.");
                }
            }
            else
            {
                Debug.Log("��� �� ����� �� � ���� ������ � �������� ��������� ��������������.");
            }
        }
    }

    public void Awake()
    {
        inventory = GetComponent<Inventory>();
        if (inventory != null)
        {
            Debug.Log("��������� Inventory ������ �� �������� NPC.");
        }
        else
        {
            Debug.LogError("��������� Inventory �� ������ �� �������� NPC.");
        }
    }

    void Interact()
    {
        Debug.Log("������� ������� ������� �� ���������...");

        bool success = Inventory.Instance.DeleteInventory(1);

        if (success)
        {
            Debug.Log("������� ������� ������ �� ���������. ��������� ������...");
            MoneyManager.Instance.AddMoney(moneyToAdd);
        }
        else
        {
            Debug.LogWarning("�� ������� ������� ������� �� ���������. ������ �� ���������.");
        }
    }
}