using System.Collections;
using UnityEngine;

public class KeyScript : MonoBehaviour
{

    public GameObject doors;
    public Transform playerPosition;
    public bool foundDoor = false;
    private bool isPickedUp = false;
    private bool isPickedDown = false;
    public int moveSpeed = 5;
    public Vector2 targetPosition;
    public AudioSource openDoorSound;

    void Update()
    {
        if (foundDoor && !isPickedDown)
        {
            isPickedDown = true;
            StartCoroutine(goToDoor());
        }


    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isPickedUp)
        {
            isPickedUp = true;
            StartCoroutine(followPlayer());
        }
    }

    IEnumerator followPlayer()
    {   
        while (isPickedUp && !foundDoor)
        {       
                targetPosition = playerPosition.position;
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
                Debug.Log("Llave recogida.");  
        }
    }
    IEnumerator goToDoor()
    {
        targetPosition = doors.transform.position;
        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, 2 * Time.deltaTime);
            Debug.Log("Moving");
            yield return null;
            if (transform.position.Equals(targetPosition))
            {
                break;
            }
        }
        openDoorSound.Play();
        yield return new WaitForSeconds(2f);
        doors.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
