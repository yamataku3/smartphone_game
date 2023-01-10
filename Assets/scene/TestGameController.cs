using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

// get sensor data
public class TestGameController: MonoBehaviour
{
    // the variance of getting sensor data.
    public Vector3 acc;
    public Gyroscope gyro;
    public Vector3 pos;
    //threshold for deciding the flyingpan animation.
    static double TH_large = 3;
    static double TH_middle = 1.8;
    static double TH_small = 1.2;
    
    int count = 0;//
    // management variances
    bool gesture_judge_flag = true; // ジェスチャを判定する状態か否か
    bool move_flyingpan_flag = false; // flying_panを動かすか否か
    int move_flyingpan_mode = 0;// flying_panを動かす際の状態遷移

    public int move_option = 0; // 動かし方を管理する変数
    public int game_phase_management = 0; //ゲームのフェーズ管理
    
    [SerializeField] Text chahan_text;// text
    //[SerializeField] Text Animated_Text;//
    [SerializeField] Button put_onion_button;
    [SerializeField] Button put_meat_button;
    public float force_ratio;
    float delay_time = 0.0f;
    // Animator
    //private Animator text_anim;
    // FoodMaster
    private FoodMaster foodMasterScript;
    public int count_lower = 0;//焦げた回数をカウント

    public double score;

