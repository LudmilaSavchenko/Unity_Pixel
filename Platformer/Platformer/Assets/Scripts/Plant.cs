using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private int damage;
    [SerializeField] private float coolDownAttack;

    private Health health;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    private void OnCollisionEnter2D(Collision2D col) //Stay - Enter
    {
        if (GameManager.Instance.healthContainer.ContainsKey(col.gameObject))
        {  
            health = GameManager.Instance.healthContainer[col.gameObject];
            StartCoroutine(StartPlantLife());
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (GameManager.Instance.healthContainer.ContainsKey(col.gameObject))
        {
            animator.SetBool("isReadeToAttack", false);
            health = null;
        }
    }
    
    public void PlantAttack()
    {
        if (health!=null)
        {
            health.TakeHit(damage);
        }
    }

    private IEnumerator StartPlantLife()
    {
        yield return new WaitForSeconds(coolDownAttack);
        animator.SetBool("isReadeToAttack", true);
        yield break;
    }
}

