using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public float speed;
    public float force;
    public Rigidbody2D rigidbody;

    //private bool isGrounded = false;
    //public Transform groundCheck;
    //private float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float minimalHeight;
    public bool isCheatMode;
    public SpriteRenderer[] renderers;
    public GroundDetection groundDetection;

    // void FixedUpdate()
    //{
    //    isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
    //}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }

        if (Input.GetKeyDown(KeyCode.Space) && groundDetection.isGrounded)
        {
            rigidbody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }

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
