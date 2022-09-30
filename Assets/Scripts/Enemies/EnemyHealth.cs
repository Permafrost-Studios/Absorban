using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    private float m_currentHealth;

    private Animator anim;
    
    void Start() {
    
        m_currentHealth = maxHealth;
    
    }
    
    public void TakeDamage(float damageReceived) {
    
        m_currentHealth -= damageReceived;

        if (m_currentHealth <= 0) {
        
            Die();
        
        }
    
    }
    
    void Die() {
    
        // do something
    
    }
}
