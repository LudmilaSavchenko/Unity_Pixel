using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }
    #endregion

    public Dictionary<GameObject, Health> healthContainer;
    public Dictionary<GameObject, Coin> coinContainer;
    public Dictionary<GameObject, BuffReciever> buffRecieverConteiner;

    private void Awake()
    {
        Instance = this;
        healthContainer = new Dictionary<GameObject, Health>();
        coinContainer = new Dictionary<GameObject, Coin>();
        buffRecieverConteiner = new Dictionary<GameObject, BuffReciever>();
    }

    //private void Start()
    //{
    //    var healthObjects = FindObjectsOfType<Healh>(); - вариант 1
    //    foreach ( var health in healthObjects)
    //    {
    //        healthContainer.Add(health.gameObject, health);
    //    }
    //}
}
