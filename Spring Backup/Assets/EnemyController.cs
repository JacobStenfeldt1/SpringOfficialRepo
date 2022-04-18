using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float playerdistance;
    public float awareAI = 10f; // distansen som vi kan kolla efetr en spelare
    public float damping = 6.0f;
    public Transform[] navPoint;
    public float AIMoveSpeed;
    public UnityEngine.AI.NavMeshAgent birdG;
    public int destinationpoint = 0;
    public Transform goal;

    public void Start()
    {
        UnityEngine.AI.NavMeshAgent birdG = GetComponent<UnityEngine.AI.NavMeshAgent>();
        birdG.destination = goal.position;

        birdG.autoBraking = false; 
    }
    public void Update()
    {
        // kolla hur lång ifrån spelaren är på banan 
        playerdistance = Vector3.Distance(player.position, transform.position);

        if(playerdistance < awareAI)
        {
            LookAtPlayer();
            Debug.Log("seen");
        }
        if(playerdistance < awareAI)
        {
            if (playerdistance > 0.2f)
            {
                Chase();
                Debug.Log("Chased");
            }

            else
            {
                NextPoint();
                Debug.Log("Nesxpoint");
            }
        }
    }

    private void NextPoint()
    {
        if(navPoint.Length == 0)
            return;
            birdG.destination = navPoint[destinationpoint].position;
            destinationpoint = (destinationpoint + 1) % navPoint.Length;
    }

    private void Chase()
    {
        transform.Translate(Vector3.forward * AIMoveSpeed * Time.deltaTime);
    }

    private void LookAtPlayer()
    {
        transform.LookAt(player);
    }
}
