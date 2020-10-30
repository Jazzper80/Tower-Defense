using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemies : MonoBehaviour
{

    public List<GameObject> enemiesInRange;
    private float lastShotTime;
    private MonsterData monsterData;
    public bool canShoot = false;
    private void Start()
    {
        enemiesInRange = new List<GameObject>();
        lastShotTime = Time.time;
        monsterData = gameObject.GetComponentInChildren<MonsterData>();
    }

    private void Update()
    {
        GameObject target = null;
        float minimalEnemyDistance = float.MaxValue;
        foreach (GameObject enemy in enemiesInRange)
        {
            float distanceToGoal = enemy.GetComponent<MoveEnemy>().DistanceToGoal();
            if (distanceToGoal < minimalEnemyDistance)
            {
                target = enemy;
                minimalEnemyDistance = distanceToGoal;
            }

            if (target != null)
            {
                if (Time.time - lastShotTime > monsterData.CurrentLevel.fireRate)
                {
                    Shoot(target.GetComponent<Collider2D>());
                    lastShotTime = Time.time;
                }
                Vector3 direction = gameObject.transform.position - target.transform.position;
                gameObject.transform.rotation = Quaternion.AngleAxis(
                    Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI,
                    new Vector3(0, 0, 1));
            }
        }
    }

    void OnEnemyDestroy(GameObject enemy)
    {
        enemiesInRange.Remove(enemy);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (CompareTag("RedTower") && other.gameObject.CompareTag("RedEnemy") ||
            CompareTag("BlueTower") && other.gameObject.CompareTag("BlueEnemy") ||
            CompareTag("YellowTower") && other.gameObject.CompareTag("YellowEnemy") ||
            CompareTag("PurpleTower") && other.gameObject.CompareTag("RedEnemy") || CompareTag("PurpleTower") && other.gameObject.CompareTag("BlueEnemy") || CompareTag("PurpleTower") && other.gameObject.CompareTag("PurpleEnemy") ||
            CompareTag("GreenTower") && other.gameObject.CompareTag("YellowEnemy") || CompareTag("GreenTower") && other.gameObject.CompareTag("BlueEnemy") || CompareTag("GreenTower") && other.gameObject.CompareTag("GreenEnemy") ||
            CompareTag("OrangeTower") && other.gameObject.CompareTag("YellowEnemy") || CompareTag("OrangeTower") && other.gameObject.CompareTag("RedEnemy") || CompareTag("OrangeTower") && other.gameObject.CompareTag("OrangeEnemy")
            )
          {
            enemiesInRange.Add(other.gameObject);
            Debug.Log("enemy removed " + other);
            EnemyDestructionDelegate del =
                other.gameObject.GetComponent<EnemyDestructionDelegate>();
            del.enemyDelegate -= OnEnemyDestroy;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (
            CompareTag("RedTower") && other.gameObject.CompareTag("RedEnemy") ||
            CompareTag("BlueTower") && other.gameObject.CompareTag("BlueEnemy") ||
            CompareTag("YellowTower") && other.gameObject.CompareTag("YellowEnemy") ||
            CompareTag("PurpleTower") && other.gameObject.CompareTag("RedEnemy") || CompareTag("PurpleTower") && other.gameObject.CompareTag("BlueEnemy") || CompareTag("PurpleTower") && other.gameObject.CompareTag("PurpleEnemy") ||
            CompareTag("GreenTower") && other.gameObject.CompareTag("YellowEnemy") || CompareTag("GreenTower") &&  other.gameObject.CompareTag("BlueEnemy") || CompareTag("GreenTower") &&  other.gameObject.CompareTag("GreenEnemy") ||
            CompareTag("OrangeTower") && other.gameObject.CompareTag("YellowEnemy") || CompareTag("OrangeTower") &&  other.gameObject.CompareTag("RedEnemy") || CompareTag("OrangeTower") &&  other.gameObject.CompareTag("OrangeEnemy")
            )
        {
            enemiesInRange.Remove(other.gameObject);
            Debug.Log("enemy removed " + other);
            EnemyDestructionDelegate del =
                other.gameObject.GetComponent<EnemyDestructionDelegate>();
                del.enemyDelegate -= OnEnemyDestroy;
            }
    }
    void Shoot(Collider2D target)
    {
        if (canShoot)
        {
            GameObject bulletPrefab = monsterData.CurrentLevel.bullet;
            Vector3 startPosition = gameObject.transform.position;
            Vector3 targetPosition = target.transform.position;
            startPosition.z = bulletPrefab.transform.position.z;
            targetPosition.z = bulletPrefab.transform.position.z;

            GameObject newBullet = (GameObject)Instantiate(bulletPrefab);
            newBullet.transform.position = startPosition;
            BulletBehaviour bulletComp = newBullet.GetComponent<BulletBehaviour>();
            bulletComp.target = target.gameObject;
            bulletComp.startPosition = startPosition;
            bulletComp.targetPosition = targetPosition;
        }
    }
}
