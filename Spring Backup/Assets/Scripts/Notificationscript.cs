using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notificationscript : MonoBehaviour
{
    [Header("Inehåll")]
    [SerializeField] private Text NotifikationText;

    [Header("Meddelande")]
    [SerializeField] [TextArea] private string TextMessage;

    [Header("Avsluta")]
    [SerializeField] private bool Removeafterextit = false;
    [SerializeField] private bool Removeaftertimer = false;
    [SerializeField] private float TimerTime = 1.0f;

    [Header("Animation")]
    [SerializeField] private Animator MessageAnimation;
    private BoxCollider TriggerCollider;

    private void Awake()
    {
        TriggerCollider = gameObject.GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(EnableNotification());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player") && Removeafterextit)
        {
            RemoveNotification();
        }
    }
    IEnumerator EnableNotification()
    {
        TriggerCollider.enabled = false;
        MessageAnimation.Play("NotificationAnimationIn");
        NotifikationText.text = TextMessage;
        if (Removeaftertimer)
        {
            yield return new WaitForSeconds(TimerTime);
            RemoveNotification();
        }

    }


    private void RemoveNotification()
    {
        MessageAnimation.Play("NotificationAnimationOut");
        gameObject.SetActive(false);
    }
}
