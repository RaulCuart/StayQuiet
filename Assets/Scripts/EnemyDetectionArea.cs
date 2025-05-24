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
            if (playerScript.flashlightStatus && enemy.monsterName == Enemy.monsterType.Eye && !playerScript.isHidden)
            {
                enemy.playerInRange = true;
            }
            if (enemy.movesToLastKnownPos && !playerScript.isHidden)
            {
                enemy.playerInRange = true;
                enemy.stopMoveTolastKnownpos();
            }


            if (!playerScript.isShifting && enemy.monsterName == Enemy.monsterType.Bat && !playerScript.isHidden)
            {
                enemy.playerInRange = true;
            }
            if (playerScript.isShifting && enemy.monsterName == Enemy.monsterType.Bat)
            {
                enemy.playerInRange = false;
            }
        }

    }

   private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerDetection") && playerScript.flashlightStatus && enemy.monsterName == Enemy.monsterType.Eye && !playerScript.isHidden)
        {

            enemy.playerInRange = true;
        }
        if (collision.CompareTag("PlayerDetection") && !playerScript.isShifting && enemy.monsterName == Enemy.monsterType.Bat && !playerScript.isHidden)
        {

            enemy.playerInRange = true;
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
        }
    }
}
