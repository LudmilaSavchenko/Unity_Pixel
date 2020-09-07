using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image health;
    private float healthValue;
    private Player player;

    void Start()
    {
        player = Player.Instance; // FindObjectOfType<Player>();
    }

    
    void Update()
    {
        health.fillAmount = player.Health.CurrentHealth / 100.0f; 
    }
}
