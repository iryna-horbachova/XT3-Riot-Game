using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    private int maxHealth = 50;
    private int currentHealth;

    private Animator animator;

    [SerializeField] 
    private AudioSource deathSoundEffect;

    void Start()
    {
        animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHealth(int health)
    {
        maxHealth = health;
        currentHealth = health;
    }

    public void TakeDamage(int damage) 
    {
        currentHealth -= damage;

        StartCoroutine(VisualIndicator(Color.red));
    
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator VisualIndicator(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }


    private void Die()
    {
        deathSoundEffect.Play();
        animator.SetTrigger("Death");
    }
}
