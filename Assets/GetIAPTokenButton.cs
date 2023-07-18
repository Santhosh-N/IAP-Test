using UnityEngine;
using UnityEngine.UI;

public class GetIAPTokenButton : MonoBehaviour
{
    private IAPHandler iapHandler;

    private void Start()
    {
        iapHandler = FindObjectOfType<IAPHandler>();
        GetComponent<Button>().onClick.AddListener(GetIAPToken);
    }

    private void GetIAPToken()
    {
        if (iapHandler != null)
        {
            iapHandler.GetIAPToken();
        }
    }
}