using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public float speed;
    public float force;
    public Rigidbody2D rigidbody;

    public float minimalHeight;
    public bool isCheatMode;
    public GroundDetection groundDetection;
    private Vector3 direction;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private bool isJumping;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (animator != null)
            animator.SetBool("isGrounded", groundDetection.isGrounded);

        if (!isJumping && !groundDetection.isGrounded)
            animator.SetTrigger("StartFall");

        isJumping = isJumping && !groundDetection.isGrounded;

        direction = Vector3.zero; //(0,0)

        if (Input.GetKey(KeyCode.A))
            direction = Vector3.left; // (x-1,y0)

        if (Input.GetKey(KeyCode.D))
            direction = Vector3.right; // (1,0)

        direction *= speed;
        direction.y = rigidbody.velocity.y;
        rigidbody.velocity = direction;

        if (Input.GetKeyDown(KeyCode.Space) && groundDetection.isGrounded)
        {
            rigidbody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            animator.SetTrigger("StartJump");
            isJumping = true;
        }

        if (direction.x > 0)
            spriteRenderer.flipX = false;
        if (direction.x < 0)
            spriteRenderer.flipX = true;

        animator.SetFloat("Speed", Mathf.Abs(direction.x));

        CheckFall();
    }

    void CheckFall()
    {
        if (transform.position.y < minimalHeight && isCheatMode)
        {
            rigidbody.velocity = new Vector2(0, 0);
            transform.position = new Vector3(1, 1, 0);
        }
        else if (transform.position.y < minimalHeight)
        {
            Destroy(gameObject);
        }
    }
}
