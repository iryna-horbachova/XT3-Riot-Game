using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private int maxHealth = 100;
    private int currentHealth;

    public HealthBar healthBar;

    private Animator animator;
    private Rigidbody2D rigidbody;

    [SerializeField] private AudioSource deathSoundEffect;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);


        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage) 
    {
        currentHealth -= damage;

        StartCoroutine(VisualIndicator(Color.red));
        healthBar.SetHealth(currentHealth);
        
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
        rigidbody.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("Death");
        healthBar.SetHealth(0);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            TakeDamage(20);
        }
    }
}
