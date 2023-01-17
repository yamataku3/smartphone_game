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
    int resultScore;
    [SerializeField] Text scoreText;
    [SerializeField] Button RetryButton;
    [SerializeField] Button TitleButton;

    // Audio clip
    AudioSource audio_source_se;
    public AudioClip audio_clip_se;
    // Start is called before the first frame update
    void Start()
    {
        audio_source_se = GetComponent<AudioSource>();
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
