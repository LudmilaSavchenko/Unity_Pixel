using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, IObjectDestroyer
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private TriggerDamage triggerDamage;
    [SerializeField] private float force;
    [SerializeField] private float lifeTime;

    public float Force
    {
        get { return force; }
        set { force = value; }
    }
    private Player player;

    public void Destroy(GameObject gameObject)
    {
        player.ReturnArrowToPool(this);
    }

    public void SetImpulse(Vector2 direction, float forcePerson, Player player)
    {
        this.player = player;
        triggerDamage.Init(this);
        triggerDamage.Parent = player.gameObject;
        rigidbody.AddForce(direction * forcePerson * force, ForceMode2D.Impulse);

        if (forcePerson < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        StartCoroutine(StartLife());
    }

    private IEnumerator StartLife()
    {   
        yield return new WaitForSeconds(lifeTime);
        Debug.Log("Фигня001");
        Destroy(gameObject);
        yield break;
    }
}
