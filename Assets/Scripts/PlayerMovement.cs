using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{


    public Transform targetObject;

    public float gravityForce;
    [Range(1f, 10f)]
    public float movementSpeed;

    private float verticalVelocity;
    public Vector2 movementInput;
    public Vector2 mousePosition;
    public Vector3 lookTarget;
    private CharacterController characterController;
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float health;
    public HealthBar healthbar;

    private bool isPoweredUp = false;
    public float powerUpDuration = 5f;
    public float poweredDamage = 100f;
    public float normalDamage = 25f;
    public ParticleSystem muzzleFlash;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    void Start()
    {
        if (healthbar != null)
        {
            healthbar.maxHealth = health;
            healthbar.currentHealth = health;
            healthbar.UpdateBar();
        }
    }

    void Update()
    {
        if (!characterController.isGrounded)
        {

            verticalVelocity = gravityForce * Time.deltaTime;
        }

        float movementX = (movementInput.x * movementSpeed * Time.deltaTime);
        float movementZ = (movementInput.y * movementSpeed * Time.deltaTime);

        Vector3 finalMovement = new Vector3(movementX, verticalVelocity, movementZ);

        characterController.Move(finalMovement);

        lookTarget.y = transform.position.y;
        transform.LookAt(lookTarget);

    }
    public void OnMove(InputAction.CallbackContext context)
    {

        movementInput = context.ReadValue<Vector2>();
        Debug.Log("Movement Input: " + movementInput);
    }

    public void OnMouseLook(InputAction.CallbackContext context)
    {

        mousePosition = context.ReadValue<Vector2>();
        Debug.Log("Mouse Postion: " + mousePosition);

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float enter))
        {
            lookTarget = ray.GetPoint(enter);

        }
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletScript = newBullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                if (isPoweredUp)
                    bulletScript.damage = poweredDamage;
                else
                    bulletScript.damage = normalDamage;
            }
            if (muzzleFlash != null)
                muzzleFlash.Play();
        }
    }

    
    public void TakeDamage(float damage)
    {
        health -= damage;

        if (healthbar != null)
        {
            healthbar.currentHealth = health;
            healthbar.UpdateBar();
        }


        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        
        Destroy(gameObject);
        SceneManager.LoadScene("Lose");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(25);
          
        }
        else if (other.CompareTag("Heart"))
        {
            Heal(30);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Potion"))
        {
            Destroy(other.gameObject);
            StartCoroutine(PowerUpRoutine());
        }
    }
    public void Heal(int amount)
    {
        health += amount;
        if (health > healthbar.maxHealth)
            health = healthbar.maxHealth;

        if (healthbar != null)
        {
            healthbar.currentHealth = health;
            healthbar.UpdateBar();
        }
    }
    IEnumerator PowerUpRoutine()
    {
        isPoweredUp = true;
        Debug.Log("activadp");
        yield return new WaitForSeconds(powerUpDuration);
        isPoweredUp = false;
        Debug.Log("terminado");
    }
}
