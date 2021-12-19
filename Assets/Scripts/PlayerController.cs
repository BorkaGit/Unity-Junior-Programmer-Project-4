using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject powerupIndicator;
    private GameObject enemy; 
    private GameObject player; 
    private Rigidbody bulletRb;
    public float speed=5.0f;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public bool hasPowerup = false;
    public bool hasShotgun = false;
    private float powerupStrength = 15.0f;
    private Bullet bt;
    public GameObject bulletPrefab;
    private Bullet bulletVar;
    private float spawnRange = 1.0f;
    public bool CoolDownB = false;
    public double radius = 1.2;
    private SpawnManager enemyPrefab;
    public int enemyCount;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
        bulletRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");


    }
    
    void Update()
    {
        
        enemyCount = FindObjectsOfType<Enemy>().Length;
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward*speed*forwardInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0f, -0.5f, 0f);
        
         if ( hasShotgun && !CoolDownB)
        {
            //bulletPrefab.GetComponent<Bullet>().bulletCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
            Debug.Log("AAAAAAAAAAAAAAAAAAaa");
            //GameObject.FindGameObjectsWithTag("Enemy").Length
            //bulletVar.SpawnBullets(GameObject.FindGameObjectsWithTag("Enemy").Length);
            // for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
             //Debug.Log("Number = " +  GameObject.FindGameObjectsWithTag("Enemy").Length);
                //Instantiate(bulletPrefab, GenerateSpawnPosition(), bulletPrefab.transform.rotation);
                //StartCoroutine(BulletsSpawnCountdownRoutine());
                //here!!
                GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
                {
                    StartCoroutine(BulletsSpawnCountdownRoutine());
                }

                Vector3 lookDirection = (enemy.transform.position - player.transform.position).normalized;
            bulletRb.AddForce(lookDirection*speed,ForceMode.Impulse);
            
        }
    }

    IEnumerator BulletsSpawnCountdownRoutine()
    {
        CoolDownB = true;
        yield return new WaitForSeconds(1);
        Instantiate(bulletPrefab, GenerateSpawnPosition(), bulletPrefab.transform.rotation);
        CoolDownB = false;

    }
    
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, -0.5f, spawnPosZ);
        return randomPos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Power Up"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
        else if (other.CompareTag("ShotGun"))
        {
            hasShotgun = true;
            Destroy(other.gameObject);
            StartCoroutine(ShotgunCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }

    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }
    
    IEnumerator ShotgunCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasShotgun = false;
        powerupIndicator.gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            Debug.Log("Collided with " + collision.gameObject.name + "with powerup set up to " + hasPowerup);
            enemyRigidbody.AddForce(awayFromPlayer*powerupStrength,ForceMode.Impulse);
        }
        else if (collision.gameObject.CompareTag("Enemy") && hasShotgun)
        {
            bulletVar.SpawnBullets(GameObject.FindGameObjectsWithTag("Enemy").Length);
            Vector3 lookDirection = (enemy.transform.position - player.transform.position).normalized;
            bulletRb.AddForce(lookDirection*speed,ForceMode.Impulse);
            
        }
    }

   // public Vector3 bulletsCoord()
   //{
      
        //var deltaX = gos[0].transform.position.x - player.transform.position.x;
        //var deltaZ = enemy.transform.position.z - player.transform.position.z;
        //var K =(float) radius/(deltaX*deltaX+deltaZ*deltaZ);
        //var xCoord = player.transform.position.x + deltaX * K;
        //var zCoord = player.transform.position.z + deltaZ * K;
        
        //Vector3 bulletsCor = new Vector3 (xCoord, 0 , zCoord);
        //return bulletsCor;

   // }
    
   
}
