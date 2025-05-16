using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DoorDetector : MonoBehaviour
{
    public KeyScript key;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag =="Door")
        {
            Debug.Log("Hay puerta");
            key.foundDoor = true;
        }
    }
    
}
