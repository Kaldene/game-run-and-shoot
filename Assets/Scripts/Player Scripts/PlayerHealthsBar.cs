using System;
using UnityEngine;

public class PlayerHealthsBar : MonoBehaviour
{
    [SerializeField] public float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    [SerializeField] private string[] damagetag = { "Enemy" };
    
    private bool isDead;
    
    private PlayerCore core;
    private UIHealthBarPlayer healthBar;
    private LevelManager levelManager;
    public static  PlayerHealthsBar instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        
        currentHealth = maxHealth;
        
        core = GetComponent<PlayerCore>();
        if(core == null) return; 
        
        healthBar = GetComponent<UIHealthBarPlayer>();
        if(healthBar == null) return; 
        
        UpdateHealthBar();
    }

    private void Update()
    {
        CheckDie();
    }
    
    public void TakeDamage(float damage)
    {
        if (core?.animator != null)
        {
            core.animator.SetTrigger("hurt"); 
        }

        currentHealth -= damage;
        Debug.Log($"[PlayerHealth] HP после урона: {currentHealth}/{maxHealth}");
        
        UpdateHealthBar();
        CheckDie();
    }

    public void UpdateHealthBar()
    {
        if (UIHealthBarPlayer.Instance != null)
        {
            float healthPercent = currentHealth / maxHealth;
            UIHealthBarPlayer.Instance.SetValue(healthPercent);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isDead) return;

        foreach (string tag in damagetag )
        {

            if (other.gameObject.CompareTag(tag))
            {
                if (core.animator != null && core != null)
                {
                    core.animator.SetTrigger("hurt"); 
                }
            }

            TakeDamage(5f);
            healthBar.SetValue(currentHealth / maxHealth); 
            break;
        }
        CheckDie();
    }

    private void CheckDie()
    {
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        if (currentHealth <= 0)
        {
            isDead = true;
            if (core?.animator != null)
            {
                core.animator.SetTrigger("Die");
            }
            
            if(TryGetComponent<Rigidbody2D>(out var rb))
            {
                core.rb.linearVelocity = Vector2.zero;
                core.rb.gravityScale = 1;
                core.rb.constraints = RigidbodyConstraints2D.None;
            }
            
            var colliders = GetComponents<Collider2D>();
            foreach (var collider in colliders)
            {
                collider.enabled = false;
            }
            Destroy(gameObject,0.5f);
            
            LevelManager.instance.PlayerDied();
        }
    }
}
