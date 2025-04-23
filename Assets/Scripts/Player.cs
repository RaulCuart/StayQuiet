using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float velocidad = 5f;
    public Rigidbody2D rb;
    private Vector2 movimiento;
    public Collider2D playerCollider;

    public Sprite[] spritesAbajo;
    public Sprite[] spritesArriba;
    public Sprite[] spritesIzquierda;
    public Sprite[] spritesDerecha;
    private Sprite[] animacionActual;

    public float animationTime = 0.2f;
    private SpriteRenderer spriteRenderer;
    private int animationFrame;
    private bool estaCaminando = false;

    private Sprite ultimoSprite;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animacionActual = spritesAbajo; 
        ultimoSprite = animacionActual[0]; 
    }

    void Update()
    {

        movimiento.x = 0;
        movimiento.y = 0;
        estaCaminando = false;

        if (Input.GetKey(KeyCode.A))
        {
            movimiento.x = -1;
            estaCaminando = true;
            animacionActual = spritesIzquierda;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movimiento.x = 1;
            estaCaminando = true;
            animacionActual = spritesDerecha;
        }

        if (Input.GetKey(KeyCode.W))
        {
            movimiento.y = 1;
            estaCaminando = true;
            animacionActual = spritesArriba;
        }

        if (Input.GetKey(KeyCode.S))
        {
            movimiento.y = -1;
            estaCaminando = true;
            animacionActual = spritesAbajo;
        }

        //Para que no camine mas rapido de forma diagonal
        movimiento = movimiento.normalized;

        if (estaCaminando)
        {
            AnimateSprite();
        }
        else
        {
            animationFrame = 0;
            spriteRenderer.sprite = ultimoSprite;
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

    private void AnimateSprite()
    {
        animationFrame++;

        if (animationFrame >= animacionActual.Length)
        {
            animationFrame = 0;
        }

        spriteRenderer.sprite = animacionActual[animationFrame];
        ultimoSprite = spriteRenderer.sprite; 
    }
}
