using System.Collections;
using UnityEngine;

public class MonsterAI : MonoBehaviour, IDamageable
{
    public Transform target;
    public GameObject healthBarPrefab;
    private GameObject healthBarInstance;
    private Vector3 healthBarOffset = new Vector3(-2f, 0.8f, 0);

    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    [SerializeField] private HealthMonster healthMonster; // Gán thủ công trong Inspector
    [SerializeField] private GameObject deathEffectPrefab; // Hiệu ứng chết

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBarPrefab != null)
        {
            healthBarInstance = Instantiate(healthBarPrefab, transform.position + healthBarOffset, Quaternion.identity);
            healthBarInstance.transform.SetParent(transform);
        }
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime);
        }

        if (healthBarInstance != null)
        {
            healthBarInstance.transform.position = transform.position + healthBarOffset;
        }
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
        ChangeColor(Color.white); // Hoặc màu gốc của Monster
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Monster bị bắn, máu còn lại: " + currentHealth);
        ChangeColor(Color.red);

        ChangeColor(Color.red);

        if (healthMonster != null)
            healthMonster.UpdateHeath(currentHealth, maxHealth);

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
        Debug.Log("Monster chết!");

        Destroy(healthBarInstance);

        if (deathEffectPrefab != null)
        {
            GameObject deathEffect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(deathEffect, 0.75f); 
        }

        Destroy(gameObject);

    }

};
