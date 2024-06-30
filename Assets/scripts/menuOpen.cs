using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject inventoryPanel; // ������ �� ������ ���������

    private void Start()
    {
        // ������ ������ ��������� ��� ������
        inventoryPanel.SetActive(false);
    }

    private void Update()
    {
        // �������� ������� ������� I
        if (Input.GetKeyDown(KeyCode.I))
        {
            // ������������ ��������� ������
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }
}