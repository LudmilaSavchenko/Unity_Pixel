using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image health;
    [SerializeField] private float delta;
    private float healthValue;
    private float currentHealth;
    private Player player;

    void Start()
    {
        player = Player.Instance; // FindObjectOfType<Player>();
        //delta = player.Health.CurrentHealth / 100.0f;
    }

    
    void Update()
    {
        currentHealth = player.Health.CurrentHealth / 100.0f;
        if (currentHealth > healthValue)
            healthValue += delta;
        else if (currentHealth < healthValue)
            healthValue -= delta;
        else if (Mathf.Abs(currentHealth - healthValue) < delta)
            healthValue = currentHealth;

        //health.fillAmount = player.Health.CurrentHealth / 100.0f;
        health.fillAmount = healthValue;
    }
}
