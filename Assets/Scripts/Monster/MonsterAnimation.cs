using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetMoving(bool isMoving)
    {
        if (animator != null)
        {
            animator.SetBool("isMoving", isMoving);
        }
    }

    public void TriggerAttack()
    {
        if (animator != null)
        {
            animator.SetTrigger("attack");
        }
    }

    public void TriggerDeath()
    {
        if (animator != null)
        {
            animator.SetTrigger("death");
        }
    }
    public void ChangeColor(Color newColor)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = newColor;
        }
    }

    public void ResetColor()
    {
        ChangeColor(Color.white);
    }
}
