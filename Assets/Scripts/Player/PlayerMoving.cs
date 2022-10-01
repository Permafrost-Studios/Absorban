using UnityEngine;


public class PlayerMoving : MonoBehaviour
{	

	[SerializeField] private Rigidbody2D playerBody;

	[SerializeField] private float sprintMultiplier;
	
	[SerializeField] private float m_speedmult;
	
	public Transform feet;
	public GameObject jumpDust;

	public float jumpVel;
	public float playerWidth;
	public int extraJumpCounter;
	private int m_privJumpCounter;
	
	private float m_playerVelocityX;
	private float m_playerVelocityY;

	private bool m_isGrounded; 
	public bool facingRight;

	public float coyoteTime;
	private float m_coyoteTime;

	private Animator anim;

	[SerializeField] public LayerMask whatIsGround;
	[SerializeField] private GameObject m_groundCheckObj;
	
    void Start() {
		playerBody = GetComponent<Rigidbody2D>();  
		anim = GetComponent<Animator>();
		m_speedmult = 10;
		sprintMultiplier = 1;
		m_privJumpCounter = extraJumpCounter;
		
    }
	

    void Update() {
		
		if ( Input.GetButtonDown("Sprint") ) {		
			sprintMultiplier = 2;
		} else {
			sprintMultiplier = 1; 
		}
		
		m_isGrounded = Physics2D.OverlapBox(m_groundCheckObj.transform.position, new Vector2(playerWidth - .1f, .05f), 0f, whatIsGround);

		if(m_isGrounded) 
		{
			m_coyoteTime = coyoteTime;
			m_privJumpCounter = extraJumpCounter;
		} 
		else 
		{ 
			m_coyoteTime -= Time.deltaTime;
		}
		
		if ( Input.GetKeyDown(KeyCode.Space) ) {
			if ( m_coyoteTime >= 0f) { 
				// GameObject dust = Instantiate(jumpDust, feet.position, Quaternion.identity);
				// dust.GetComponent<ParticleSystem>().Play();

				Jump(); 
			} else if ( m_privJumpCounter > 0 ) {
				Jump();
				m_privJumpCounter--;
			}
		}
		
		

		m_playerVelocityX = Input.GetAxisRaw("Horizontal") * m_speedmult * sprintMultiplier;
		playerBody.velocity = new Vector2(m_playerVelocityX, playerBody.velocity.y);


		if ( Input.GetAxisRaw("Horizontal") < 0 && facingRight ) {
            Flip();
        } else if ( Input.GetAxisRaw("Horizontal") > 0 && !facingRight ) { 
            Flip();  
        } 

		if (Mathf.Abs(m_playerVelocityX) > 0.1f) 
		{
			anim.SetBool("isMoving", true);
		} 
		else 
		{
			anim.SetBool("isMoving", false);	
		}
		
    }
	
	void Jump() {
		playerBody.velocity = new Vector2(playerBody.velocity.x, jumpVel);

		GameObject dust = Instantiate(jumpDust, feet.position, Quaternion.identity);
		dust.GetComponent<ParticleSystem>().Play();
		Destroy(dust, dust.GetComponent<ParticleSystem>().main.duration);
	}

	void Flip() {
        facingRight = !facingRight;
        Vector2 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

	public float returnDirectionMultiplier() 
	{
		return (facingRight ? 1 : -1);
	}
}