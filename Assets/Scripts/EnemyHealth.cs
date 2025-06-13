using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public HealthBar healthBar;
    public PowerUps powerUpDrops; 
    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxHealth = maxHealth;
            healthBar.currentHealth = currentHealth;
            healthBar.UpdateBar();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (healthBar != null)
        {
            healthBar.currentHealth = currentHealth;
            healthBar.UpdateBar();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        DropPowerUp();
    }
    void DropPowerUp()
    {
        if (powerUpDrops == null || powerUpDrops.posiblesDrops == null || powerUpDrops.posiblesDrops.Length == 0)
            return;

        float roll = Random.value; 
        float acumulado = 0f;

        foreach (var drop in powerUpDrops.posiblesDrops)
        {
            acumulado += drop.probabilidad;
            if (roll <= acumulado)
            {
                Instantiate(drop.objeto, transform.position, Quaternion.identity);
                break;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
                Destroy(other.gameObject);
            }
        }
    }
}
