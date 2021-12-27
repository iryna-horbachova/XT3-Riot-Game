using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage = 10;

    public void IncreaseAttackDamage(int damageIncrease)
    {
        damage += damageIncrease;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<EnemyLife>() != null)
        {
            EnemyLife enemyLife = collider.GetComponent<EnemyLife>();
            enemyLife.TakeDamage(damage); 
        }
    }
}
