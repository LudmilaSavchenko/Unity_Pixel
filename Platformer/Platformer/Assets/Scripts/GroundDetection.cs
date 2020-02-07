using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    public bool isGrounded;
    public Transform groundCheck;
    private float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    //public bool isGrounded;

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
    }

    //private void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col.gameObject.CompareTag("Platform"))
    //    {
    //        isGrounded = true;
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D col)
    //{
    //    if (col.gameObject.CompareTag("Platform"))
    //    {
    //        isGrounded = false;
    //    }
    //}
}
