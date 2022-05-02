using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInverntory : MonoBehaviour
{
public int OrbInventory;
    public void OrbCollection()
    {
        OrbInventory ++;
    }
    public void EnemyHit()
    {
        OrbInventory--;
    }
}
