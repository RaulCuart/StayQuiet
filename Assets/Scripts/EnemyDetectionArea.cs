using System.Collections;
using UnityEngine;

public class EnemyDetectionArea : MonoBehaviour
{
    public Enemy enemy;
    public PlayerScript playerScript;
    private AudioSource batDetection;
    private AudioSource eyeDetection;
    private AudioSource chaseMusic;
    private bool soundAlreadyPlayed = false;
    private float startMusicVolume;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        batDetection = GameObject.Find("BatDetectionSound").GetComponent<AudioSource>();
        eyeDetection = GameObject.Find("EyeDetectionSound").GetComponent<AudioSource>();
        chaseMusic = GameObject.Find("ChaseMusic").GetComponent<AudioSource>();
        startMusicVolume = chaseMusic.volume;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("PlayerDetection"))
        {
            if (!chaseMusic.isPlaying && enemy.playerInRange)
            {
                chaseMusic.volume = startMusicVolume;
                chaseMusic.Play();
            }

            if (playerScript.flashlightStatus && enemy.monsterName == Enemy.monsterType.Eye && !playerScript.isHidden && !enemy.playerInRange)
            {
                enemy.playerInRange = true;
                if (!eyeDetection.isPlaying && !soundAlreadyPlayed)
                {
                    eyeDetection.Play();
                    chaseMusic.PlayDelayed(1f);

                }
                soundAlreadyPlayed = true;

            }
            if (enemy.movesToLastKnownPos && !playerScript.isHidden && enemy.monsterName == Enemy.monsterType.Bat && !playerScript.isShifting)
            {
                enemy.playerInRange = true;
                if (!batDetection.isPlaying)
                {
                    batDetection.Play();

                }
                enemy.stopMoveTolastKnownpos();
            }

            if (playerScript.flashlight && enemy.monsterName == Enemy.monsterType.Eye)
            {
                enemy.playerInRange = false;
            }


            if (!playerScript.isShifting && enemy.monsterName == Enemy.monsterType.Bat && !playerScript.isHidden && !enemy.playerInRange)
            {
                enemy.playerInRange = true;
                if (!batDetection.isPlaying)
                {
                    batDetection.Play();
                }

            }
            if (playerScript.isShifting && enemy.monsterName == Enemy.monsterType.Bat)
            {
                enemy.playerInRange = false;
            }
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       if (collision.CompareTag("PlayerDetection") && playerScript.isBeingHit)
        {
            enemy.playerInRange = true;
        }
        

        if (!chaseMusic.isPlaying && enemy.playerInRange)
        {
            chaseMusic.volume = startMusicVolume;
            chaseMusic.Play();
        }

        if (collision.CompareTag("PlayerDetection") && playerScript.flashlightStatus && enemy.monsterName == Enemy.monsterType.Eye && !playerScript.isHidden)
        {

            enemy.playerInRange = true;
            if (!eyeDetection.isPlaying && !soundAlreadyPlayed)
            {
                eyeDetection.Play();

            }
            soundAlreadyPlayed = true;
        }
        if (collision.CompareTag("PlayerDetection") && !playerScript.isShifting && enemy.monsterName == Enemy.monsterType.Bat && !playerScript.isHidden)
        {

            enemy.playerInRange = true;
            if (!batDetection.isPlaying && !soundAlreadyPlayed)
            {
                batDetection.Play();

            }
            soundAlreadyPlayed = true;
        }
        if (collision.CompareTag("PlayerDetection") && playerScript.isHidden)
        {
            enemy.playerInRange = false;
            enemy.startsMoveToLastKnownPos();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerDetection"))
        {
            enemy.playerInRange = false;
            enemy.startsMoveToLastKnownPos();
            StartCoroutine(fadeOutChasingMusic(chaseMusic));
            soundAlreadyPlayed = false;
        }
    }

    private IEnumerator fadeOutChasingMusic(AudioSource chaseMusic)
    {
        float actualVolume = chaseMusic.volume;

        float duration = 2f;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            chaseMusic.volume = Mathf.Lerp(actualVolume, 0f, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        chaseMusic.volume = 0f;
        chaseMusic.Stop();
        chaseMusic.volume = actualVolume;
    }
}
