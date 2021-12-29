using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int damage = 5;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private bool swarm = true;
    [SerializeField]
    private bool facingRight = true;

    // Moving Enemy back and forth
    private float dirX;
    private Rigidbody2D rb;
    
    private Vector3 localScale;

    [SerializeField]
    private EnemyData data;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SetValues();

        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        dirX = -1f;
    }

    void Update()
    {
        if (swarm)
        {
            Swarm();
        } 
        else 
        {
            rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
        }
    }

    void LateUpdate()
    {
        CheckWhereToFace();
    }

    private void SetValues()
    {
        GetComponent<EnemyLife>().SetHealth(data.health);
        damage = data.damage;
        speed = data.speed;
    }

    private void Swarm()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        dirX = transform.position.x > 0 ? 1 : -1;
    }

    private void CheckWhereToFace() 
    {
        if (dirX > 0)
        {
            facingRight = true;
        }
        else if (dirX < 0)
        {
            facingRight = false;
        }

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
        {
            localScale.x *= -1;
        }
        transform.localScale = localScale;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if(collider.GetComponent<PlayerLife>() != null)
            {
                collider.GetComponent<PlayerLife>().TakeDamage(damage);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            dirX *= -1f;
        }
    }
}
