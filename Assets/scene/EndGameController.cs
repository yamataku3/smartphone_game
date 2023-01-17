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
    GameObject timeTextObject, chahanNameTextObject, strongTextObject, weakTextObject, RemainingIngredientTextObject, percentTextObject, scoreisObject, scoreTextObject, RetryButtonObject, TitleButtonObject;

    string fried_rice_Name;
    // Audio clip
    AudioSource audio_source_se;
    public AudioClip audio_clip_se;
    GameObject[] timeTextObjects, weakTextObjects, strongTextObjects, percentTextObjects, scoreTextObjects;
    // Start is called before the first frame update
    void Start()
    {
        SetFalseObject();

        audio_source_se = GetComponent<AudioSource>();
        resultScore = PlayerPrefs.GetInt("score");
        resultUpper = PlayerPrefs.GetInt("count_upper");
        resultLower = PlayerPrefs.GetInt("count_lower");
        resultMiddle = PlayerPrefs.GetInt("count_middle");
        ingredientCount = PlayerPrefs.GetInt("ingredient_count");
        detectionCount = PlayerPrefs.GetInt("detection_count");
        Minutes = PlayerPrefs.GetInt("Minutes");
        Seconds = PlayerPrefs.GetInt("Seconds");
        fried_rice_Name = PlayerPrefs.GetString("fried_rice_type");

        TitleButton.onClick.AddListener(toStartButtonClick);
        RetryButton.onClick.AddListener(BackToRetry);

        scoreText.text = resultScore.ToString();
        timeText.text = "TIME: " + Minutes.ToString() + " m " + Seconds.ToString() + "s";
        weakText.text += resultLower.ToString();
        strongText.text += resultUpper.ToString();
        chahanNameText.text = fried_rice_Name;
        percentText.text = (100.0 - (double)detectionCount * 100 / (double)ingredientCount).ToString() + "%";
        

        Debug.Log("Upper:" + resultUpper);
        Debug.Log("Lower:" + resultLower);
        Debug.Log("Remaining Ingredient:" + ingredientCount);
        Debug.Log("DetectionCount:" + detectionCount);
        Debug.Log("Minutes:" + Minutes);
        Debug.Log("Seconds:" + Seconds);
        
        
        StartCoroutine(display_fried_rice_name(1.0f, timeTextObjects, false));
        StartCoroutine(display_fried_rice_name(2.0f, weakTextObjects, false));
        StartCoroutine(display_fried_rice_name(3.0f, strongTextObjects, false));
        StartCoroutine(display_fried_rice_name(4.0f, percentTextObjects, false));
        StartCoroutine(display_fried_rice_name(5.0f, scoreTextObjects, true));
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
    public IEnumerator display_fried_rice_name(float delay, GameObject[] gm_list, bool is_score)
    {
        //delay秒待つ
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < gm_list.Length; i++){
            Debug.Log(gm_list[i]);
            gm_list[i].SetActive(true);
        }

        if (is_score){
            audio_source_se.PlayOneShot(audio_clip_se);
        }else{
            audio_source_se.PlayOneShot(audio_clip_se);
        }
        

    }
    public void SetFalseObject(){
        timeTextObject = GameObject.Find("Canvas/TimeText");
        timeTextObject.SetActive(false);
        weakTextObject = GameObject.Find("Canvas/WeakText");
        weakTextObject.SetActive(false);
        strongTextObject = GameObject.Find("Canvas/StrongText");
        strongTextObject.SetActive(false);
        RemainingIngredientTextObject = GameObject.Find("Canvas/RemainingIngredient");
        RemainingIngredientTextObject.SetActive(false);
        percentTextObject = GameObject.Find("Canvas/PrecentageText");
        percentTextObject.SetActive(false);
        scoreisObject = GameObject.Find("Canvas/Scoreis");
        scoreisObject.SetActive(false);
        scoreTextObject = GameObject.Find("Canvas/ScoreText");
        scoreTextObject.SetActive(false);
        RetryButtonObject = GameObject.Find("Canvas/RetryButton");
        RetryButtonObject.SetActive(false);
        TitleButtonObject = GameObject.Find("Canvas/TitleButton");
        TitleButtonObject.SetActive(false);

        timeTextObjects = new GameObject[1]{timeTextObject};
        weakTextObjects = new GameObject[1]{weakTextObject};
        strongTextObjects = new GameObject[1]{strongTextObject};
        percentTextObjects = new GameObject[2]{RemainingIngredientTextObject, percentTextObject};
        scoreTextObjects = new GameObject[4]{scoreisObject, scoreTextObject, RetryButtonObject, TitleButtonObject};
    
        
    }



}
