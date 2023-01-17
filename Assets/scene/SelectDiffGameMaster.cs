using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SelectDiffGameMaster : MonoBehaviour
{

    [SerializeField] Button easy_button;
    [SerializeField] Button normal_button;
    [SerializeField] Button hard_button;
    [SerializeField] GameObject difficultyTitle;
    [SerializeField] GameObject HowToPlayTitleText;
    [SerializeField] GameObject smartphone_hand;
    [SerializeField] GameObject how_to_play_description;
    
    GameObject easy_button_object;
    GameObject normal_button_object;
    GameObject hard_button_object;

    int retry_mode = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        easy_button_object = GameObject.Find("Canvas/EasyButton");
        normal_button_object = GameObject.Find("Canvas/NormalButton");
        hard_button_object = GameObject.Find("Canvas/HardButton");
        
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
        SceneManager.LoadScene("Chahan");
    }
}
