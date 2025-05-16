using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform playerPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 playerPos = new Vector3(playerPosition.position.x, playerPosition.position.y,-10);
        transform.position = playerPos;
    }
}
