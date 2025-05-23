using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;

public class PlayerScript : MonoBehaviour
{
    public bool isShifting = false;
    public float speed = 5f;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    private Vector2 movement;
    public Collider2D playerCollider;
    public Animator animator;
    public GameObject flashlight;
    private float normalSpeed;
    public bool isMoving = false;
    public bool isAlive = true;
    public bool isBeingHit = false;
    public bool isFalling = false;
    public bool hasFallen = false;
    public bool gameisPaused = false;
    public int maxHp = 3;
    public int currentHp;
    
    public AudioSource oof;
    public AudioSource ambientalMusic;
    public AudioSource fallingSound;
    public AudioSource gameOverSound;
    public AudioSource steps;
    public bool flashlightStatus = true;

    public Sprite lookingLeft;
    public Sprite lookingRight;
    public Sprite lookingDown;

    private void Start()
    {
        transform.position = loadCheckpoint();
        normalSpeed = speed;
        currentHp = maxHp;
    }

    void Update()
    {

        if (!isAlive)
        {
            Light2D light = flashlight.GetComponent<Light2D>();
            light.intensity = 0;
        }

        if (isFalling && !hasFallen)
        {
            animator.enabled = false;
            hasFallen = true;
            isAlive = false;
            StartCoroutine(falls());
        }
        speed = normalSpeed;
        isMoving = false;
        isShifting = false;
        animator.SetBool("isMoving", isMoving);
        movement.x = 0;
        movement.y = 0;
        animator.SetFloat("movimiento.x", movement.x);
        animator.SetFloat("movimiento.y", movement.y);


        if (Input.GetKey(KeyCode.LeftShift))     
        {
            speed = normalSpeed / 2;
            animator.speed = 0.3f;
            isShifting = true;
        } else
        {
            animator.speed = 1f;
        }

        if (Input.GetKey(KeyCode.A) && isAlive && !gameisPaused)
        {
            movement.x = -1;
            animator.SetFloat("movimiento.x", movement.x);
            isMoving = true;
            animator.SetBool("isMoving", isMoving);
            flashlight.transform.rotation = Quaternion.Euler(0, 0, 90);
            playStepSound();
        }

        if (Input.GetKey(KeyCode.D) && isAlive && !gameisPaused)
        {
            movement.x = 1;
            animator.SetFloat("movimiento.x", movement.x);
            isMoving = true;
            animator.SetBool("isMoving", isMoving);
            flashlight.transform.rotation = Quaternion.Euler(0, 0, 270);
            playStepSound();

        }

        if (Input.GetKey(KeyCode.W) && isAlive && !gameisPaused)
        {
            movement.y = 1;
            animator.SetFloat("movimiento.y", movement.y);
            isMoving = true;
            animator.SetBool("isMoving", isMoving);
            flashlight.transform.rotation = Quaternion.Euler(0, 0, 0);
            playStepSound();

        }

        if (Input.GetKey(KeyCode.S) && isAlive && !gameisPaused)
        {
            movement.y = -1;
            animator.SetFloat("movimiento.y", movement.y);
            isMoving = true;
            animator.SetBool("isMoving", isMoving);
            flashlight.transform.rotation = Quaternion.Euler(0, 0, 180);
            playStepSound();

        }

        if (Input.GetKeyDown(KeyCode.F) && !gameisPaused)
        {
            toggleFlashlight();
        }

        stopStepSound();

        if (currentHp == 0 && isAlive)
        {
            isAlive = false;
            StartCoroutine(dies());
        }


    }

    void FixedUpdate()
    {
        if (movement.x != 0 && movement.y != 0)
        {
            movement.Normalize();
        }

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
    private void toggleFlashlight()
    {
        if (isAlive)
        {
            Light2D light = flashlight.GetComponent<Light2D>();

            if (light.intensity > 0)
            {
                light.intensity = 0;
                flashlightStatus = false;
            }
            else
            {
                light.intensity = 1;
                flashlightStatus = true;
            }
        }
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

    public void setCheckpoint(Vector2 checkpoint)
    {

        PlayerPrefs.SetFloat("posX", checkpoint.x);
        PlayerPrefs.SetFloat("posY", checkpoint.y);
        PlayerPrefs.Save();
    }

    private Vector2 loadCheckpoint()
    {
        if (PlayerPrefs.HasKey("posX"))
        {
            float posX = PlayerPrefs.GetFloat("posX");
            float posY = PlayerPrefs.GetFloat("posY");
            Vector2 checkpointPos = new Vector2(posX, posY);
            return checkpointPos;
        }
        return new Vector2(0,-4);

    }

    IEnumerator dies()
    {
        for (int i = 0; i < 45; i++)
        {
            transform.rotation = Quaternion.Euler(0, 0, i*2);
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0, 0, 90);
        gameOverSound.Play();

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
        int fallSpeed = 50;
        for (int i = fallSpeed; i >= 0; i--)
        {
            float scale = i / 50f;
            transform.localScale = new Vector3(scale,scale,1);
            yield return new WaitForSeconds(0.035f);
        }
        gameOverSound.Play();
    }

    public IEnumerator getHitEffect()
    {
        if (!isBeingHit && !hasFallen && isAlive)
        {
            isBeingHit = true;
            oof.Play();
            currentHp--;
            Debug.Log(currentHp);
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
