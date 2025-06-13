using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public List<GameObject> enemyPrefabs;     
    public Transform[] spawnPoints;            
    public float spawnInterval = 3f;           

    private float timer;
    private bool canSpawn = true;

    void Update()
    {
        
        if (!canSpawn || !GameManager.Instance.gameStarted) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemies();
            timer = 0f;
        }
    }

    void SpawnEnemies()
    {
       
        if (enemyPrefabs.Count == 0 || spawnPoints.Length < 2) return;

       
        int index1 = Random.Range(0, spawnPoints.Length);
        List<Transform> availablePoints = new List<Transform>(spawnPoints);
        Transform spawnPoint1 = availablePoints[index1];

       
        availablePoints.RemoveAt(index1);

        
        int index2 = Random.Range(0, availablePoints.Count);
        Transform spawnPoint2 = availablePoints[index2];

        
        int enemy1 = Random.Range(0, enemyPrefabs.Count);
        int enemy2 = Random.Range(0, enemyPrefabs.Count);

     
        Instantiate(enemyPrefabs[enemy1], spawnPoint1.position, Quaternion.identity);
        Instantiate(enemyPrefabs[enemy2], spawnPoint2.position, Quaternion.identity);
    }

    
    public void StopSpawner()
    {
        canSpawn = false;
    }
}