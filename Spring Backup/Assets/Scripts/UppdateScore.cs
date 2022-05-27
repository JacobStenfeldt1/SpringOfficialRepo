using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UppdateScore : MonoBehaviour
{
    private TextMeshProUGUI scoretext;
    void Start()
    {
        scoretext = GetComponent < TextMeshProUGUI>();

    }
    public void UpdateScoreText (PlayerInverntory playerInverntory)
    {
        scoretext.text = "Score:" + playerInverntory.OrbInventory.ToString();
    }
}
