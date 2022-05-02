using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectOrb : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        PlayerInverntory playerinventory = other.GetComponent<PlayerInverntory>();
        if (playerinventory != null)
        {
            playerinventory.OrbCollection();
            gameObject.SetActive(false);
        }

    }
}
