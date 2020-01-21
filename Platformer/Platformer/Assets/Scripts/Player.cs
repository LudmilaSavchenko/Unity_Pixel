using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField]private float speed;
    public float Speed
    {
        get { return speed; }
        set
        {
            if (value > 0.5)
                speed = value;
        }
    }

    [SerializeField]private float force;
    public float Force
    {
        get { return force; }
        set
        {
            if (value > 0.5)
                force = value;
        }
    }

    public Rigidbody2D rigidbody;

    [SerializeField]private float minimalHeight;
    public float MinimalHeight
    {
        get { return minimalHeight; }
        set
        {
            if (value < 0 && value > -10)
                minimalHeight = value;
        }
    }

    public bool isCheatMode;
    public GroundDetection groundDetection;
    private Vector3 direction;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private bool isJumping;

     #region Singleton
    public static Player Instance { get; set; }
    #endregion
    private void Awake() // Awake -> Start -> Update -> FixUpdate -> PlayUpdate
    {
        Instance = this;
    }

    private void Start()
    {
        CatClass cat1 = new CatClass();
        CatClass cat2 = new CatClass("Kesha", 1, 2, 1, 2);
        //cat.age = 13;
        cat1.Meow();
        cat2.Meow();

        Bus bus = new Bus();
        bus.Beep();
        bus.BeepMsg();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Coin"))
        {
            PlayerInventory.Instance.CoinsCount++;
            Destroy(col.gameObject);
        }

        if (col.gameObject.CompareTag("First aid kit"))
        {
            Health healthKit = col.gameObject.GetComponent<Health>();
            Health health = this.gameObject.GetComponent<Health>();
            health.SetHealth(healthKit.HealthPoint);
            Destroy(col.gameObject);
        }
    }

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

public class CatClass
{
    public string _name;
    public int _age;
    public int _height;
    public int _lengthTail;

    public int _weight { get; set; }

    public int age => _age;

    public CatClass() { }

    public CatClass(string name, int age, int height, int lengthTail, int weight)
    {
        _name = name;
        _age = age;
        _weight = weight;
        _height = height;
        _lengthTail = lengthTail;
    }

    public void Meow()
    {
        Debug.Log($"Имя: {_name}, возраст: {age}, вес: {_weight}, рост: {_height}, длина хвоста: {_lengthTail}.");
    }
}

public struct CatStruct
{
    public string name;
    public float age;
    public float weight;
    public float height;
    public float lengthTail;

    public void Meow()
    {
            Debug.Log($"Имя: {name}, возраст: {age}, вес: {weight}, рост: {height}, длина хвоста: {lengthTail}.");
    }
}

public abstract class Vehicle
{
    public string _name;
    public abstract void Beep();
    public virtual void BeepMsg()
    {
        Debug.Log("Бибип!");
    }
}

public class Bus: Vehicle
{
    public override void Beep()
    {
        base.BeepMsg();
    }

    public override void BeepMsg()
    {
        base.BeepMsg();
        Debug.Log("Автобусное бибип!!");
    }
}

public class Car: Vehicle
{
    public override void Beep()
    {
        Debug.Log("Машинное бибип");
    }
}

public class Tractor: Vehicle
{
    public override void Beep()
    {
        Debug.Log("Тракторное бибип");
    }
}
