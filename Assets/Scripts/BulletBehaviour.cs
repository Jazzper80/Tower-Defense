using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    public float speed = 10;
    public int damage;
    public GameObject target;
    public Vector3 startPosition;
    public Vector3 targetPosition;

    private float distance;
    private float startTime;

    private GameManagerBehaviour gameManager;

    private SpawnEnemy spawnEnemy;

    private void Start()
    {
        startTime = Time.time;
        distance = Vector2.Distance(startPosition, targetPosition);
        //GameObject gm = GameObject.Find("GameManager");
        //gameManager = gm.GetComponent<GameManagerBehaviour>();
        if(GameObject.Find("YellowRoad"))
        {
            spawnEnemy = GameObject.Find("YellowRoad").GetComponent<SpawnEnemy>();
        }
        else if (GameObject.Find("OrangeRoad"))
        {
            spawnEnemy = GameObject.Find("OrangeRoad").GetComponent<SpawnEnemy>();
        }

    }

    private void Update()
    {
        // 1 
        float timeInterval = Time.time - startTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);

        // 2 
        if (gameObject.transform.position.Equals(targetPosition))
        {
            if (target != null)
            {
                // 3
                Transform healthBarTransform = target.transform.Find("HealthBar");
                HealthBar healthBar =
                    healthBarTransform.gameObject.GetComponent<HealthBar>();
                healthBar.currentHealth -= Mathf.Max(damage, 0);
                // 4
                if (healthBar.currentHealth <= 0)
                {
                    Destroy(target);
                    spawnEnemy.enemiesStillAlive -= 1;
                    AudioSource audioSource = target.GetComponent<AudioSource>();
                    AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

                }
            }
            Destroy(gameObject);
        }

    }
}
