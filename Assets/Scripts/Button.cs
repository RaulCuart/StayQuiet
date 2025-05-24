using System;
using System.Collections;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject buttonObject;
    public GameObject pitTrap;
    public BoxCollider2D activationArea;
    public GameObject player;
    public GameObject playerFlashLight;
    public Boolean isTrap;
    private Boolean canBeToggled = false;
    public Boolean activated = false;
    public AudioSource fallingEffect;
    private bool playerIsAlive;
   
    void Start()
    {
         playerIsAlive = player.GetComponent<PlayerScript>().isAlive;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canBeToggled && playerIsAlive)
        {
            if (!activated)
            {
                buttonObject.GetComponent<SpriteRenderer>().enabled = true;
                activated = true;
                if (isTrap)
                {
                    canBeToggled = false;
                    player.GetComponent<PlayerScript>().isFalling = true;
                    pitTrap.SetActive(true);
                    playerFlashLight.SetActive(false);
                }
            } else
            {
                buttonObject.GetComponent<SpriteRenderer>().enabled = false;
                activated = false;
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            canBeToggled=true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            canBeToggled=false;
        }
    }



}
