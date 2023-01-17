using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class EndGameController : MonoBehaviour
{
    int resultScore;
    [SerializeField] Text scoreText;
    [SerializeField] Button RetryButton;
    [SerializeField] Button TitleButton;
    // Start is called before the first frame update
    void Start()
    {
        resultScore = PlayerPrefs.GetInt("score");
        scoreText.text = resultScore.ToString();
        TitleButton.onClick.AddListener(toStartButtonClick);
        RetryButton.onClick.AddListener(BackToRetry);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void toStartButtonClick(){
        SceneManager.LoadScene("start");
    }
    public void BackToRetry(){
        SceneManager.LoadScene("SelectDiff");
    }
}
