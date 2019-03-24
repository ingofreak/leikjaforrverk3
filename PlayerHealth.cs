using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;                            // Hversu Mikið líf þú átt
    public int currentHealth;                                   // Hversu mikið líf er eftir
    public Slider healthSlider;                                 // UI
    public Image damageImage;                                   // Hvað kemur upp þegar þú skaðast
    public AudioClip deathClip;                                 // hljóð sem kemur upp þegar þú deyrð
    public float flashSpeed = 5f;                               // hraði á myndinni
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // litur á myndinni


    Animator anim;                                           
    AudioSource playerAudio;                               
    PlayerMovement playerMovement;                              
    bool isDead;                                              
    bool damaged;                                               


    void Awake()
    {
        
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();

        // Hvað þú byrjar með mikið líf
        currentHealth = startingHealth;
    }


    void Update()
    {
        // Ef þú skaðast
        if (damaged)
        {
           
            damageImage.color = flashColour;
        }
       
        else
        {
            
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

      
        damaged = false;
    }


    public void TakeDamage(int amount)
    {
       
        damaged = true;

        // minnka lífið
        currentHealth -= amount;

       
        healthSlider.value = currentHealth;

       
        playerAudio.Play();

       
        if (currentHealth <= 0 && !isDead)
        {
            // ef lífið fer fyrir neðan eða er 0 þá deyrðu.
            Death();
        }
    }


    void Death()
    {
        
        isDead = true;

      
        anim.SetTrigger("Die");

       
        playerAudio.clip = deathClip;
        playerAudio.Play();

      
        playerMovement.enabled = false;
    }
}
