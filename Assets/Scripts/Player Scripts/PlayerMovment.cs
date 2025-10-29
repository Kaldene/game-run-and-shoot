using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
        
    private float horizontalInput;
    
    private bool IsFacingRight = true;
    private bool isMoving;
    private bool isRunning;

    private PlayerCore core;
    [SerializeField] PlayerChekGround chekGround;
    private void Awake()
    {
        core = GetComponent<PlayerCore>();
        chekGround = GetComponent<PlayerChekGround>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        isMoving = IsMoving();
        isRunning = Input.GetKey(KeyCode.LeftShift) && isMoving && chekGround.isGrounded;
        
        Move();
        HandleFlip();
        UpdateAnimations();
    }

    private void Move()
    {
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        core.rb.linearVelocity = new Vector2(horizontalInput * currentSpeed, core.rb.linearVelocity.y);
    }
    
    private void UpdateAnimations()
    {
        if (core.animator != null)
        {
            core.animator.SetBool("Walk", isMoving);
            core.animator.SetBool("isRunning", isRunning);
        }
    }
    
    #region FLiplogic   
    
    private void HandleFlip()
    {
        if(horizontalInput == 0) return;
        bool shouldFlip = horizontalInput > 0;
        if (shouldFlip != IsFacingRight)
        {
            Flip();
        }
    }
    
    private void Flip()
    {
        IsFacingRight = !IsFacingRight;
        Vector3 newScale = transform.localScale;
        newScale.x = Mathf.Abs(newScale.x) * (IsFacingRight ? 1 : -1);
        transform.localScale = newScale;
    }
    
    #endregion    

    private bool IsMoving()
    {
        return Mathf.Abs(horizontalInput) > Mathf.Epsilon;
    }
}