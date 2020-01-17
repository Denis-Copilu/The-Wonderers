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
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            //animator.SetBool("IsJumping", true);
        }
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
