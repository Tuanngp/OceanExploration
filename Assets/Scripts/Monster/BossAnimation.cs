using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetMoving(bool state)
    {
        animator.SetBool("isMoving", state);
    }

    public void TriggerAttack()
    {
        animator.SetTrigger("attack");
    }

    public void TriggerDeath()
    {
        animator.SetTrigger("death");
    }


    public void SetLowHealth(bool state)
    {
        animator.SetBool("isLowHealth", state);
    }
}
