using UnityEngine;
using System.Collections;

public class ClickableObject : MonoBehaviour
{
    public int moneyToAdd = 10; // ���������� �����, ������� ����� ��������� ��� �����
    public CoroutineManager coroutineManager;
    public int disappearDuration; //��������
    private bool Height = true;


    void OnMouseDown()
    {
        Debug.Log("OnMouseDown called.");
        if (Height == true) { 
        {
        Inventory.Instance.AddInventory(1);
        // ���������, ��� ��������� MoneyManager ����������
        if (MoneyManager.Instance != null )
        {
            // ��������� ������
            MoneyManager.Instance.AddMoney(moneyToAdd);
            Debug.Log("Added " + moneyToAdd + " money on click.");
            // �������� ������� ������������ � ��������� ������� ����� CoroutineManager
            if (coroutineManager != null )
            {
                coroutineManager.StartCoroutine(DisappearAndReappear(this, disappearDuration));
            }
            else
            {
                Debug.LogError("CoroutineManager is not assigned.");
            }
        }
        else
        {
            Debug.LogError("MoneyManager instance is null.");
        }
        }
        }
    }

    public IEnumerator DisappearAndReappear(ClickableObject clickableObject, int delay)
    {
        Height = false;

        // ������ ������ ���������
        clickableObject.gameObject.SetActive(false);

        yield return new WaitForSeconds(delay);

        clickableObject.gameObject.SetActive(true);
        clickableObject.gameObject.transform.localScale = new Vector3(92, 92, 92);
        yield return new WaitForSeconds(delay);

        clickableObject.gameObject.transform.localScale = new Vector3(184, 184, 184);

        yield return new WaitForSeconds(delay);

        clickableObject.gameObject.transform.localScale = new Vector3(276.91f, 276.91f, 276.91f);

        Height = true;


    }
}