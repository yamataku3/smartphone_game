using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Threading;
public class EndGameController : MonoBehaviour
{
    int resultScore, resultLower, resultUpper, resultMiddle, ingredientCount, detectionCount, Minutes, Seconds;
    [SerializeField] Text timeText, chahanNameText, strongText, weakText, percentText, scoreText;
    [SerializeField] Button RetryButton, TitleButton;
  
    // Audio clip
    AudioSource audio_source_se;
    public AudioClip audio_clip_se;
    // Start is called before the first frame update
    void Start()
    {
        audio_source_se = GetComponent<AudioSource>();
        resultScore = PlayerPrefs.GetInt("score");
        resultUpper = PlayerPrefs.GetInt("count_upper");
        resultLower = PlayerPrefs.GetInt("count_lower");
        resultMiddle = PlayerPrefs.GetInt("count_middle");
        ingredientCount = PlayerPrefs.GetInt("ingredient_count");
        detectionCount = PlayerPrefs.GetInt("detection_count");
        Minutes = PlayerPrefs.GetInt("Minutes");
        Seconds = PlayerPrefs.GetInt("Seconds");
        //fryed_rice_type = PlayerPrefs.GetString("fryed_rice_type");

        TitleButton.onClick.AddListener(toStartButtonClick);
        RetryButton.onClick.AddListener(BackToRetry);

        scoreText.text = resultScore.ToString();
        timeText.text = "TIME: " + Minutes.ToString() + " m " + Seconds.ToString() + "s";
        weakText.text += resultLower.ToString();
        strongText.text += resultUpper.ToString();
        percentText.text = (100.0 - (double)detectionCount * 100 / (double)ingredientCount).ToString() + "%";
        

        Debug.Log("Upper:" + resultUpper);
        Debug.Log("Lower:" + resultLower);
        Debug.Log("Remaining Ingredient:" + ingredientCount);
        Debug.Log("DetectionCount:" + detectionCount);
        Debug.Log("Minutes:" + Minutes);
        Debug.Log("Seconds:" + Seconds);
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
    public IEnumerator display_fried_rice_name(float delay, string name, bool is_score)
    {
        //delay秒待つ
        yield return new WaitForSeconds(delay);
        GameObject.Find(name).SetActive(true);
        if (is_score){
            audio_source_se.PlayOneShot(audio_clip_se);
        }else{
            audio_source_se.PlayOneShot(audio_clip_se);
        }
        

    }


}
