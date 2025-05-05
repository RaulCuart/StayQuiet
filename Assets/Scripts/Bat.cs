using System.Collections;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public GameObject player;
    public float speed = 5f;
    public Rigidbody2D rb;
    private Vector2 playerPosition;
    private Vector2 movimiento;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position;
        if (playerPosition.x < transform.position.x)
        {
            movimiento.x = -1;
        }
        if (playerPosition.x > transform.position.x)
        {
            movimiento.x = 1;
        }
        if (playerPosition.x == transform.position.x)
        {
            movimiento.x = 0;
        }
        if (playerPosition.y < transform.position.y)
        {
            movimiento.y = -1;
        }
        if (playerPosition.y > transform.position.y)
        {
            movimiento.y = 1;
        }
        if (playerPosition.y == transform.position.y)
        {
            movimiento.y = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movimiento * speed * Time.fixedDeltaTime);
    }
    public IEnumerator moveToPlayer(Transform targetPosition)
    {


        yield return null;

    }
}
