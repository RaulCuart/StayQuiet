using UnityEngine;

public class EnemyDetectionArea : MonoBehaviour
{
    public Enemy enemy;
    public PlayerScript playerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("PlayerDetection"))
        {
            if (playerScript.flashlightStatus && enemy.monsterName == Enemy.monsterType.Eye)
            {
                enemy.playerInRange = true;


            }
            if (enemy.movesToLastKnownPos)
            {
                enemy.playerInRange = true;
                enemy.startsMoveToLastKnownPos();
            }


            if (!playerScript.isShifting && enemy.monsterName == Enemy.monsterType.Bat)
            {

                enemy.playerInRange = true;
            }
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerDetection") && playerScript.flashlightStatus && enemy.monsterName == Enemy.monsterType.Eye)
        {

            enemy.playerInRange = true;
        }
        if (collision.CompareTag("PlayerDetection") && !playerScript.isShifting && enemy.monsterName == Enemy.monsterType.Bat)
        {

            enemy.playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerDetection"))
        {
            enemy.playerInRange = false;
            enemy.startsMoveToLastKnownPos();
        }
    }
}
