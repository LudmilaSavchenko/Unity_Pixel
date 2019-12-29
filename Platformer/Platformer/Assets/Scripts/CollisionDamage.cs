﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public int damage;
    public string collisionTag;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(collisionTag))
        {
            //Debug.Log($"{col.gameObject.tag} Взаимодействие с правильным объектом");
            Health health = col.gameObject.GetComponent<Health>();
            health.TakeHit(damage);
        }
    }
}
