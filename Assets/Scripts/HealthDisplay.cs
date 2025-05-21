using UnityEngine;
using UnityEngine.UI;
public class HealthDisplay : MonoBehaviour
{
    private int health;
    private int maxHealth;
    public PlayerScript player;

    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Image[] hearts;
    void Start()
    {
        health = player.currentHp;
        maxHealth = player.maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        health = player.currentHp;
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            } else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled= false;
            }
        }


    }
}
