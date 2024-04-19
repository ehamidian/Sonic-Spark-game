using UnityEngine;

public class HitEnemy : MonoBehaviour
{
    [SerializeField] EnemyBar enemyBar;
    [SerializeField] HealthBar healthBar;

    [Header("Stats")]
    public int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
        enemyBar.SetHealth(damage);

        if (health <= 0)
        {
            Destroy(gameObject);
            SoundEFManager.instance.PlaySoundEffect("enemy");
        }

        if (enemyBar.IsEnemyDead())
        {
            healthBar.SetHealthOnEnemy(10, false);
            UpdateHealthBar();
        }
    }

    private void UpdateHealthBar()
    {
        enemyBar.ResetHealth(100);
    }
}