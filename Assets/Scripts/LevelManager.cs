using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    
    [Header("Respawn Settings")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject playerPrefab;
    
    private GameObject currentPlayer;
    private float currentHealth;
    
    public System.Action<GameObject> OnPlayerRespawned;
    
    [SerializeField] private CameraPInput Camera;
    [SerializeField] private  PlayerHealthsBar healthBar;

    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        Camera = CameraPInput.instance;
        if(Camera == null) return;
        
        healthBar = PlayerHealthsBar.instance;
        if(healthBar == null) return;
    }

    private void Start()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        if (spawnPoint == null) return;
          
        if (playerPrefab == null) return;
           
        if (currentPlayer != null)
        {
            Destroy(currentPlayer);
        }
        
        currentPlayer = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        OnPlayerRespawned?.Invoke(currentPlayer);
        
        UpdateCameraTarget();
        UpdateUIHealth();
    }

    public void RespawnPlayer()
    {
        SpawnPlayer();
    }

    // Метод для смены точки спавна 
    public void SetSpawnPoint(Transform newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }

    // Для вызова из других скриптов 
    public void PlayerDied()
    {
        
        Invoke(nameof(RespawnPlayer), 1f);
    }
    
    private void UpdateCameraTarget()
    {
        Camera = FindObjectOfType<CameraPInput>();
        if (Camera != null)
        {
            Camera.SetTarget(currentPlayer.transform);
        }
    }

    //не работает/ нарушена логика
    private void UpdateUIHealth()
    {
        if (healthBar != null)
        {


            currentHealth = healthBar.maxHealth;
        }

        if (healthBar != null)
        {
            float healthPercent = currentHealth / healthBar.maxHealth;
            UIHealthBarPlayer.Instance.SetValue(healthPercent);
        }
    }
}