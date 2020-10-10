﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterLevel
{
    public int cost;
    public GameObject visualization;
    public GameObject bullet;
    public float fireRate;

}

public class MonsterData : MonoBehaviour
{
    public List<MonsterLevel> levels;
    private MonsterLevel currentLevel;
    private GameManagerBehaviour gameManager;
    private GameObject monster;

    public MonsterLevel CurrentLevel
    {
        get
        {
            return currentLevel;
        }
        set
        {
            currentLevel = value;
            int currentLevelIndex = levels.IndexOf(currentLevel);

            GameObject levelVisualization = levels[currentLevelIndex].visualization;
            for (int i=0; i < levels.Count; i++)
            {
                if(levelVisualization != null)
                {
                    if(i == currentLevelIndex)
                    {
                        levels[i].visualization.SetActive(true);
                    }
                    else
                    {
                        levels[i].visualization.SetActive(false);
                    }
                }
            }
        }
    }

    void OnEnable()
    {
        CurrentLevel = levels[0];
        monster = this.gameObject;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehaviour>();

    }

    public MonsterLevel GetNextLevel()
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        int maxLevelIndex = levels.Count - 1;
        if (currentLevelIndex < maxLevelIndex)
        {
            return levels[currentLevelIndex + 1];
        }
        else
        {
            return null;
        }


    }

    public void IncreaseLevel()
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        if (currentLevelIndex < levels.Count - 1)
        {
            CurrentLevel = levels[currentLevelIndex + 1];
        }
    }
    private void OnMouseUp()
    {
        if (CanUpgradeMonster())
        {
            monster.GetComponent<MonsterData>().IncreaseLevel();
            //AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            //audioSource.PlayOneShot(audioSource.clip);
            gameManager.Gold -= monster.GetComponent<MonsterData>().CurrentLevel.cost;
        }
    }

    private bool CanUpgradeMonster()
    {
        if (monster != null)
        {
            MonsterData monsterData = monster.GetComponent<MonsterData>();
            MonsterLevel nextLevel = monsterData.GetNextLevel();
            if (nextLevel != null)
            {
                return gameManager.Gold >= nextLevel.cost;
            }
        }
        return false;
    }

}
