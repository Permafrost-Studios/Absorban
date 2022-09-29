using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    private float m_currentHealth;

    private UIDocument document;
    private VisualElement healthBar;
    
    void Start() 
    {
        document = GetComponent<UIDocument>();
        healthBar = document.rootVisualElement.Q("Fill");

        m_currentHealth = maxHealth;

        // Stub();
    }
    
    public void TakeDamage(float damageReceived) 
    {
        Debug.Log("Damage Took");
        m_currentHealth -= damageReceived;

        if (m_currentHealth <= 0) {
        
            Die();
        
        }

        UpdateHealthBar();
    
    }

    void UpdateHealthBar() 
    {
        healthBar.style.width = new StyleLength(Length.Percent((m_currentHealth/maxHealth) * 100));
    }
    
    void Die() {
    
        // Transition to end scene
        Debug.Log("Lol you died");
    }

    // void Stub() 
    // {
    //     TakeDamage(20f);
    // }
}
