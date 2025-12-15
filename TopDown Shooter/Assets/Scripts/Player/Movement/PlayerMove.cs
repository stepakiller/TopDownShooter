using UnityEngine;
using UnityEngine.UI;
public class PlayerMove : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    [SerializeField] float healingSpeed;

    [Header("Stamina")]
    [SerializeField] Image staminaImg;
    [SerializeField] float maxstamina;
    float currentstamina;
    [SerializeField] float durationRegen;
    [SerializeField] float durationUse;
    bool isRunning;

    [Header("Crouch")]
    [SerializeField] float crouchSpeed;
    [SerializeField] float crouchHeight;
    float standHeight = 2.2f;
    Vector3 crouchCenter = new Vector3(0, 0.5f, 0);
    Vector3 standCenter = new Vector3(0, 1.1f, 0);
    bool isCrouch = false;


    [Header("Dash")]
    [SerializeField] float dashSpeed;
    public float dashDistance;
    [SerializeField] float dashCooldown;
    [SerializeField] Image cooldown;
    float dashDuration => dashDistance / dashSpeed;
    bool isDashing = false;
    Vector3 dashDirection;
    float dashTimer = 0f;
    float lastDashTime = -Mathf.Infinity;

    public static PlayerMove Instance { get; private set; }
    CharacterController characterController;
    float horizontal;
    float vertical;
    float currentSpeed;
    CapsuleCollider _collider;
    AnimationsController animCon;

    void Awake()
    {
        Instance = this;
        characterController = GetComponent<CharacterController>();
        _collider = GetComponent<CapsuleCollider>();
        animCon = GetComponentInChildren<AnimationsController>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 moveInput = new Vector3(horizontal, 0f, vertical);
        Vector3 localMove = transform.InverseTransformDirection(moveInput);
        animCon.SetMovementDirection(localMove.x, localMove.z);

        bool isMoving = Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f;
        
        if(isMoving)
        {
            animCon.IsMoving(true);
            
            if(Mathf.Abs(localMove.z) > Mathf.Abs(localMove.x))
            {
                if(localMove.z > 0) animCon.SetDirection("Forward");
                else animCon.SetDirection("Backward");
            }
            else
            {
                if(localMove.x > 0) animCon.SetDirection("Right");
                else animCon.SetDirection("Left");
            }
        }
        else animCon.IsMoving(false);


        if (!isDashing)
        {
            if (Input.GetKey(Settings.runKey) && currentstamina > 0 && !isCrouch)
            {
                MedKitController.Instance.CancelHealing();
                if (Input.GetKeyDown(Settings.runKey) && CanDash()) Dash();
                currentSpeed = runSpeed;
                isRunning = true;
                currentstamina -= durationUse;
                staminaImg.fillAmount = currentstamina / maxstamina;
            }
            else if (Input.GetKeyUp(Settings.runKey)) isRunning = false;
            else if (Input.GetKey(Settings.crouchtKey))
            {
                characterController.height = Mathf.Lerp(characterController.height, crouchHeight, 5f * Time.deltaTime);
                characterController.center = Vector3.Lerp(characterController.center, crouchCenter, 5f * Time.deltaTime);
                currentSpeed = crouchSpeed;
                isCrouch = true;
            }
            else if (!CanStand())
            {
                characterController.height = Mathf.Lerp(characterController.height, standHeight, 5f * Time.deltaTime);
                characterController.center = Vector3.Lerp(characterController.center, standCenter, 5f * Time.deltaTime);
                currentSpeed = walkSpeed;
                isCrouch = false;
            }
            if (MedKitController.Instance.isHealing)
            {
                currentSpeed = healingSpeed;
            }

            Vector3 playerMove = new Vector3(horizontal, 0f, vertical) * currentSpeed * Time.deltaTime;
            characterController.Move(playerMove);
            transform.position = new Vector3(transform.position.x, -0.05f, transform.position.z);

            if (currentstamina <= maxstamina && !isRunning)
            {
                currentstamina += durationRegen;
                staminaImg.fillAmount = currentstamina / maxstamina;
                if (currentstamina >= maxstamina)
                {
                    currentstamina = maxstamina;
                    staminaImg.fillAmount = currentstamina / maxstamina;
                }
            }
        }
        else HandleDash();

        float cooldownProgress = Mathf.Clamp01((Time.time - lastDashTime) / dashCooldown);
        
        if (cooldownProgress >= 1f) cooldown.fillAmount = 1f;
        else cooldown.fillAmount = cooldownProgress;
    }

    bool CanDash()
    {
        return Time.time - lastDashTime >= dashCooldown && !isDashing;
    }

    bool CanStand()
    {
        float raycastLength = standHeight - crouchHeight + 0.1f;
        Vector3 topOfHead = transform.position + characterController.center + Vector3.up * (characterController.height / 2);

        if (Physics.Raycast(topOfHead, Vector3.up, out RaycastHit hit, raycastLength)) return true;
        return false;
    }

    void Dash()
    {
        _collider.enabled = false;
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
        else
        {
            isDashing = false;
            _collider.enabled = true;
            isRunning = false;
        }
    }
}
