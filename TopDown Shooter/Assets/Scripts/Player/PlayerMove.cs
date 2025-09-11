using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float healingSpeed;

    [Header("Dash")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDistance;
    [SerializeField] float dashCooldown;

    public static PlayerMove Instance { get; private set; }
    bool isDashing = false;
    float dashTimer = 0f;
    float lastDashTime = -Mathf.Infinity;
    Vector3 dashDirection;
    CharacterController characterController;
    float horizontal;
    float vertical;
    float currentSpeed;
    float dashDuration => dashDistance / dashSpeed;

    void Start()
    {
        Instance = this;
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
                MedKitController.Instance.CancelHealing();
                if (Input.GetKeyDown(Settings.runKey) && CanDash()) Dash();
                currentSpeed = runSpeed;
            }
            if (MedKitController.Instance.isHealing)
            {
                currentSpeed = healingSpeed;
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

        if (dashTimer < dashDuration) characterController.Move(dashDirection * dashSpeed * Time.deltaTime);
        else isDashing = false;
    }
}
