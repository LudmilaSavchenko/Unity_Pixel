using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]private int coinsCount;
    public int CoinsCount
    {
        get { return coinsCount; }
        set
        {
            if (value > 0)
                coinsCount = value;
        }
    }

    #region Singleton
    public static PlayerInventory Instance { get; set; }
    #endregion
    private void Awake() // Awake -> Start -> Update -> FixUpdate -> PlayUpdate
    {
        Instance = this;
    }

    //private void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.gameObject.CompareTag("Coin"))
    //    {
    //        coinsCount++;
    //        Destroy(col.gameObject);
    //    }

    //    if (col.gameObject.CompareTag("First aid kit"))
    //    {
    //        Health healthKit = col.gameObject.GetComponent<Health>();
    //        Health health = this.gameObject.GetComponent<Health>();
    //        health.SetHealth(healthKit.health);
    //        Destroy(col.gameObject);
    //    }
    //}
}
