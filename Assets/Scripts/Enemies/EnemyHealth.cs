using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    private float m_currentHealth;
    public GameObject DeadBody;

    public int weaponDropID;

    public string memoryName;

    public enum Drops {none, weapon, memory, both};

    public Drops drops;

    private Animator anim;

    private AudioSource source;
    public AudioClip hurtSound;
    public AudioClip dieSound;
    
    void Start() {
        source = GetComponent<AudioSource>();
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

        source.PlayOneShot(hurtSound);
    }
    
    void Die() {
        
        anim.SetTrigger("Dies");

        GameObject dead = Instantiate(DeadBody);

        switch (drops)
        {
            case Drops.both:
                dead.transform.position = gameObject.transform.position;
                dead.GetComponent<DeadBody>().Init(weaponDropID, memoryName);
                break;
            case Drops.weapon:
                dead.transform.position = gameObject.transform.position;
                dead.GetComponent<DeadBody>().Init(weaponDropID, memoryName);
                break;
            case Drops.memory:
                dead.transform.position = gameObject.transform.position;
                dead.GetComponent<DeadBody>().Init(memoryName);
                break;
            case Drops.none:
                Destroy(dead);
                break;
            default:
                break;
        }  
        
        source.PlayOneShot(dieSound);
        Destroy(gameObject);
    }
}
