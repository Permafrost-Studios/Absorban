using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterChaser : MonoBehaviour
{
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
    public GameObject groundCheck;
    public GameObject shootPoint;
    public GameObject projectile;
    public GameObject sightPoint;
    public float groundCheckRadius;
    public float shootRange;
    public float sightRange;
    
    public float shootDamage;
    public float shootForce;
    public float fireRate;
    private float m_shootCooldown;

    private float m_timeout = 1f;
    
    public float moveSpeed;
    public bool isFacingRight;

    private Rigidbody2D body;
    private GameObject player;
    private bool m_forwardIsGround;
    private bool m_facingRight;
    public bool m_isChasing;
    private Collider2D m_playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        m_shootCooldown = 0f;
        m_isChasing = false;
        m_facingRight = isFacingRight;
        
        body = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player Revised"); //Based on the assumption that the player is named "Player"

        //For roaming
        body.velocity = new Vector2(moveSpeed, body.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        m_shootCooldown -= Time.deltaTime;

        m_forwardIsGround = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, whatIsGround);

        //Changes direction if the enemy reaches the end of a platform
        if (m_forwardIsGround == false && m_isChasing == false) 
        {
            Flip();
        }

        // RaycastHit2D sightHit = Physics2D.Raycast(sightPoint.transform.position, transform.right * (m_facingRight ? 1 : -1), sightRange, whatIsPlayer);
        RaycastHit2D sightHit = Physics2D.BoxCast(transform.position, new Vector2(2, 2), 0f, transform.right * (m_facingRight ? 1 : -1), sightRange, whatIsPlayer);

        if(sightHit.collider != null) 
        {
            m_isChasing = true;
        }

        RaycastHit2D shootHit = Physics2D.Raycast(shootPoint.transform.position, transform.right * (m_facingRight ? 1 : -1), shootRange, whatIsPlayer);

        if(shootHit.collider != null) 
        {
            if(m_shootCooldown <= 0) {
                Debug.Log("Shot at the player");
                GameObject currentProjectile;
                currentProjectile = Instantiate(projectile, shootPoint.transform.position, shootPoint.transform.rotation);
                currentProjectile.GetComponent<EnemyProjectile>().Shoot(shootDamage, shootForce, (m_facingRight ? 1 : -1));

                m_shootCooldown = fireRate;
            }
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

    void OnDrawGizmos() 
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(sightPoint.transform.position, sightPoint.transform.position + transform.right * sightRange * (m_facingRight ? 1 : -1));
    }
}
