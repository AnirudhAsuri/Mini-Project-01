using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EnemyStats : CharacterStats
    {
        public BossHealthBar bossHealthBar; // Reference to boss health UI
        public bool isBoss = false; // Mark if this enemy is a boss

        private Animator animator;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();

            if (isBoss) // Only assign health bar if this is a boss
            {
                bossHealthBar = FindObjectOfType<BossHealthBar>();
            }
        }

        private void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;

            if (isBoss && bossHealthBar != null)
            {
                bossHealthBar.SetMaxHealth(maxHealth);
                bossHealthBar.SetCurrentHealth(currentHealth);
            }
        }

        private int SetMaxHealthFromHealthLevel()
        {
            return healthLevel * 10; // Modify this for balance if needed
        }

        public void TakeDamage(int damage)
        {
            if (isDead)
                return;

            currentHealth -= damage;

            if (isBoss && bossHealthBar != null)
            {
                bossHealthBar.SetCurrentHealth(currentHealth);
            }

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead)
                return;

            isDead = true;
            animator.Play("Death");

            // Disable Collider after death
            if (GetComponent<Collider>())
            {
                GetComponent<Collider>().enabled = false;
            }

            if (isBoss && bossHealthBar != null)
            {
                bossHealthBar.gameObject.SetActive(false); // Hide boss health bar
            }

            Destroy(gameObject, 5f); // Destroy enemy after 5 seconds
        }
    }
}