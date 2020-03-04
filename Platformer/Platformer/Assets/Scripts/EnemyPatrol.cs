using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject leftBorder;
    public GameObject rightBorder;
    [SerializeField]private Rigidbody2D rigidbody;
    public GroundDetection groundDetection;


    [SerializeField]private SpriteRenderer spriteRenderer;
    [SerializeField]private CollisionDamage collisionDamage;
    [SerializeField]private Animator animator;

    public bool isRightDirection;
    [SerializeField] private float speed;

    public float Speed
    {
        get { return speed; }
        set
        {
            if (value > 0.5)
                speed = value;
        }
    }

    private void Update()
    {
        if (groundDetection.isGrounded)
        {
            if (transform.position.x > rightBorder.transform.position.x || collisionDamage.Direction < 0)
                isRightDirection = false;
            else if (transform.position.x < leftBorder.transform.position.x || collisionDamage.Direction > 0)
                isRightDirection = true;
            rigidbody.velocity = isRightDirection ? Vector2.right : Vector2.left;
            rigidbody.velocity *= speed;
        }

        if (rigidbody.velocity.x > 0)
            spriteRenderer.flipX = false;
        else { spriteRenderer.flipX = true; }

        animator.SetFloat("Speed", Mathf.Abs(speed));
    }
}

