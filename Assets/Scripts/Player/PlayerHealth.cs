using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    private float m_currentHealth;
    
    void Start() {
    
        m_currentHealth = maxHealth;
    
    }
    
    public void TakeDamage(float damageReceived) {
        Debug.Log("Damage Took");
        m_currentHealth -= damageReceived;

        if (m_currentHealth <= 0) {
        
            Die();
        
        }
    
    }
    
    void Die() {
    
        // Transition to end scene
        Debug.Log("Lol you died");
    }
}
