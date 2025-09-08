using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    CharacterController characterController;
    float horizontal;
    float vertical;
    float currentSpeed;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Input.GetKey(Settings.runKey)) currentSpeed = runSpeed;
        else currentSpeed = walkSpeed;

        Vector3 playerMove = new Vector3(horizontal, 0f, vertical) * currentSpeed * Time.deltaTime;

        characterController.Move(playerMove);
    }
}
