using UnityEngine.UI;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Text coinsText;
    [SerializeField] private int coinsCount;
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

    private void Start()
    {
        coinsText.text = "Количество монет: 0";
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //if (col.gameObject.CompareTag("Coin"))
        if (GameManager.Instance.coinContainer.ContainsKey(col.gameObject))
        {
            CoinsCount++;
            coinsText.text = "Количество монет: " + coinsCount;
           //Destroy(col.gameObject);
           var coin = GameManager.Instance.coinContainer[col.gameObject];
            coin.StartDestroy();
            Debug.Log("Player после StartDestroy");
        }

        if (col.gameObject.CompareTag("First aid kit"))
        {
            Health healthKit = col.gameObject.GetComponent<Health>();
            Health health = this.gameObject.GetComponent<Health>();
            health.SetHealth(healthKit.HealthPoint);
            Destroy(col.gameObject);
        }
    }

}
