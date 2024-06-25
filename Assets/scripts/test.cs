using UnityEngine;

public class SomeOtherScript : MonoBehaviour
{
    void WTF()
    {      
        if (MoneyManager.Instance != null)
        {
            MoneyManager.Instance.AddMoney(100);
        }
    }
}