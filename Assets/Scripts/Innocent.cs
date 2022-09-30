using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Innocent : MonoBehaviour
{
    public float moveCooldown;
    public float variance;
    private float m_moveCooldown;
    private bool m_facingRight;

    void Start() {
        m_facingRight = true;

        m_moveCooldown = moveCooldown; 
    }

    // Update is called once per frame
    void Update()
    {
        m_moveCooldown -= Time.deltaTime;

        if (m_moveCooldown <= 0) 
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-variance, variance), gameObject.GetComponent<Rigidbody2D>().velocity.y); 

            m_moveCooldown = moveCooldown; 
        }

        if((gameObject.GetComponent<Rigidbody2D>().velocity.x > 0) & !m_facingRight) 
        {
            Flip();
        } else if((gameObject.GetComponent<Rigidbody2D>().velocity.x < 0) & m_facingRight)
        {
            Flip();
        }
    }

    void Flip() 
    {
        //Done in multiple steps because you cannot directly set transform.localscale.x
        m_facingRight = !m_facingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}
