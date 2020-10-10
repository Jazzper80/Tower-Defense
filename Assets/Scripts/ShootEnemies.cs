using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemies : MonoBehaviour
{

    public List<GameObject> enemiesInRange;
    private float lastShotTime;
    private MonsterData monsterData;

    private void Start()
    {
        enemiesInRange = new List<GameObject>();
        lastShotTime = Time.time;
        monsterData = gameObject.GetComponentInChildren<MonsterData>();

    }


    private void Update()
    {
        GameObject target = null;
        // 1
        float minimalEnemyDistance = float.MaxValue;
        foreach (GameObject enemy in enemiesInRange)
        {
            float distanceToGoal = enemy.GetComponent<MoveEnemy>().DistanceToGoal();
            if (distanceToGoal < minimalEnemyDistance)
            {
                target = enemy;
                minimalEnemyDistance = distanceToGoal;
            }
        }
        // 2
        if (target != null)
        {
            if (Time.time - lastShotTime > monsterData.CurrentLevel.fireRate)
            {
                Shoot(target.GetComponent<Collider2D>());
                lastShotTime = Time.time;
            }
            // 3
            Vector3 direction = gameObject.transform.position - target.transform.position;
            gameObject.transform.rotation = Quaternion.AngleAxis(
                Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI,
                new Vector3(0, 0, 1));
        }

    }


    // 1
    void OnEnemyDestroy(GameObject enemy)
    {
        enemiesInRange.Remove(enemy);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 2
        if ((this.tag == "RedTower" && other.gameObject.tag.Equals("RedEnemy")) || 
            (this.tag == "BlueTower" && other.gameObject.tag.Equals("BlueEnemy")) || 
            (this.tag == "YellowTower" && other.gameObject.tag.Equals("YellowEnemy")) ||
            (this.tag == "PurpleTower" && (other.gameObject.tag.Equals("RedEnemy") || other.gameObject.tag.Equals("BlueEnemy"))) ||
            (this.tag == "GreenTower" && (other.gameObject.tag.Equals("YellowEnemy") || other.gameObject.tag.Equals("BlueEnemy"))) ||
            (this.tag == ("OrangeTower") && (other.gameObject.tag.Equals("YellowEnemy") || other.gameObject.tag.Equals("RedEnemy"))) ||
            (this.tag == "BrownTower" && (other.gameObject.tag.Equals("YellowEnemy") || other.gameObject.tag.Equals("BlueEnemy") || other.gameObject.tag.Equals("RedEnemy")))
            
            )
        {
            enemiesInRange.Add(other.gameObject);
            EnemyDestructionDelegate del =
                other.gameObject.GetComponent<EnemyDestructionDelegate>();
            del.enemyDelegate += OnEnemyDestroy;
        }
    }
    // 3
    void OnTriggerExit2D(Collider2D other)
    {
        if ((this.tag == "RedTower" && other.gameObject.tag.Equals("RedEnemy")) ||
            (this.tag == "BlueTower" && other.gameObject.tag.Equals("BlueEnemy")) ||
            (this.tag == "YellowTower" && other.gameObject.tag.Equals("YellowEnemy")) ||
            (this.tag == "PurpleTower" && (other.gameObject.tag.Equals("RedEnemy") || other.gameObject.tag.Equals("BlueEnemy"))) ||
            (this.tag == "GreenTower" && (other.gameObject.tag.Equals("YellowEnemy") || other.gameObject.tag.Equals("BlueEnemy"))) ||
            (this.tag == ("OrangeTower") && (other.gameObject.tag.Equals("YellowEnemy") || other.gameObject.tag.Equals("RedEnemy"))) ||
            (this.tag == "BrownTower" && (other.gameObject.tag.Equals("YellowEnemy") || other.gameObject.tag.Equals("BlueEnemy") || other.gameObject.tag.Equals("RedEnemy")))

            )
        {
            enemiesInRange.Remove(other.gameObject);
            EnemyDestructionDelegate del =
                other.gameObject.GetComponent<EnemyDestructionDelegate>();
            del.enemyDelegate -= OnEnemyDestroy;
        }
    }
    void Shoot(Collider2D target)
    {
        GameObject bulletPrefab = monsterData.CurrentLevel.bullet;
        // 1 
        Vector3 startPosition = gameObject.transform.position;
        Vector3 targetPosition = target.transform.position;
        startPosition.z = bulletPrefab.transform.position.z;
        targetPosition.z = bulletPrefab.transform.position.z;

        // 2 
        GameObject newBullet = (GameObject)Instantiate(bulletPrefab);
        newBullet.transform.position = startPosition;
        BulletBehaviour bulletComp = newBullet.GetComponent<BulletBehaviour>();
        bulletComp.target = target.gameObject;
        bulletComp.startPosition = startPosition;
        bulletComp.targetPosition = targetPosition;

        // 3 
        //Animator animator =
       //     monsterData.CurrentLevel.visualization.GetComponent<Animator>();
        //animator.SetTrigger("fireShot");
        //AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        //audioSource.PlayOneShot(audioSource.clip);
    }

}
