using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float knockbackForce = 5f;

    [SerializeField] private PlayerHealthsBar playerHealthsBar;

    private void Awake()
    {
        playerHealthsBar = GetComponent<PlayerHealthsBar>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
          
            PlayerHealthsBar playerHealthsBar = collision.GetComponent<PlayerHealthsBar>();
        
            if (playerHealthsBar != null)
            {
                // Наносим урон
                playerHealthsBar.TakeDamage(damage);
            
                // Создаем направление для отталкивания
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            
                // Применяем отталкивание к игроку
                Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    playerRb.linearVelocity = Vector2.zero;
                    playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
                }
            }
        }
    }
    
}

