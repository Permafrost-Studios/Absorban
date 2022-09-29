using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProjectile : MonoBehaviour
{
    private float m_despawnTime = 3f;
    private float m_damage;
    private Rigidbody2D body;

    public void Shoot(float damage, float shootForce, float directionMultiplier) {
        m_damage = damage;
        body = GetComponent<Rigidbody2D>();
        body.AddForce(transform.right * shootForce * directionMultiplier);
    }

    public void Shoot(float damage, float shootForce, Vector2 direction) {
        m_damage = damage;
        body = GetComponent<Rigidbody2D>();
        body.AddForce(direction.normalized * shootForce);
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        Debug.Log(m_damage);
        if (other.gameObject.layer == 8) {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(m_damage);
        } 
        Destroy(gameObject);
    }

    void Update() {
        m_despawnTime -= Time.deltaTime;

        if(m_despawnTime <= 0f) 
        {
            Destroy(gameObject);
        }
    }
}
