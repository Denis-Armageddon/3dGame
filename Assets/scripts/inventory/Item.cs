using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class inventoryMain : MonoBehaviour
{
    public int quantity; // Количество предметов
    public TextMeshProUGUI quantityText;

    private void Update()
    {
        quantityText.text =  quantity.ToString();
        Debug.Log(quantity);
    }
}
