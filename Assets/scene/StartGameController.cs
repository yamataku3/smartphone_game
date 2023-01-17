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
    GameObject start_button_object;

    int retry_mode = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        start_button_object = GameObject.Find("Canvas/StartButton");
        start_button_object.SetActive(true);
        start_button.onClick.AddListener(toSelectDifficulty);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toSelectDifficulty(){
        SceneManager.LoadScene("SelectDiff");
    }

}
