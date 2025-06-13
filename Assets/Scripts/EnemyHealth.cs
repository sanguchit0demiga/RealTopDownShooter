using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;

    public HealthBar healthBar;
    public PowerUps powerUpDrops;
    public GameObject bloodSplashPrefab;
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

    public void TakeDamage(float damage)
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
        if (bloodSplashPrefab != null)
        {
            Instantiate(bloodSplashPrefab, transform.position, Quaternion.identity);
        }
        if (GameManager.Instance != null)
        {
            GameManager.Instance.EnemyKilled();
        }
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
