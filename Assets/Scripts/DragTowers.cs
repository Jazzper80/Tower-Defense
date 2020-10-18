using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class DragTowers : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    public GameObject blueTower, yellowTower, redTower, orangeTower, greenTower, purpleTower;
    private GameObject thisTower;

    private bool hoveringOnOpenSpot;
    private string colourOfCollision;
    private Vector3 openSpotBeingHovered;

    private ShootEnemies shootEnemies;
    private GameObject openSpot;
    private GameObject towerSetOnOpenspot;

    private bool towerHasBeenSet;
    private bool hasOtherTowerBeenSet;

    public GameObject overlaySprite;

    private bool boxColliderTrigger;

    private void Start()
    {
        shootEnemies = gameObject.GetComponent<ShootEnemies>();
        towerHasBeenSet = false;
        thisTower = gameObject;
        hasOtherTowerBeenSet = false;
    }
    void OnMouseDown()
    {
        if(openSpot != null)
        {
            if (openSpot.CompareTag("Openspot"))
            {
                openSpot.GetComponent<BoxCollider2D>().isTrigger = true;
                towerHasBeenSet = false;
            }
        }
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        overlaySprite.SetActive(true);
        shootEnemies.canShoot = false;
    }

    void OnMouseDrag()
    {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        openSpot = collision.gameObject;

        if(collision.CompareTag("Openspot"))
        {
            hoveringOnOpenSpot = true;
            openSpotBeingHovered = collision.gameObject.transform.position;
        }
        else if(collision.CompareTag("BlueTower") && collision.GetComponent<DragTowers>().towerHasBeenSet == true)
        {
            towerSetOnOpenspot = collision.gameObject;
            hasOtherTowerBeenSet = openSpot.GetComponent<DragTowers>().towerHasBeenSet;
            colourOfCollision = "Blue";
            Debug.Log("The colourOfCollision = " + colourOfCollision);
        }
        else if(collision.CompareTag("YellowTower") && collision.GetComponent<DragTowers>().towerHasBeenSet == true)
        {
            towerSetOnOpenspot = collision.gameObject;
            hasOtherTowerBeenSet = openSpot.GetComponent<DragTowers>().towerHasBeenSet;
            colourOfCollision = "Yellow";
            Debug.Log("The colourOfCollision = " + colourOfCollision);

        }
        else if(collision.CompareTag("RedTower") && collision.GetComponent<DragTowers>().towerHasBeenSet == true)
        {
            towerSetOnOpenspot = collision.gameObject;
            hasOtherTowerBeenSet = openSpot.GetComponent<DragTowers>().towerHasBeenSet;
            colourOfCollision = "Red";
            Debug.Log("The colourOfCollision = " + colourOfCollision);

        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        hasOtherTowerBeenSet = false;
        if (collision.CompareTag("Openspot"))
        {
            hoveringOnOpenSpot = false;
            openSpotBeingHovered = transform.position;
        }
    }

    private void OnMouseUp()
    {
        overlaySprite.SetActive(false);
        if (hoveringOnOpenSpot == true)
        {
            this.transform.position = openSpotBeingHovered;
            shootEnemies.canShoot = true;
            towerHasBeenSet = true;
            if(openSpot.CompareTag("Openspot"))
            {
                openSpot.GetComponent<BoxCollider2D>().isTrigger = false;
            }
            CombineTower();
        }
        else
        {
            Debug.Log("Not hovering a tower");
        }
    }
    private void CombineTower()
    {
        if(hoveringOnOpenSpot == true && hasOtherTowerBeenSet == true)
        {
            if (thisTower.tag == "YellowTower" && colourOfCollision == "Blue")
            {
                Instantiate(greenTower, transform.position, Quaternion.identity);
                greenTower.GetComponent<ShootEnemies>().canShoot = true;
                Destroy(towerSetOnOpenspot);
                Destroy(thisTower);
            }
            else if (thisTower.tag == "BlueTower" && colourOfCollision == "Yellow")
            {
                Instantiate(greenTower, transform.position, Quaternion.identity);
                greenTower.GetComponent<ShootEnemies>().canShoot = true;
                Destroy(towerSetOnOpenspot);
                Destroy(thisTower);
            }
            else if (thisTower.tag == "BlueTower" && colourOfCollision == "Red")
            {
                Instantiate(purpleTower, transform.position, Quaternion.identity);
                purpleTower.GetComponent<ShootEnemies>().canShoot = true;
                Destroy(towerSetOnOpenspot);
                Destroy(thisTower);
            }
            else if (thisTower.tag == "RedTower" && colourOfCollision == "Blue")
            {
                Instantiate(purpleTower, transform.position, Quaternion.identity);
                purpleTower.GetComponent<ShootEnemies>().canShoot = true;
                Destroy(towerSetOnOpenspot);
                Destroy(thisTower);
            }
            else if (thisTower.tag == "YellowTower" && colourOfCollision == "Red")
            {
                Instantiate(orangeTower, transform.position, Quaternion.identity);
                orangeTower.GetComponent<ShootEnemies>().canShoot = true;
                Destroy(towerSetOnOpenspot);
                Destroy(thisTower);
            }
            else if (thisTower.tag == "RedTower" && colourOfCollision == "Yellow")
            {
                Instantiate(orangeTower, transform.position, Quaternion.identity);
                orangeTower.GetComponent<ShootEnemies>().canShoot = true;
                Destroy(towerSetOnOpenspot);
                Destroy(thisTower);
            }
        }
    }
}
