using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject attackArea = default;
    private Animator animator;

    [SerializeField] private AudioSource attackSoundEffect;

    private bool attacking = false;

    private float timeToAttack = 0.1f;
    private float timer = 0f;

    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Attack"))
        {
            Attack();
        }

        if(attacking)
        {
            timer += Time.deltaTime;

            if(timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                attackArea.SetActive(attacking);
            }
        }
    }

    public void IncreaseAttackDamage(int damageIncrease)
    {
        attackArea.GetComponent<AttackArea>().IncreaseAttackDamage(damageIncrease);
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        attackSoundEffect.Play();
        attacking = true;
        attackArea.SetActive(attacking);
    }
}
