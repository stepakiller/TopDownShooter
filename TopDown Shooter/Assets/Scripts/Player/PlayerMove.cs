using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    
    [Header("Dash")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDistance;
    [SerializeField] float dashCooldown;

    private bool isDashing = false;
    private float dashTimer = 0f;
    private float lastDashTime = -Mathf.Infinity;
    private Vector3 dashDirection;
    private CharacterController characterController;
    private float horizontal;
    private float vertical;
    private float currentSpeed;
    private float dashDuration => dashDistance / dashSpeed;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        if (!isDashing)
        {
            if (Input.GetKey(Settings.runKey))
            {
                if (Input.GetKeyDown(Settings.runKey) && CanDash()) Dash();
                currentSpeed = runSpeed;
            }
            else currentSpeed = walkSpeed;

            Vector3 playerMove = new Vector3(horizontal, 0f, vertical) * currentSpeed * Time.deltaTime;
            characterController.Move(playerMove);
        }
        else HandleDash();
    }

    bool CanDash()
    {
        return Time.time - lastDashTime >= dashCooldown && !isDashing;
    }

    void Dash()
    {
        isDashing = true;
        dashTimer = 0f;
        lastDashTime = Time.time;
        
        bool hasInput = Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f;
        dashDirection = hasInput ? new Vector3(horizontal, 0, vertical).normalized : transform.forward;
    }

    void HandleDash()
    {
        dashTimer += Time.deltaTime;
        
        if (dashTimer < dashDuration)characterController.Move(dashDirection * dashSpeed * Time.deltaTime);
        else isDashing = false;
    }
}
