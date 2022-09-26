using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float damage;
    public float shootForce;

    private Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        body.AddForce(transform.right * shootForce);
    }

    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.layer == 7)
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        } 
    }
}
