using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;

public class PlayerScript : MonoBehaviour
{
    public bool isShifting = false;
    public float velocidad = 5f;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    private Vector2 movimiento;
    public Collider2D playerCollider;
    public Animator animator;
    public GameObject flashlight;
    private bool hasSword = false;
    private float normalSpeed;
    public bool isMoving = false;
    public bool isAlive = true;
    public bool isBeingHit = false;


    private bool musicPlayed = false;
    public bool isFalling = false;
    public bool hasFallen = false;
    public GameObject GameManager;

    public AudioSource oof;
    public AudioSource ambientalMusic;
    public AudioSource fallingSound;
    public AudioSource gameOverSound;
    public AudioSource steps;
    private bool flashlightStatus = true;

    public Sprite lookingLeft;
    public Sprite lookingRight;
    public Sprite lookingDown;

    public GameObject attackObjectprefab;
    public Vector2 ultimaDireccion = Vector2.down;

    private void Start()
    {
        normalSpeed = velocidad;
    }

    void Update()
    {
        if (isFalling && !hasFallen)
        {
            animator.enabled = false;
            hasFallen = true;
            isAlive = false;
            StartCoroutine(falls());
        }
        velocidad = normalSpeed;
        isMoving = false;
        isShifting = false;
        animator.SetBool("isMoving", isMoving);
        movimiento.x = 0;
        movimiento.y = 0;
        animator.SetFloat("movimiento.x", movimiento.x);
        animator.SetFloat("movimiento.y", movimiento.y);


        if (Input.GetKey(KeyCode.LeftShift))     
        {
            velocidad = normalSpeed / 2;
            animator.speed = 0.3f;
            isShifting = true;
        } else
        {
            animator.speed = 1f;
        }

        if (Input.GetKey(KeyCode.A) && isAlive)
        {
            movimiento.x = -1;
            animator.SetFloat("movimiento.x", movimiento.x);
            isMoving = true;
            animator.SetBool("isMoving", isMoving);
            ultimaDireccion = Vector2.left;
            flashlight.transform.rotation = Quaternion.Euler(0, 0, 90);
            playStepSound();
        }

        if (Input.GetKey(KeyCode.D) && isAlive)
        {
            movimiento.x = 1;
            animator.SetFloat("movimiento.x", movimiento.x);
            isMoving = true;
            animator.SetBool("isMoving", isMoving);
            ultimaDireccion = Vector2.right;
            flashlight.transform.rotation = Quaternion.Euler(0, 0, 270);
            playStepSound();

        }

        if (Input.GetKey(KeyCode.W) && isAlive)
        {
            movimiento.y = 1;
            animator.SetFloat("movimiento.y", movimiento.y);
            isMoving = true;
            animator.SetBool("isMoving", isMoving);
            ultimaDireccion = Vector2.up;
            flashlight.transform.rotation = Quaternion.Euler(0, 0, 0);
            playStepSound();

        }

        if (Input.GetKey(KeyCode.S) && isAlive)
        {
            movimiento.y = -1;
            animator.SetFloat("movimiento.y", movimiento.y);
            isMoving = true;
            animator.SetBool("isMoving", isMoving);
            ultimaDireccion = Vector2.down;
            flashlight.transform.rotation = Quaternion.Euler(0, 0, 180);
            playStepSound();

        }

        if (Input.GetKeyDown(KeyCode.Space) && hasSword)
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            toggleFlashlight();
        }

        stopStepSound();
    }

    void FixedUpdate()
    {
        if (movimiento.x != 0 && movimiento.y != 0)
        {
            movimiento.Normalize();
        }

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

    private void toggleFlashlight()
    {
        Light2D light = flashlight.GetComponent<Light2D>();
        flashlightStatus = light.intensity > 0;

        if (flashlightStatus)
        {
            light.intensity = 0;
        } 
        else
        {
            light.intensity = 1;
        }
    }

    IEnumerator PlayGameOverSound(float delay)
    {
        yield return new WaitForSeconds(delay);
        
    }

    private void playStepSound()
    {
        if (!steps.isPlaying && !isShifting && isMoving)
        {
            steps.Play();
        }
    }

    private void stopStepSound()
    {
        if (!isMoving && steps.isPlaying || isShifting && steps.isPlaying)
        {
            steps.Stop();
        }
    }

    IEnumerator falls()
    {
        ambientalMusic.Stop();
        yield return new WaitForSeconds(1.5f);
        spriteRenderer.sprite = lookingLeft;
        yield return new WaitForSeconds(1.5f);
        spriteRenderer.sprite = lookingRight;
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.sprite = lookingLeft;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.sprite = lookingRight;
        }
      
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.sprite = lookingDown;
        fallingSound.Play();
        for (int i = 73; i >= 0; i--)
        {
            yield return null;
            float scale = i / 73f;
            transform.localScale = new Vector3(scale,scale,1);
        }
        gameOverSound.Play();
    }

    public bool isLightOn ()
    {
        return this.flashlightStatus;
    }

    public IEnumerator getHitEffect()
    {
        if (!isBeingHit && !hasFallen)
        {
            isBeingHit = true;
            oof.Play();
            spriteRenderer.color = Color.red;
             yield return new WaitForSeconds(0.5f);
            spriteRenderer.color = Color.white;
            for (int i = 0; i < 4; i++)
            {
                 spriteRenderer.color = Color.red;
                 yield return new WaitForSeconds(0.1f);
                 spriteRenderer.color = Color.white;
                 yield return new WaitForSeconds(0.1f);
            }
        }
        yield return new WaitForSeconds(1f);
        isBeingHit = false;
    }
}
