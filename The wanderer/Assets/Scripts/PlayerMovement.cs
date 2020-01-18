using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    public float moveSpeed = 3f;
    public float jumpForce = 40;
    float velX;
    float velY;
    bool facingRight = true;
    public Animator animator;
    public int contor=0;
    //public static float healthAmount;
    /*private void Start()
    {
        healthAmount = 1;
    }*/
    // Update is called once per frame
    void Update()
    {

        Jump();
        velX = Input.GetAxisRaw("Horizontal");
       // velY = rigBody.velocity.y;
        //rigBody.velocity = new Vector2(velX * moveSpeed,velY);
        //rigBody.freezeRotation();

        Vector3 horizontal = new Vector3(velX, 0.0f, 0.0f);
        transform.position = transform.position + horizontal*moveSpeed;
        
        animator.SetFloat("Speed", System.Math.Abs(velX));
        
       // OnLanding();
    }
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);

    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            contor++;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            if(contor >1)
            {
                jumpForce = jumpForce-30;
                
            }
            //animator.SetBool("IsJumping", true);
            //SoundManager.PlaySound();
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Coins"))
        {
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Putere"))
        {

            Destroy(other.gameObject);
        }
        /*
        if (other.gameObject.CompareTag("Panou"))
            healthAmount -= 0.1f;*/
    }
    
    private void LateUpdate()
    {
      Vector3 localScale = transform.localScale;
        if(velX>0)
        {
            facingRight = true;

        }else if(velX<0)
        {
            facingRight = false;
        }
        if(((facingRight)&&(localScale.x<0))||((!facingRight)&&(localScale.x>0)))
        {
            localScale.x *= -1;
        }
        transform.localScale = localScale;
        
    }

}
