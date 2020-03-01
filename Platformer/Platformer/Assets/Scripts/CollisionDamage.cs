using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public int damage;
    [SerializeField]private string collisionTag= "Player";
    [SerializeField]private Animator animator;
    public SpriteRenderer spriteRenderer;
    private Health health;
    private float direction;
    public float Direction
    {
        get { return direction; }
    }

    private void OnCollisionStay2D(Collision2D col) //Stay - Enter
    {
        //health = col.gameObject.GetComponent<Health>();

        //if (health != null)

        if (GameManager.Instance.healthContainer.ContainsKey(col.gameObject))
        {
            var health = GameManager.Instance.healthContainer[col.gameObject];
            direction = col.transform.position.x - transform.position.x;
            animator.SetFloat("Direction", Mathf.Abs(direction));
        }

    }

    public void SetDamage()
   {
        if (health != null)
            health.TakeHit(damage);
        health = null;
        direction = 0;
        animator.SetFloat("Direction", 0f);
    }
}       
