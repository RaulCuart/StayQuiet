using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public GameObject aura;
    public float speed = 5f;
    public Rigidbody2D rb;
    private Vector2 playerPosition;
    private Vector2 movimiento;
    public bool playerInRange;
    public Animator animator;
    public float range = 1f;
    public monsterType monsterName;
    public PlayerScript playerScript;
    public bool isAlive = true;
    private Vector2 lastPlayerPosition;
    public bool movesToLastKnownPos = false;

    public enum monsterType
    {
        Bat,
        Eye
    }

    void Start()
    {
        Vector2 startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!movesToLastKnownPos)
        {
            movimiento.x = 0;
            movimiento.y = 0;
        }


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
            else if (playerPosition.x < transform.position.x && playerInRange)
            {
                movimiento.x = -1;
                animator.SetFloat("movimiento.x", movimiento.x);
                lastPlayerPosition = playerPosition;
            }
            else if (playerPosition.x > transform.position.x && playerInRange)
            {
                movimiento.x = 1;
                animator.SetFloat("movimiento.x", movimiento.x);
                lastPlayerPosition = playerPosition;
            }

            if (Mathf.Abs(distance_y) < range)
            {
                movimiento.y = 0;
                animator.SetFloat("movimiento.y", movimiento.y);
            }
            else if (playerPosition.y < transform.position.y && playerInRange)
            {
                movimiento.y = -1;
                animator.SetFloat("movimiento.y", movimiento.y);
                lastPlayerPosition = playerPosition;
            }
            else if (playerPosition.y > transform.position.y && playerInRange)
            {
                movimiento.y = 1;
                animator.SetFloat("movimiento.y", movimiento.y);
                lastPlayerPosition = playerPosition;
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
        if (!playerScript.isBeingHit && playerScript.isAlive && !playerScript.isHidden)
        {
            animator.SetBool("isAttacking", true);
            StartCoroutine(cooldownAttack());
            yield return StartCoroutine(playerScript.getHitEffect());
            yield return new WaitForSeconds(2f);
           
        }
    }

    public void OnTriggerExit2D(Collider2D other )
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected");
            playerInRange = false;


        }

    }

    IEnumerator cooldownAttack()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("isAttacking", false);
    }
    
    public void startsMoveToLastKnownPos()
    {
        if (!movesToLastKnownPos)
        {
            Debug.Log("Starting coroutine");
            StartCoroutine(moveToLastKnownPos());
        }
    }
    public void stopMoveTolastKnownpos()
    {
        StopCoroutine(moveToLastKnownPos());
    }
    
    IEnumerator moveToLastKnownPos()
    {
        Debug.Log("Coroutine started");
        movesToLastKnownPos = true;
        float distance_x = lastPlayerPosition.x - transform.position.x;
        float distance_y = lastPlayerPosition.y - transform.position.y;
        float timer = 0f;
        float maxTime = 2f;

         while (timer < maxTime)
         {
            if (Mathf.Abs(distance_x) <= range)
            {
                movimiento.x = 0;
                animator.SetFloat("movimiento.x", movimiento.x);
            }
            else if (lastPlayerPosition.x < transform.position.x && playerInRange)
            {
                movimiento.x = -1;
                animator.SetFloat("movimiento.x", movimiento.x);
            }
            else if (lastPlayerPosition.x > transform.position.x && playerInRange)
            {
                movimiento.x = 1;
                animator.SetFloat("movimiento.x", movimiento.x);
            }

            if (Mathf.Abs(distance_y) < range)
            {
                movimiento.y = 0;
                animator.SetFloat("movimiento.y", movimiento.y);
            }
            else if (lastPlayerPosition.y < transform.position.y && playerInRange)
            {
                movimiento.y = -1;
                animator.SetFloat("movimiento.y", movimiento.y);
            }
            else if (lastPlayerPosition.y > transform.position.y && playerInRange)
            {
                movimiento.y = 1;
                animator.SetFloat("movimiento.y", movimiento.y);

            }
            rb.MovePosition(rb.position + movimiento * speed * Time.fixedDeltaTime);
            yield return null;
            distance_x = lastPlayerPosition.x - transform.position.x;
            distance_y = lastPlayerPosition.y - transform.position.y;
            timer += Time.fixedDeltaTime;

         }
        movesToLastKnownPos = false;
        Debug.Log("Coroutine ends");
    }
}
