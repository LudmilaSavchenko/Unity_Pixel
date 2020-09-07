using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health;
    public int CurrentHealth {get { return health; }}
    [SerializeField] private Animator animator;
    public int HealthPoint
    {
        get { return health; }
        set
        {
            if (value > 0)
                health = value;
        }
    }
    
    private void Start()
    {
        //Debug.Log(Player.Instance.isCheatMode);
        GameManager.Instance.healthContainer.Add(gameObject, this);
    }

    public void TakeHit(int damage)
    {
        health -= damage;
        if (animator != null)
            animator.SetTrigger("TakeHit");
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    public void SetHealth (int bonusHealth)
    {
        health += bonusHealth;

        //if (health > 100)
        //{
        //    health = 100;
        //}
    }
}
