using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MonsterAI : MonoBehaviour, IDamageable
{
    public Transform target;
    public float speed = 5f;
    private Animator animator;
    private bool isDead = false;
    private int maxHealth = 100;
    private int currentHealth = 100;
    private bool isMoving = false;
    private bool hasSpottedPlayer = false;
    public static int monstersKilled = 0;  // Biến static để theo dõi số quái bị giết
    public MonsterProgress monsterProgress;  // Để cập nhật tiến độ

    void Start()
    {
        // animator = GetComponent<Animator>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (animator == null) return;
        if (target != null)
        {
            if (!hasSpottedPlayer && IsTargetVisible())
            {
                hasSpottedPlayer = true;
                Debug.Log("Monster đã phát hiện tàu, bắt đầu đuổi theo!");
            }

            if (hasSpottedPlayer)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                isMoving = direction.magnitude > 0.01f;

                if (isMoving)
                {
                    MoveToTarget();
                    animator.SetBool("isMoving", true);
                }
                else
                {
                    animator.SetBool("isMoving", false);
                }
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void MoveToTarget()
    {
        if (target == null) return;

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (angle > 90f || angle < -90f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                angle += 180f;
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }



    private bool IsTargetVisible()
    {
        if (target == null) return false;

        float distance = Vector3.Distance(transform.position, target.position);
        return distance < 60f;
    }

    private void ChangeColor(Color newColor)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = newColor;
        }
    }

    private void ResetColor()
    {
        ChangeColor(Color.white);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Giới hạn trong khoảng 0 - maxHealth

        Debug.Log("Monster bị bắn, máu còn lại: " + currentHealth);
        ChangeColor(Color.red);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            Invoke(nameof(ResetColor), 0.2f);
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("death");

        monstersKilled++;
        if (monsterProgress != null)
        {
            monsterProgress.UpdateProgress(monstersKilled, FindObjectOfType<SpawnMonsters>().maxMonsters);
        }
        else
        {
            Debug.LogWarning("MonsterProgress chưa được gán!");
        }

        Debug.Log("Tiến độ giết quái: " + monstersKilled + "/" + FindObjectOfType<SpawnMonsters>().maxMonsters);

        Destroy(transform.root.gameObject, 0.75f);
    }
    // {
    //     isDead = true;
    //     animator.SetTrigger("death");
    //     if (OnMonsterKilled != null)
    //     {
    //         OnMonsterKilled(this);
    //     }
    //     else
    //     {
    //         Debug.LogWarning("OnMonsterKilled chưa có ai đăng ký!");
    //     }
    //     Destroy(transform.root.gameObject, 0.75f);
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("attack");
            Debug.Log("Monster's attacking");
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

}
