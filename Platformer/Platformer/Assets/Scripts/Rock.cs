using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private int damage;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    private Health health;

    private void OnCollisionEnter2D(Collision2D col) //Stay - Enter
    {
        if (GameManager.Instance.healthContainer.ContainsKey(col.gameObject))
        {
            health = GameManager.Instance.healthContainer[col.gameObject];
            health.TakeHit(damage);
        }
    }

}
