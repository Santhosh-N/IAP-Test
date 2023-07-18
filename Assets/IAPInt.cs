using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IAPInt : MonoBehaviour
{

    public UIDocument uiDocument;
    public VisualElement root;

    public Button sandButton;

    // Start is called before the first frame update
    void Start()
    {
        root = uiDocument.rootVisualElement.Q<VisualElement>("root");
        sandButton = root.Q<Button>("sandButton");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
