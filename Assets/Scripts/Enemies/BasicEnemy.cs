using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
    public GameObject groundCheck;
    public GameObject attackPoint;
    public float groundCheckRadius;
    public float attackRadius;
    public float attackDamage;
    
    public float moveSpeed;
    public bool isFacingRight;

    private Rigidbody2D body;
    private bool m_forwardIsGround;
    private bool m_facingRight;
    private Collider2D m_playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        m_facingRight = isFacingRight;
        body = GetComponent<Rigidbody2D>();

        //For roaming
        body.velocity = new Vector2(moveSpeed * (m_facingRight ? 1 : -1), body.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        m_forwardIsGround = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, whatIsGround);
        m_playerCollider = Physics2D.OverlapCircle(attackPoint.transform.position, attackRadius, whatIsPlayer);
        bool isAPlayer = Physics2D.OverlapCircle(attackPoint.transform.position, attackRadius, whatIsPlayer);

        //Changes direction if the enemy reaches the end of a platform
        if (m_forwardIsGround == false) 
        {
            Flip();
        }

        if (isAPlayer) 
        {
            Debug.Log("Found a Player");
            m_playerCollider.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }

    }

    void Flip() 
    {
        //Done in multiple steps because you cannot directly set transform.localscale.x
        m_facingRight = !m_facingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;

        body.velocity = new Vector2(body.velocity.x * (m_facingRight ? 1 : -1), body.velocity.y);
    }
}
