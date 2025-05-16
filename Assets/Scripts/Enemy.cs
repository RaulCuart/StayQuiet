using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float speed = 5f;
    public Rigidbody2D rb;
    private Vector2 playerPosition;
    private Vector2 movimiento;
    public Animator animator;
    public float range = 1f;
    public monsterType monsterName;
    public PlayerScript playerScript;
    public bool isAlive = true;

    public enum monsterType
    {
        Bat,
        Eye
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        movimiento.x = 0;
        movimiento.y = 0;

        playerPosition = player.transform.position;
        float distance_x = playerPosition.x - transform.position.x;
        float distance_y = playerPosition.y - transform.position.y;
        if (isAlive)
        {
            if (Mathf.Abs(distance_x) <= range)
            {
                movimiento.x = 0;
                animator.SetFloat("movimiento.x", movimiento.x);
            }
            else if (playerPosition.x < transform.position.x)
            {
                movimiento.x = -1;
                animator.SetFloat("movimiento.x", movimiento.x);
            }
            else if (playerPosition.x > transform.position.x)
            {
                movimiento.x = 1;
                animator.SetFloat("movimiento.x", movimiento.x);
            }

            if (Mathf.Abs(distance_y) < range)
            {
                movimiento.y = 0;
                animator.SetFloat("movimiento.y", movimiento.y);
            }
            else if (playerPosition.y < transform.position.y)
            {
                movimiento.y = -1;
                animator.SetFloat("movimiento.y", movimiento.y);
            }
            else if (playerPosition.y > transform.position.y)
            {
                movimiento.y = 1;
                animator.SetFloat("movimiento.y", movimiento.y);
            }
            if (Mathf.Abs(distance_y) <= range && Mathf.Abs(distance_x) <= range)
            {
                StartCoroutine(enemyAttacks());
            }
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movimiento * speed * Time.fixedDeltaTime);
    }
    public IEnumerator enemyAttacks()
    {
        if (!playerScript.isBeingHit && playerScript.isAlive)
        {
            animator.SetBool("isAttacking", true);
            StartCoroutine(cooldownAttack());
            yield return StartCoroutine(playerScript.getHitEffect());
            yield return new WaitForSeconds(2f);
           

        }
    }
    
    IEnumerator cooldownAttack()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("isAttacking", false);
    }
}
