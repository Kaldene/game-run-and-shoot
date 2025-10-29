using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerCore : MonoBehaviour
{
    public static PlayerCore instance {get; private set;}

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public BoxCollider2D boxCollider;
    [HideInInspector] public Animator animator;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
    }
}
