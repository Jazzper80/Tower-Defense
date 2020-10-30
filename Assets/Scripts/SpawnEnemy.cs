using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Wave
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2;
    public int maxEnemies = 20;
}

public class SpawnEnemy : MonoBehaviour
{
    public float timeBetweenEnemies = 2;
    public bool startSpawingEnemies;
    private GameManagerBehaviour gameManager;

    private float lastSpawnTime;
    private int enemiesSpawned = 0;

    public GameObject[] enemiesInThisLevel;

    public GameObject[] waypoints;
    public GameObject testEnemyPrefab;

    private int enemiesCounter;
    private SceneLoader sceneLoader;

    public int enemiesStillAlive;
    private bool endOfLevelReached;

    private int currentSceneIndex;

    private void Start()
    {
        startSpawingEnemies = true;
        enemiesCounter = enemiesInThisLevel.Length;
        sceneLoader = GameObject.Find("Main Camera").GetComponent<SceneLoader>();
        timeBetweenEnemies = 2;
        endOfLevelReached = false;
        currentSceneIndex = SceneManager.sceneCountInBuildSettings;
    }

    private void Update()
    {
        if(timeBetweenEnemies <0 && enemiesCounter >= 0 && endOfLevelReached == false)
        {
            timeBetweenEnemies = 2;
            SpawnEnemiesThisLevel();
        }

        timeBetweenEnemies -= Time.deltaTime;
    }

    private void SpawnEnemiesThisLevel()
        {
        if (enemiesCounter > 0)
        {
            GameObject newEnemy = Instantiate(enemiesInThisLevel[enemiesCounter - 1], new Vector3 (-20f, 0f,0f), Quaternion.identity);
            enemiesStillAlive += 1;
            newEnemy.GetComponent<MoveEnemy>().blueWaypoints = waypoints;
            enemiesCounter -= 1;
        }
        else if(enemiesStillAlive == 0)
        {
            endOfLevelReached = true;
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            sceneLoader.LoadMyScene(currentSceneIndex + 1);
        }
        else
        {
            return;
        }
    }


}
