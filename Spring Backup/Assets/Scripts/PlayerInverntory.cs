using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInverntory : MonoBehaviour
{
    public UnityEvent<PlayerInverntory> OnOrbCollection;
    public int OrbInventory { get; private set;}
    public void OrbCollection()
    {
        OrbInventory ++;
        OnOrbCollection.Invoke(this);
    }
    public void EnemyHit()
    {
        if (OrbInventory > 0) 
        {
            OrbInventory--;
            OnOrbCollection.Invoke(this);
        }
        else
        {
            OrbInventory = 0;
            OnOrbCollection.Invoke(this);
        }
    }
}
