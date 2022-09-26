using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float despawnTime;
    private float m_damage;
    private Rigidbody2D body;

    public void Shoot(float damage, float shootForce, float directionMultiplier) 
    {
        m_damage = damage;
        body = GetComponent<Rigidbody2D>();
        body.AddForce(transform.right * shootForce * directionMultiplier);
    }

    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.layer == 7)
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(m_damage);
            Destroy(gameObject);
        } 
    }

    void Update() {
        despawnTime -= Time.deltaTime;

        if(despawnTime <= 0f) 
        {
            Destroy(gameObject);
        }
    }
}
