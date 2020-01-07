using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public int damage;
    public string collisionTag;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(collisionTag))
        {
            //Debug.Log($"{col.gameObject.tag} Взаимодействие с правильным объектом");
            Health health = col.gameObject.GetComponent<Health>();
            health.TakeHit(damage);
                       
            if (animator != null)
            {
                if (col.transform.position.x < transform.position.x && spriteRenderer != null)
                {
                    spriteRenderer.flipX = false;
                }
                else if (spriteRenderer != null)
                {
                    spriteRenderer.flipX = true;
                }

                animator.SetTrigger("Biting");
            }
               
        }
    }
}
