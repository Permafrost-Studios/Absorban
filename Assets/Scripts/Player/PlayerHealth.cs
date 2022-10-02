using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    private float m_currentHealth;
    private bool m_immune;

    public float immunityLength;
    public float knockbackMultiplier;
	
	[SerializeField] private GameObject respawnHandler;

    private UIDocument document;
    private VisualElement healthBar;

    private Animator anim;

    private Rigidbody2D body;
    
	void Awake() {
		
        m_currentHealth = maxHealth;
		
	}
	
    void Start() 
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        document = GetComponent<UIDocument>();
        healthBar = document.rootVisualElement.Q("Fill");

        // Stub();
    }
    
    public void TakeDamage(float damageReceived) 
    {
        if(m_immune == false) 
        {
            m_currentHealth -= damageReceived;

            if (m_currentHealth <= 0) {
            
                Die();
            
            }

            m_currentHealth = Mathf.Clamp(m_currentHealth, -maxHealth, maxHealth);

            UpdateHealthBar();

            anim.SetTrigger("Hurts");

            StartCoroutine(iFrames(immunityLength));

            body.velocity =  new Vector2 (-body.velocity.x, Mathf.Clamp(-body.velocity.y, 0f, 10f));
        }
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

    IEnumerator iFrames(float length) 
    {
        gameObject.GetComponent<PlayerMoving>().ToggleMovement(true);
        m_immune = true;
        yield return new WaitForSeconds(length);
        m_immune = false;
        gameObject.GetComponent<PlayerMoving>().ToggleMovement(false);
    }
}
