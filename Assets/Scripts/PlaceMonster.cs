using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlaceMonster : MonoBehaviour
{/*
    public GameObject monsterPrefab;
    private GameObject monster;
    private GameManagerBehaviour gameManager;
    private GameObject _TowerChooser;


    private void OnMouseUp()
    {
            _TowerChooser = GameObject.FindGameObjectWithTag("TowerChooser");
            monster = (GameObject)
                Instantiate(monsterPrefab, transform.position, quaternion.identity);
            monster.transform.position = _TowerChooser.transform.position - new Vector3(0.27f, 0, 0);

            //AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            //audioSource.PlayOneShot(audioSource.clip);
            //gameManager.Gold -= monster.GetComponent<MonsterData>().CurrentLevel.cost;
            DestroyGameObject();
    }
    
    private void Start()
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehaviour>();

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

    }*/
}
