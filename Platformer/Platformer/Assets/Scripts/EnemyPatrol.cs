using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject leftBorder;
    public GameObject rightBorder;
    public Rigidbody2D rigidbody;
    public GroundDetection groundDetection;


    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public bool isRightDirection;
    public float speed;

    private void Update()
    {
        if (isRightDirection && groundDetection.isGrounded)
        {
            rigidbody.velocity = Vector2.right * speed;
            if (transform.position.x > rightBorder.transform.position.x)
            {
                isRightDirection = !isRightDirection;
                spriteRenderer.flipX = false;
            }
        }
        else if (groundDetection.isGrounded)
        {
            rigidbody.velocity = Vector2.left * speed;
            if (transform.position.x < leftBorder.transform.position.x)
            {
                isRightDirection = !isRightDirection;
                spriteRenderer.flipX = true;
            }
        }

        animator.SetFloat("Speed", Mathf.Abs(speed));
    }
}

