using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectButterfly : MonoBehaviour
{
     void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
