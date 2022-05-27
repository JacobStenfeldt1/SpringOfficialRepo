using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    public GameObject wall;
    public void OnTriggerEnter(Collider other)
    {
        PlayerInverntory playerinventory = other.GetComponent<PlayerInverntory>();
        if (playerinventory.OrbInventory >= 3)
        {
            wall.SetActive(false);
        }

    }
}
