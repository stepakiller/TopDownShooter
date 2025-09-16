using UnityEngine;
using UnityEngine.UI;
public class PlayerMove : MonoBehaviour
{
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
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
    [SerializeField] float dashDistance;
    [SerializeField] float dashCooldown;
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
            else if (MedKitController.Instance.isHealing)
            {
                currentSpeed = healingSpeed;
            }

            Vector3 playerMove = new Vector3(horizontal, 0f, vertical) * currentSpeed * Time.deltaTime;
            characterController.Move(playerMove);
            transform.position = new Vector3(transform.position.x, 0.085f, transform.position.z);
            
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
