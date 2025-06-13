using UnityEngine;
using UnityEngine.AI;

public class EnemyShooter : MonoBehaviour
{
    public Transform playerTransform;
    private NavMeshAgent navMeshAgent;

    [SerializeField] private float refreshRate = 0.5f;
    public float fireRate = 2f;
    private float fireTimer;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public ParticleSystem muzzleFlash;
    void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player")?.transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        fireTimer = fireRate;
        InvokeRepeating(nameof(UpdateDestination), 0f, refreshRate);
    }

    void Update()
    {
        if (playerTransform == null) return;

        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            ShootAtPlayer();
            fireTimer = fireRate;
            transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));

        }
    }

    void UpdateDestination()
    {
        if (playerTransform != null)
        {
            navMeshAgent.SetDestination(playerTransform.position);
        }
    }

    void ShootAtPlayer()
    {
        Vector3 direction = (playerTransform.position - firePoint.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(direction));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }
        Destroy(bullet, 5f);
        if (muzzleFlash != null)
            muzzleFlash.Play();
    }
}
