using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]private int health;
    public int HealthPoint
    {
        get { return health; }
        set
        {
            if (value > 0)
                health = value;
        }
    }

    private void start()
    {
        Debug.Log(Player.Instance.isCheatMode);
    }

    public void TakeHit(int damage)
    {
        health -= damage;

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
