using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public float speed;
    public float force;
    public Rigidbody2D rigidbody;
    private bool isGrounded = false;
    public Transform groundCheck;
    private float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float minimalHeight;
    public bool isCheatMode;


    float squareArea;
    float rectangleArea;
    float circleArea;

    float squareSide = 2.4f;
    float rectangleSideA = 3.3f;
    float rectangleSideB = 4.2f;
    float circleRadius = 5.1f;

    int applesCount = 3;
    int orangesCount = 5;
    int tomaoesCount = 2;

    long moonDistance = 300000000000;
    int paperFold=0;
    long paperDistance=1;

    void Start()
    {
        //1
        squareArea = Mathf.Pow(squareSide, 2);
        rectangleArea = rectangleSideA * rectangleSideB;
        circleArea = Mathf.PI * Mathf.Pow(circleRadius, 2);

        Debug.Log($"Square area is {squareArea};\nRectangle area is {rectangleArea};\nCircle area is {circleArea};");

        //2
        if (applesCount >= orangesCount && applesCount >= tomaoesCount)
        {
            if (orangesCount > tomaoesCount)
            {
                Debug.Log("apples, oranges, tomatoes.");
            }
            else
            {
                Debug.Log("apples, tomatoes, oranges.");
            }
        }
        else if (orangesCount >= applesCount && orangesCount >= tomaoesCount)
        {
            if (applesCount > tomaoesCount)
            {
                Debug.Log("oranges, apples, tomatoes.");
            }
            else
            {
                Debug.Log("oranges, tomatoes, apples.");
            }
        }
        else
        {
            if (applesCount > orangesCount)
            {
                Debug.Log("tomatoes, apples, oranges.");
            }
            else
            {
                Debug.Log("tomatoes, oranges, apples.");
            }
        }

        //3
        while (paperDistance < moonDistance)
        {
            paperDistance *= 2;
            paperFold++;
        }

        Debug.Log($"The paper needs to be folded {paperFold} times");

    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
    }

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

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
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
