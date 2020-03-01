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

    //[SerializeField] private GameObject arrow;
    [SerializeField] private Arrow arrow;
    [SerializeField] private Transform arrowSpawPoint;
    [SerializeField] private bool isReadyToShoot = true;
    [SerializeField] private float coolDownShot;
    [SerializeField] private int arrowCount = 3;
    private Arrow prefab;
    private List<Arrow> arrowPool;



    #region Singleton
    public static Player Instance { get; set; }
    #endregion
    private void Awake() // Awake -> Start -> Update -> FixUpdate -> PlayUpdate
    {
        Instance = this;
    }

    private void Start()
    {
        arrowPool = new List<Arrow>();
        for (int i = 0; i < arrowCount; i++)
        {
            var arrowTemp = Instantiate(arrow, arrowSpawPoint); //.position , Quaternion.identity);
            arrowPool.Add(arrowTemp);
            arrowTemp.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //if (col.gameObject.CompareTag("Coin"))
        if (GameManager.Instance.coinContainer.ContainsKey(col.gameObject))
        {
            PlayerInventory.Instance.CoinsCount++;
            //Destroy(col.gameObject);
            var coin = GameManager.Instance.coinContainer[col.gameObject];
            coin.StartDestroy();
            Debug.Log("Player после StartDestroy");
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
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

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
            StartCoroutine(CoolDownShot());
        }
    }

    public void MakeShoot()
    {
        prefab = GetArrowFromPool();
        prefab.SetImpulse
             (Vector2.right, transform.lossyScale.x * force, this);
        // (Vector2.right, spriteRenderer.flipX ? -force : force, gameObject);
    }

    private IEnumerator CoolDownShot()
    {
        isReadyToShoot = false;
        yield return new WaitForSeconds(coolDownShot);
        isReadyToShoot = true;
        yield break;
    }

    private Arrow GetArrowFromPool()
    {
        if (arrowPool.Count > 0)
        {
            var arrowTemp = arrowPool[0];
            arrowPool.Remove(arrowTemp);
            arrowTemp.gameObject.SetActive(true);
            arrowTemp.transform.parent = null;
            arrowTemp.transform.position = arrowSpawPoint.transform.position;
            return arrowTemp;
        }

        return Instantiate
                    (arrow, arrowSpawPoint.position, Quaternion.identity);
    }

    public void ReturnArrowToPool(Arrow arrowTemp)
    {
        if (!arrowPool.Contains(arrowTemp))
        {
            arrowPool.Add(arrowTemp);
            arrowTemp.transform.parent = arrowSpawPoint;
            arrowTemp.transform.position = arrowSpawPoint.transform.position;
            arrowTemp.gameObject.SetActive(false);
        }
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
