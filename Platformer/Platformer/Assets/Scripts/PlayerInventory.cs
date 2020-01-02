using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int coinsCount;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Coin"))
        {
            coinsCount++;
            Destroy(col.gameObject);
        }

        if (col.gameObject.CompareTag("First aid kit"))
        {
            Health healthKit = col.gameObject.GetComponent<Health>();
            Health health = this.gameObject.GetComponent<Health>();
            health.SetHealth(healthKit.health);
            Destroy(col.gameObject);
        }
    }
}
