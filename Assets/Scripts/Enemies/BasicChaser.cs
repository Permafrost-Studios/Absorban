using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicChaser : MonoBehaviour
{
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
    public GameObject groundCheck;
    public GameObject attackPoint;
    public float groundCheckRadius;
    public float attackRadius;
    public float attackDamage;
    public float sightRange;
    
    public float moveSpeed;
    public bool isFacingRight;

    private float m_timeout = 2f;

    private Rigidbody2D body;
    private GameObject player;
    private bool m_forwardIsGround;
    private bool m_facingRight;
    public bool m_isChasing;
    private Collider2D m_playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        m_isChasing = false;
        m_facingRight = isFacingRight;
        
        body = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player"); //Based on the assumption that the player is named "Player"

        //For roaming
        body.velocity = new Vector2(moveSpeed, body.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        m_forwardIsGround = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, whatIsGround);
        m_playerCollider = Physics2D.OverlapCircle(attackPoint.transform.position, attackRadius, whatIsPlayer);
        bool isAPlayer = Physics2D.OverlapCircle(attackPoint.transform.position, attackRadius, whatIsPlayer);

        //Changes direction if the enemy reaches the end of a platform
        if (m_forwardIsGround == false && m_isChasing == false) 
        {
            Flip();
        }

        if (isAPlayer) 
        {
            m_playerCollider.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }

        RaycastHit2D sightHit = Physics2D.Raycast(transform.position, transform.right * (m_facingRight ? 1 : -1), sightRange, whatIsPlayer);

        if(sightHit.collider != null) 
        {
            // Debug.Log("Found a Player");
            m_isChasing = true;
        }

        if(m_isChasing == true) 
        {
            if((player.transform.position.x - transform.position.x) < 0) 
            {
                if (m_facingRight) 
                {
                    Flip();
                }
            } 
            else 
            {
                if (!m_facingRight) 
                {
                    Flip();
                }
            }
        }

        if (Mathf.Abs(player.transform.position.x - transform.position.x) <  0.1f) 
        {
            m_timeout -= Time.deltaTime;
        }

        if(m_timeout <= 0f) 
        {
            m_isChasing = false;
        }

    }

    void Flip() 
    {
        //Done in multiple steps because you cannot directly set transform.localscale.x
        m_facingRight = !m_facingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;

        body.velocity = new Vector2(-body.velocity.x, body.velocity.y);
    }

    // void OnDrawGizmos() 
    // {
    //     Gizmos.color = Color.blue;
    //     Gizmos.DrawLine(transform.position, transform.position + transform.right * sightRange * (m_facingRight ? 1 : -1));
    // }
}
