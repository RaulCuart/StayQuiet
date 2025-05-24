using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PitScript : MonoBehaviour
{
    public AudioSource fallingSound;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        GameObject entityToFall = collision.gameObject;

        PlayerScript playerScript = null;
        Enemy enemyScript = null;

        if (entityToFall.tag == "Player")
        {

            playerScript = entityToFall.GetComponent<PlayerScript>();
        }
        else if (entityToFall.CompareTag("Enemy"))
        {
            enemyScript = entityToFall.GetComponent<Enemy>();
        }

        if (entityToFall.tag == "Enemy" || entityToFall.tag == "Player")
        {
            fallingSound.Play();
            StartCoroutine(falls(entityToFall.transform, playerScript, enemyScript));
        }
    }

    IEnumerator falls(Transform entityToFall, PlayerScript playerScript, Enemy enemyScript)
    {
        Debug.Log("algo se cayo: " );


        if (playerScript != null)
        {
            playerScript.isAlive = false;
        }
        if (enemyScript != null)
        
            {
                enemyScript.isAlive = false;
            }

            fallingSound.Play();
            int fallSpeed = 50;
            for (int i = fallSpeed; i >= 0; i--)
            {
                float scale = i / 50f;
                entityToFall.localScale = new Vector3(scale, scale, 1);
                yield return new WaitForSeconds(0.035f);
            }
            if (enemyScript != null)
              {
                enemyScript.aura.SetActive(false);
              }
            if (playerScript != null)
              {
                playerScript.gameOverScreen();
              }
    }
}
