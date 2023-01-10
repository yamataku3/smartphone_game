using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartGameController : MonoBehaviour
{
    [SerializeField] GameObject flyingpan;
    [SerializeField] GameObject TitleText;
    [SerializeField] Button start_button;
    [SerializeField] Button easy_button;
    [SerializeField] Button normal_button;
    [SerializeField] Button hard_button;
    [SerializeField] GameObject difficultyTitle;
    [SerializeField] GameObject HowToPlayTitleText;
    [SerializeField] GameObject smartphone_hand;
    [SerializeField] GameObject how_to_play_description;
    
    GameObject start_button_object;
    GameObject easy_button_object;
    GameObject normal_button_object;
    GameObject hard_button_object;
    
    // Start is called before the first frame update
    void Start()
    {
        start_button_object = GameObject.Find("Canvas/StartButton");
        easy_button_object = GameObject.Find("Canvas/EasyButton");
        normal_button_object = GameObject.Find("Canvas/NormalButton");
        hard_button_object = GameObject.Find("Canvas/HardButton");
        
        start_button_object.SetActive(true);
        flyingpan.SetActive(true);
        TitleText.SetActive(true);

        difficultyTitle.SetActive(false);
        easy_button_object.SetActive(false);
        normal_button_object.SetActive(false);
        hard_button_object.SetActive(false);
        HowToPlayTitleText.SetActive(false);
        smartphone_hand.SetActive(false);
        how_to_play_description.SetActive(false);

        start_button.onClick.AddListener(toSelectDifficultyUI);
        easy_button.onClick.AddListener(() => toChahanButtonClick(1));
        normal_button.onClick.AddListener(() => toChahanButtonClick(2));
        hard_button.onClick.AddListener(() => toChahanButtonClick(3));
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toChahanButtonClick(int diff){
        PlayerPrefs.SetInt("difficulty", diff);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Chahan_test");
    }

    public void toSelectDifficultyUI(){
        
        start_button_object.SetActive(false);
        flyingpan.SetActive(false);
        TitleText.SetActive(false);

        difficultyTitle.SetActive(true);
        easy_button_object.SetActive(true);
        normal_button_object.SetActive(true);
        hard_button_object.SetActive(true);
        HowToPlayTitleText.SetActive(true);
        smartphone_hand.SetActive(true);
        how_to_play_description.SetActive(true);
    }
}
