using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameController : MonoBehaviour
{
    int resultScore;
    [SerializeField] Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        resultScore = PlayerPrefs.GetInt("score");
        scoreText.text = resultScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
