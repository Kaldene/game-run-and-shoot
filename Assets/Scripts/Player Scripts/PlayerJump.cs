using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] public PlayerChekGround chekGround;
    
    private bool isJumping;
    
    private PlayerCore core;

    private void Awake()
    {
        core = GetComponent<PlayerCore>();
    }

    private void Update()
    {
        Jump();
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (chekGround.isGrounded)
            {
                core.animator.SetTrigger("Jump");
                isJumping = true;
                
                LogicJump();
            }
        }

        if (chekGround.isGrounded)
        {
            isJumping = false;
        }
    }

    private void LogicJump()
    {
        core.rb.linearVelocity = new Vector2(core.rb.linearVelocity.x, 0);
        core.rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
    }
}
