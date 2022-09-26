using UnityEngine;


public class PlayerMoving : MonoBehaviour
{	

	[SerializeField] private Rigidbody2D playerBody;


	[SerializeField] private float sprintMultiplier;
	
	[SerializeField] private float m_speedmult;
	public float jumpVel;
	public float playerWidth;
	public int extraJumpCounter;
	private int m_privJumpCounter;
	
	private float m_playerVelocityX;
	private float m_playerVelocityY;

	private bool m_isGrounded; 
	private bool facingRight;

	[SerializeField] public LayerMask whatIsGround;
	[SerializeField] private GameObject groundCheckObj;
	
    void Awake() {
		m_speedmult = 10;
		sprintMultiplier = 1;
		m_privJumpCounter = extraJumpCounter;
    }
	

    void FixedUpdate() {
		if ( Input.GetButtonDown("Sprint") ) {		
			sprintMultiplier = 2;
		} else {
			sprintMultiplier = 1; 
		}
		
		m_isGrounded = Physics2D.OverlapBox(groundCheckObj.transform.position, new Vector2(playerWidth, .05f), 0f, whatIsGround);
		
		Debug.Log(m_privJumpCounter);
		
		if ( m_isGrounded ) {	
			m_privJumpCounter = extraJumpCounter;
		} 
		
		if ( Input.GetButtonDown("Jump") ) {
			if ( m_isGrounded ) { 
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
		
    }
	
	void Jump() {
		playerBody.velocity = new Vector2(playerBody.velocity.x, jumpVel);	
	}

	void Flip() {
        facingRight = !facingRight;
        Vector2 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
	
}