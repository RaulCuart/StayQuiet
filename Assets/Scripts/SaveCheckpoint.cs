using Unity.VisualScripting;
using UnityEngine;

public class SaveCheckpoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Checkpoint seteado");
            Vector2 checkpointPosition = new Vector2(transform.position.x,transform.position.y);
            GameObject playerObject = collision.gameObject;
            PlayerScript player = playerObject.GetComponent<PlayerScript>();
            player.setCheckpoint(checkpointPosition);
            
        }
    }
}
