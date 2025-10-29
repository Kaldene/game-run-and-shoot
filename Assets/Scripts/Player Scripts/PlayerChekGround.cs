using UnityEngine;

public class PlayerChekGround : MonoBehaviour
{
    [SerializeField] private Transform poinChekGround;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private float distance;
    
    [SerializeField ] public bool isGrounded;
    
    private PlayerCore core;

    private void Awake()
    {
        core = GetComponent<PlayerCore>();
    }

    private void Update()
    {
        isGrounded = ChekGround();
    }

    private bool ChekGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(poinChekGround.position,
            core.boxCollider.size,
            0f,
            Vector2.down,
            distance, GroundLayer);
        
        return hit.collider != null;
    }
}
