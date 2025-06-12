using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
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

    public int health;
    public HealthBar healthbar;

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
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
    public void TakeDamage(int damage)
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
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(25);
          
        }
    }
}