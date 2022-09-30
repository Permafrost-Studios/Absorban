using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    private float m_currentHealth;
	
	[SerializeField] private GameObject respawnHandler;

    private UIDocument document;
    private VisualElement healthBar;

    private Animator anim;
    
	void Awake() {
		
        m_currentHealth = maxHealth;
		
	}
	
    void Start() 
    {
        anim = GetComponent<Animator>();
        document = GetComponent<UIDocument>();
        healthBar = document.rootVisualElement.Q("Fill");

        // Stub();
    }
    
    public void TakeDamage(float damageReceived) 
    {
        m_currentHealth -= damageReceived;

        if (m_currentHealth <= 0) {
        
            Die();
        
        }

        m_currentHealth = Mathf.Clamp(m_currentHealth, -maxHealth, maxHealth);

        UpdateHealthBar();

        anim.SetTrigger("Hurts");
    }

    void UpdateHealthBar() 
    {
        healthBar.style.width = new StyleLength(Length.Percent((m_currentHealth/maxHealth) * 100));
    }
    
    void Die() {

        //Respawn you at the start of the level
		
        anim.SetTrigger("Dies");
		
		respawnHandler.GetComponent<PlayerRespawnHandler>().RespawnPlayer();
        
		m_currentHealth = maxHealth;
    }

    // void Stub() 
    // {
    //     TakeDamage(20f);
    // }
}
