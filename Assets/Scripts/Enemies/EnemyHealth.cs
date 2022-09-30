using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    private float m_currentHealth;

    private Animator anim;
    
    void Start() {

        anim = GetComponent<Animator>();
        m_currentHealth = maxHealth;
    
    }
    
    public void TakeDamage(float damageReceived) {
    
        m_currentHealth -= damageReceived;

        if (m_currentHealth <= 0) 
        {
            Die();
        } 
        else 
        {
            anim.SetTrigger("Hurts");
        }
    
    }
    
    void Die() {
        
        anim.SetTrigger("Dies");
        
        Destroy(gameObject);
    }
}
