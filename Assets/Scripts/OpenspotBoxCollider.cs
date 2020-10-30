using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenspotBoxCollider : MonoBehaviour
{

    private BoxCollider2D boxCollider2D;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void TurnBoxColliderOnOrOff()
    {
        if (boxCollider2D.enabled == true)
        {
            boxCollider2D.enabled = false;

        }
        else if(boxCollider2D.enabled == false)
        {
            boxCollider2D.enabled = true;
        }
    }


}
