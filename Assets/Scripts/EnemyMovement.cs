using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour
{
   

    public Transform playerTransform; 
    private NavMeshAgent navMeshAgent; 
    [SerializeField] private float refreshRate = 0.5f; 
    private void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player")?.transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        InvokeRepeating("FindPlayer", 0f, refreshRate); 
    }

    
    void Update()
    {

    }

    public void FindPlayer()
    {

        navMeshAgent.SetDestination(playerTransform.position);
        transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));

    }



}
