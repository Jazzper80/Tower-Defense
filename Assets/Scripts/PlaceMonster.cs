using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlaceMonster : MonoBehaviour
{
    public GameObject monsterPrefab;
    private GameObject monster;
    private GameManagerBehaviour gameManager;
    private GameObject _TowerChooser;

    private bool CanPlaceMonster()
    {
        //hoe duur een monster is
        int cost = monsterPrefab.GetComponent<MonsterData>().levels[0].cost;
        //als het monster niet null is en niet meer kost dan hoeveel je hebt
        return monster == null && gameManager.Gold >= cost;

    }

    private void OnMouseUp()
    {
        if (CanPlaceMonster())
        {
            _TowerChooser = GameObject.FindGameObjectWithTag("TowerChooser");
            monster = (GameObject) 
                Instantiate(monsterPrefab, transform.position, quaternion.identity);
            monster.transform.position = _TowerChooser.transform.position - new Vector3(0.27f, 0, 0);
            
            //AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            //audioSource.PlayOneShot(audioSource.clip);
            gameManager.Gold -= monster.GetComponent<MonsterData>().CurrentLevel.cost;
            DestroyGameObject();
        }
        /*else if(CanUpgradeMonster())
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
    */}
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehaviour>();

    }

    private void DestroyGameObject()
    {
        Destroy(_TowerChooser);
        FindClosestOpenspot();
        
    }
    public GameObject FindClosestOpenspot()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Openspot");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        Destroy(closest);
        return closest;

    }
}
