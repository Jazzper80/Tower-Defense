using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class TowerChooser : MonoBehaviour
{
    public GameObject towerChooser;
    private GameObject openSpot;
    public GameObject monsterPrefab;
    private GameObject monster;
    private GameManagerBehaviour gameManager;
    private GameObject _TowerChooser;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehaviour>();

    }


    private void OnMouseUp()
    {
        _TowerChooser = GameObject.FindGameObjectWithTag("TowerChooser");
        if (_TowerChooser == null)
        {
            if (CanPlaceMonster())
            {
                Instantiate(towerChooser, transform.position, quaternion.identity);
            }
            else
            {
                //make clear to player that he has not have sufficient gold
            }
        }
        else
        {
            Debug.Log("There is already a tower on screen");
            return;

        }
    }

    public void Destroy()
    {
        Destroy(towerChooser);
    }
    private bool CanPlaceMonster()
    {
        //hoe duur een monster is
        int cost = monsterPrefab.GetComponent<MonsterData>().levels[0].cost;
        //als het monster niet null is en niet meer kost dan hoeveel je hebt
        return gameManager.Gold >= cost;

    }

}

