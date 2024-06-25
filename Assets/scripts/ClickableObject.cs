using UnityEngine;
using System.Collections;

public class ClickableObject : MonoBehaviour
{
    public int moneyToAdd = 10; // ���������� �����, ������� ����� ��������� ��� �����
    public CoroutineManager coroutineManager;
    public int disappearDuration; //��������

    void OnMouseDown()
    {
        Debug.Log("OnMouseDown called.");

        // ���������, ��� ��������� MoneyManager ����������
        if (MoneyManager.Instance != null)
        {
            // ��������� ������
            MoneyManager.Instance.AddMoney(moneyToAdd);
            Debug.Log("Added " + moneyToAdd + " money on click.");

            // �������� ������� ������������ � ��������� ������� ����� CoroutineManager
            if (coroutineManager != null)
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

    public static IEnumerator DisappearAndReappear(ClickableObject clickableObject, int delay)
    {

        // ������ ������ ���������
        clickableObject.gameObject.SetActive(false);

        // ���� 10 ������
        yield return new WaitForSeconds(delay);
        // ���������, �� ��� �� ������ ���������

        if (clickableObject != null)
        {
            clickableObject.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Object was destroyed before reappearing.");
        }
    }
}