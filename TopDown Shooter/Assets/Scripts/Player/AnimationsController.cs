using System.Collections;
using UnityEngine;

public class AnimationsController : MonoBehaviour
{
    [HideInInspector] public Animator anim;
    bool isHands;
    bool isShoot;
    
    void Start() => anim = GetComponent<Animator>();

    void Update()
    {
        float currentLayerWeight = anim.GetLayerWeight(1);
        float targetLayerWeight;
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(1);
    
        if(stateInfo.normalizedTime < 1.0f || anim.IsInTransition(1)) 
            targetLayerWeight = 1f;
        else 
            targetLayerWeight = 0f;
        
        anim.SetLayerWeight(1, targetLayerWeight);
    }

    public void SetMovementDirection(float horizontal, float vertical)
    {
        bool isMoving = Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f;
        anim.SetBool("isMoving", isMoving);
        
        if(isMoving)
        {
            anim.SetFloat("MoveX", horizontal);
            anim.SetFloat("MoveY", vertical);
        }
        else
        {
            float currentX = anim.GetFloat("MoveX");
            float currentY = anim.GetFloat("MoveY");
            
            anim.SetFloat("MoveX", Mathf.Lerp(currentX, 0f, Time.deltaTime * 10f));
            anim.SetFloat("MoveY", Mathf.Lerp(currentY, 0f, Time.deltaTime * 10f));
        }
    }
    public void IsMoving(bool ToF) => anim.SetBool("isMoving", ToF);
    public void SetForwardMovement(float amount) => anim.SetFloat("MoveY", amount);
    public void SetRightMovement(float amount) => anim.SetFloat("MoveX", amount);
    
    public void SetDirection(string direction)
    {
        switch(direction.ToLower())
        {
            case "forward":
                anim.SetFloat("MoveX", 0f);
                anim.SetFloat("MoveY", 1f);
                anim.SetBool("isMoving", true);
                break;
            case "backward":
                anim.SetFloat("MoveX", 0f);
                anim.SetFloat("MoveY", -1f);
                anim.SetBool("isMoving", true);
                break;
            case "left":
                anim.SetFloat("MoveX", -1f);
                anim.SetFloat("MoveY", 0f);
                anim.SetBool("isMoving", true);
                break;
            case "right":
                anim.SetFloat("MoveX", 1f);
                anim.SetFloat("MoveY", 0f);
                anim.SetBool("isMoving", true);
                break;
            default:
                anim.SetBool("isMoving", false);
                anim.SetFloat("MoveX", 0f);
                anim.SetFloat("MoveY", 0f);
                break;
        }
    }

    public void IsHands(bool ToF) => isHands = ToF;
    public void IsShoot(bool ToF) => isShoot = ToF;

    public void GetHit()
    {
        if(isHands) anim.Play("Punch");
        else if(isShoot) anim.SetBool("isShooting", true);
    }
}
