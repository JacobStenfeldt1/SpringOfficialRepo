using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ScoreText;
    public int score;
    private void OnTriggerEnter(Collider other)
    {
        score += 1;
        ScoreText.GetComponent<Text>().text = "Score" + score;
        Destroy(gameObject);

    }
}
