using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody bulletRb;
    private GameObject player; 
    private GameObject enemy;
    private GameObject spawnManager;
    public float speed;
    private float spawnRange = 1.0f;
    public GameObject bulletPrefab;
    public int bulletCount;
    void Start()
    {
        bulletRb = GetComponent<Rigidbody>(); 
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");
        
    }

    
    void Update()
    {

       // int b =GameObject.FindGameObjectsWithTag("Enemy").Length;
       // Debug.Log("Number = " +  bulletCount);
        //int b = spawnManager.GetComponent<SpawnManager>().waveNumberS; Можно проверить с помощью GameObject.FindGameObjectsWithTag сколько врагов сейчас на карте и столько спавнить буллетов
        Vector3 lookDirection = (enemy.transform.position - player.transform.position).normalized;
        bulletRb.AddForce(lookDirection*speed,ForceMode.Impulse);
        
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, -0.5f, spawnPosZ);
        return randomPos;
    }
    public void SpawnBullets(int bulletsToSpawn)
    {
        Debug.Log("BBBBBBBBBBBBBb"); 
        for (int i = 0; i < bulletsToSpawn; i++)
        {
            Debug.Log("Number = " +  bulletsToSpawn);
            Instantiate(bulletPrefab, GenerateSpawnPosition(), bulletPrefab.transform.rotation);
        }
    }
}
