using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float velocidad = 5f;
    public Rigidbody2D rb;
    private Vector2 movimiento;
    public Collider2D playerCollider;
    public Animator animator;
    private bool hasSword = false;
    public bool isMoving = false;

    public GameObject attackObjectprefab;
    public Vector2 ultimaDireccion = Vector2.down;



    void Update()
    {
        isMoving = false;
        animator.SetBool("isMoving", isMoving);
        movimiento.x = 0;
        movimiento.y = 0;
        animator.SetFloat("movimiento.x", movimiento.x);
        animator.SetFloat("movimiento.y", movimiento.y);


        if (Input.GetKey(KeyCode.A))
        {
            movimiento.x = -1;
            animator.SetFloat("movimiento.x", movimiento.x);
            isMoving = true;
            animator.SetBool("isMoving", isMoving);
            ultimaDireccion = Vector2.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movimiento.x = 1;
            animator.SetFloat("movimiento.x", movimiento.x);
            isMoving = true;
            animator.SetBool("isMoving", isMoving);
            ultimaDireccion = Vector2.right;
        }

        if (Input.GetKey(KeyCode.W))
        {
            movimiento.y = 1;
            animator.SetFloat("movimiento.y", movimiento.y);
            isMoving = true;
            animator.SetBool("isMoving", isMoving);
            ultimaDireccion = Vector2.up;
        }

        if (Input.GetKey(KeyCode.S))
        {
            movimiento.y = -1;
            animator.SetFloat("movimiento.y", movimiento.y);
            isMoving = true;
            animator.SetBool("isMoving", isMoving);
            ultimaDireccion = Vector2.down;

        }

        if (Input.GetKeyDown(KeyCode.Space) && hasSword)
        {
            Attack();
        }
    }

    void FixedUpdate()
    {

        rb.MovePosition(rb.position + movimiento * velocidad * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sword"))
        {
            hasSword = true;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Attack()
    {
        Vector2 posicionAtaque = (Vector2)transform.position + ultimaDireccion;
        Instantiate(attackObjectprefab, posicionAtaque, Quaternion.identity);

    }

}