    void Start()
    {
        // 入力にジャイロをONにする
        Input.gyro.enabled = true;
        Input.compass.enabled = true;
        // get the FoodMaster
        foodMasterScript= GameObject.Find("GameObject").GetComponent<FoodMaster>();
        //食材を追加するためのボタン
        put_onion_button.onClick.AddListener(GreenOnionButtonOnClick);
        put_meat_button.onClick.AddListener(MeatButtonOnClick);

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        acc = Input.acceleration;
        gyro = Input.gyro;
        pos = this.gameObject.transform.position;
        //ジェスチャ判定する時
        if(gesture_judge_flag){
            if (acc.z > TH_large - 1){
                gesture_judge_flag = false;
                move_flyingpan_flag = true;
                move_option = 3;
                
                force_ratio = 2.0f;
                Debug.Log("Large");
                // 具材の物理演算
                StartCoroutine(foodMasterScript.forceToRice(0.0f, false, force_ratio, -3.5f, 0, 0));
                StartCoroutine(foodMasterScript.forceToRice(0.1f, true, force_ratio, 0, 0, 0));
                StartCoroutine(foodMasterScript.forceToRice(0.2f, false, force_ratio, 3.0f, 0, 0));
                // 元のテキストを消す
                chahan_text.enabled = false;
                //Animated_Text.text = "Too strong...";


            }else if (acc.z > TH_middle - 1){
                gesture_judge_flag  = false;
                move_flyingpan_flag = true;
                move_option = 2;
                
                force_ratio = 1.3f;
                Debug.Log("Middle");
                
                // 具材の物理演算
                StartCoroutine(foodMasterScript.forceToRice(0.0f, false, force_ratio, -2.5f, 0, 0));
                StartCoroutine(foodMasterScript.forceToRice(0.1f, true, force_ratio, 0, 0, 0.2f));
                StartCoroutine(foodMasterScript.forceToRice(0.2f, false, force_ratio, 2.0f, 0, 0));
                chahan_text.enabled = false;

                if(game_phase_management == 2){                   
                    // score計算
                    score = foodMasterScript.calculatingScore(count_lower);
                    Debug.Log(score);
                    // score保存
                    PlayerPrefs.SetInt("score", (int)score);
                    PlayerPrefs.Save();
                    //シーン遷移
                    StartCoroutine(changeScene(1.0f));
                }else{
                    StartCoroutine(foodMasterScript.riceColorChange(0.5f, game_phase_management));
                }
                game_phase_management++;

                
            }else if (acc.z > TH_small - 1){
                gesture_judge_flag  = false;
                move_flyingpan_flag = true;
                move_option = 1;
                count_lower++;
                force_ratio = 1.0f;
                Debug.Log("Small");

                // 具材の物理演算
                StartCoroutine(foodMasterScript.forceToRice(0.0f, false, force_ratio, -1.5f, 0, 0));
                StartCoroutine(foodMasterScript.forceToRice(0.1f, true, force_ratio, 0, 0, 0.2f));
                StartCoroutine(foodMasterScript.forceToRice(0.2f, false, force_ratio, 1.0f, 0, 0));
                chahan_text.enabled = false;


            }
        }
        //フライパン動かす時
        if(move_flyingpan_flag){

            if(move_flyingpan_mode == 0){
                if(move_option == 3){
                    this.gameObject.transform.position = new Vector3 (pos.x - 0.05f, pos.y, pos.z);
                    this.gameObject.transform.Rotate(0,  - 1.5f, 0);
                }else if(move_option == 2){
                    this.gameObject.transform.position = new Vector3 (pos.x - 0.04f, pos.y, pos.z);
                    this.gameObject.transform.Rotate(0,  - 0.4f, 0);
                }else if(move_option == 1){
                    this.gameObject.transform.position = new Vector3 (pos.x - 0.03f, pos.y, pos.z);
                    this.gameObject.transform.Rotate(0,  - 0.3f, 0);
                }
                count++;
                if(count > 10){
                    count = 0;
                    move_flyingpan_mode = 1;
                }
            }else if(move_flyingpan_mode == 1){
                if(move_option == 3){
                    this.gameObject.transform.position = new Vector3 (pos.x, pos.y, pos.z + 0.05f);
                    this.gameObject.transform.Rotate(0,  3.0f, 0);
                }else if(move_option == 2){
                    this.gameObject.transform.position = new Vector3 (pos.x, pos.y, pos.z + 0.04f);
                    this.gameObject.transform.Rotate(0,  0.8f, 0);
                }else if(move_option == 1){
                    this.gameObject.transform.position = new Vector3 (pos.x, pos.y, pos.z + 0.03f);
                    this.gameObject.transform.Rotate(0,  0.6f, 0);
                }
                count++;
                if(count > 10){
                    count = 0;
                    move_flyingpan_mode = 2;
                }
            }else if(move_flyingpan_mode == 2){
                if(move_option == 3){
                    this.gameObject.transform.position = new Vector3 (pos.x + 0.05f, pos.y, pos.z - 0.05f);
                    this.gameObject.transform.Rotate(0,  -1.5f, 0);
                }else if(move_option == 2){
                    this.gameObject.transform.position = new Vector3 (pos.x + 0.04f, pos.y, pos.z - 0.04f);
                    this.gameObject.transform.Rotate(0,  -0.4f, 0);
                }else if(move_option == 1){
                    this.gameObject.transform.position = new Vector3 (pos.x + 0.03f, pos.y, pos.z - 0.03f);
                    this.gameObject.transform.Rotate(0,  -0.3f, 0);
                }
                count++;
                if(count > 10){
                    count = 0;
                    move_flyingpan_mode = 3;
                    StartCoroutine(changeState(0.5f));
                }
            }
        }		
    }
    public void GreenOnionButtonOnClick()
    {
        Debug.Log("onion");
        foodMasterScript.put_greenonion();
    }
    public void MeatButtonOnClick()
    {
        Debug.Log("meat");
        foodMasterScript.put_meat();
    }

    IEnumerator changeState(float delay){
        yield return new WaitForSeconds(delay);
        gesture_judge_flag  = true;
        move_flyingpan_flag = false;
        move_flyingpan_mode = 0;

        //表示テキストの変更
        int times = 3 - game_phase_management;
        chahan_text.enabled = true;
        chahan_text.text = "Shake your smartphone\n(" + times + "times)";
        
        Debug.Log("start");
        
    }

    
    IEnumerator changeScene(float delay)
    {
        //delay秒待つ
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("endScene");
    }
}