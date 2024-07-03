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

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                if (hit.collider.gameObject == gameObject) // Проверка, что луч попал в этот объект
                {
                    Interact();
                }
            }
        }
    }
    public void Awake()
    {
        inventory = GetComponent<Inventory>();
    }
    void Interact()
    {

        bool success = Inventory.Instance.DeleteInventory(1);
        if (success)
        {
            MoneyManager.Instance.AddMoney(moneyToAdd);
        }

    }
}
