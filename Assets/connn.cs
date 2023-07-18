using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class connn : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        gameObject.GetComponent<IAPButton>().productId = "com.vdogames.iaptest.coin500";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
