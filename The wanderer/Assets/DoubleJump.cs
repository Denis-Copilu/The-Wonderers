using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoubleJump : MonoBehaviour {

	float dirX;

	[SerializeField]
	float jumpForce = 500f, moveSpeed = 5f;

	Rigidbody2D rb;
    bool facingRight = true;
    public Animator animator;
    bool doubleJumpAllowed = false;
	bool onTheGround = false;
    float velX;
    public GameObject hearth1, hearth2, hearth3;
    public GameObject bomb1;
    int contor = 0;
    // Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
        velX = Input.GetAxisRaw("Horizontal");
        if (rb.velocity.y == 0)
			onTheGround = true;
		else
			onTheGround = false;
		
		if (onTheGround)
			doubleJumpAllowed = true;

		if (onTheGround && Input.GetButtonDown ("Jump")) {
			Jump ();
		} else if (doubleJumpAllowed && Input.GetButtonDown ("Jump")) {
			Jump ();
			doubleJumpAllowed = false;
		}
		
		dirX = Input.GetAxis ("Horizontal") * moveSpeed;
        animator.SetFloat("Speed", System.Math.Abs(velX));

    }
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);

    }
    void FixedUpdate()
	{
		rb.velocity = new Vector2 (dirX, rb.velocity.y);
	}

	void Jump()
	{
		rb.velocity = new Vector2 (rb.velocity.x, 0f);;
		rb.AddForce (Vector2.up * jumpForce);
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coins"))
        {
            Destroy(other.gameObject);
        }
        /*if (other.gameObject.CompareTag("Respawn"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //Destroy(other.gameObject);
        }*/
        if (other.gameObject.CompareTag("Bomb"))
        {
            Destroy(bomb1);
            contor++;
            if(contor == 1)
            Destroy(hearth3);
            if (contor == 2)
                Destroy(hearth2);
            if (contor == 3)
            {
                Destroy(hearth1);
                contor = 0;
                SceneManager.LoadScene(1);
            }
            //Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Putere"))
        {
            jumpForce += 20;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Putere_next"))
        {
            Destroy(other.gameObject);
            SceneManager.LoadScene(2);

        }
        /*
        if (other.gameObject.CompareTag("Panou"))
            healthAmount -= 0.1f;*/
    }

    private void LateUpdate()
    {
        Vector3 localScale = transform.localScale;
        if (velX > 0)
        {
            facingRight = true;

        }
        else if (velX < 0)
        {
            facingRight = false;
        }
        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
        {
            localScale.x *= -1;
        }
        transform.localScale = localScale;

    }

}


