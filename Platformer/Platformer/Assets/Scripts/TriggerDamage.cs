using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    [SerializeField] private int damage;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    [SerializeField] private bool isDestroyingAfterCollision;
    [SerializeField] private IObjectDestroyer destroyer;
    private GameObject parent;
    public GameObject Parent
    {
        get { return parent; }
        set { parent = value; }
    }

    public void Init(IObjectDestroyer destroyer)
    {
        this.destroyer = destroyer;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Фигня1");
        if (col.gameObject == parent)
            return;

        var health = col.gameObject.GetComponent<Health>();
        Debug.Log(col.gameObject.name);
        Debug.Log("Фигня2");

        if (health != null)
        {
            health.TakeHit(damage);
        }

        if (isDestroyingAfterCollision)
        {
            if (destroyer == null)
                Destroy(gameObject);
            else destroyer.Destroy(gameObject);
        }
    }
}

public interface IObjectDestroyer
{
    void Destroy(GameObject gameObject);
}