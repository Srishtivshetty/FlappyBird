using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdJumper : MonoBehaviour
{
    [SerializeField] private Button button;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start called.");
        button.onClick.AddListener(Clicked);
    }

    void Clicked()
    {
        Debug.Log("Button was clicked");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}