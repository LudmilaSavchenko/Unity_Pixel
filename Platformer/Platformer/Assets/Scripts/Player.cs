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

    [SerializeField]private Rigidbody2D rigidbody;
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

    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform arrowSpawPoint;
    [SerializeField] private bool isReadyToShoot = true;
    [SerializeField] private float coolDownShot;


    #region Singleton
    public static Player Instance { get; set; }
    #endregion
    private void Awake() // Awake -> Start -> Update -> FixUpdate -> PlayUpdate
    {
        Instance = this;
    }

    private void Start()
    {
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
        {
            spriteRenderer.flipX = false;
            arrowSpawPoint.transform.position = new Vector2(transform.position.x + 1, transform.position.y);
        }
        if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
            arrowSpawPoint.transform.position = new Vector2(transform.position.x - 1, transform.position.y);
        }

        //arrowSpawPoint.transform.rotation = Quaternion.Euler(0, 180, 0);


        animator.SetFloat("Speed", Mathf.Abs(direction.x));

        CheckFall();
   }

    private void Update()
    {
        CheckShoot();
    }

    void CheckShoot()
    {
        if (Input.GetMouseButtonDown(0) && isReadyToShoot)  // 0  левая. 1 правая
        {
            animator.SetTrigger("CollisionDamage");
            //prefab.GetComponent<Arrow>().SetImpulse
               // (Vector2.right, spriteRenderer.flipX ? - force : force, gameObject);
            //MakeShoot();
            StartCoroutine(CoolDownShot());
        }
    }

    public void MakeShoot()
    {
        GameObject prefab =     Instantiate
                (arrow, arrowSpawPoint.position, Quaternion.identity);
        prefab.GetComponent<Arrow>().SetImpulse
                (Vector2.right, spriteRenderer.flipX ? -force : force, gameObject);
    }

    IEnumerator CoolDownShot()
    {
        isReadyToShoot = false;
        yield return new WaitForSeconds(coolDownShot);
        isReadyToShoot = true;
        yield break;
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

    //void DebugLog()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        StartCoroutine(Log());
    //    }
    //}

    //IEnumerator Log()
    //{
    //    for (int i = 0; i < 300; i++)
    //    {
    //        Debug.Log("сообщение");
    //        yield return new WaitForSeconds(1f);
    //        //yield return null; если считать по кадрам.
    //    }

    //    //yield return null;
    //    yield break;
    //}

}
