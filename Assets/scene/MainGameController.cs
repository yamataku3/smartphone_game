using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

// get sensor data
public class MainGameController: MonoBehaviour
{
    // the variance of getting sensor data.
    public Vector3 acc;
    public Gyroscope gyro;
    public Vector3 pos;
    //threshold for deciding the flyingpan animation.
    double TH_large = 2.2;
    double TH_middle = 1.2;
    double TH_small = 1.01;
    
    int count = 0;//
    // management variances
    bool gesture_judge_flag = true; // ジェスチャを判定する状態か否か
    bool move_flyingpan_flag = false; // flying_panを動かすか否か
    int move_flyingpan_mode = 0;// flying_panを動かす際の状態遷移

    public int move_option = 0; // 動かし方を管理する変数
    public int game_phase_management = 0; //ゲームのフェーズ管理
    
    [SerializeField] Text chahan_text;// text
    //[SerializeField] Text Animated_Text;//
    [SerializeField] Button put_ingredient_button;
    [SerializeField] Button left_button;
    [SerializeField] Button right_button;

    public float force_ratio;
    float delay_time = 0.0f;
    // Animator
    //private Animator text_anim;
    // FoodMaster
    private FoodMaster foodMasterScript;
    public int count_lower = 0;//焦げた回数をカウント
    public int count_middle = 0;
    public int count_ingredient = 0;

    public double score;



    // Audio clip
    AudioSource audio_source_se;
    public AudioClip audio_clip_se;

    public List<string> adding_ingredient_list = new List<string>() { "green_onion", "meat", "egg", "shrimp", };
    public int ingredient_index = 0;

    public Sprite greenonion_sprite;
    public Sprite meat_sprite;
    public Sprite egg_sprite;
    public Sprite shrimp_sprite;
    public List<Sprite> ingredient_sprite_list = new List<Sprite>();
    public int difficulty;
    void Start()
    {
        // 入力にジャイロをONにする
        Input.gyro.enabled = true;
        Input.compass.enabled = true;
        // get the FoodMaster
        foodMasterScript= GameObject.Find("GameObject").GetComponent<FoodMaster>();
        //食材を追加するためのボタン
        put_ingredient_button.onClick.AddListener(IngredientButtonOnClick);
        left_button.onClick.AddListener(LeftButtonOnClick);
        right_button.onClick.AddListener(RightButtonOnClick);
        // オーディオ
        audio_source_se = GetComponent<AudioSource>();
        ingredient_sprite_list.Add(greenonion_sprite);
        ingredient_sprite_list.Add(meat_sprite);
        ingredient_sprite_list.Add(egg_sprite);
        ingredient_sprite_list.Add(shrimp_sprite);
        // 難易度取得
        int difficulty = PlayerPrefs.GetInt("difficulty");
        Debug.Log(difficulty);
        if(difficulty == 2){
            // usual
            TH_large = 2.2;
            TH_middle = 1.2;
            TH_small = 1.01;
        }else if (difficulty == 1){
            // easy
            TH_large = 3.0;
            TH_middle = 1.4;
            TH_small = 1.01;
        }else if (difficulty == 3){
            // hard
            TH_large = 2.2;
            TH_middle = 1.8;
            TH_small = 1.01;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        acc = Input.acceleration;
        gyro = Input.gyro;
        pos = this.gameObject.transform.position;
        //ジェスチャ判定する時
        if(gesture_judge_flag){
            if (acc.z + 1> TH_large){
                gesture_judge_flag = false;
                move_flyingpan_flag = true;
                move_option = 3;

                force_ratio = 2.0f;
                Debug.Log("Large");
                Debug.Log(acc.z + 1);
                // 具材の物理演算
                StartCoroutine(foodMasterScript.forceToRice(0.0f, false, force_ratio, -4.5f, 0, 0));
                StartCoroutine(foodMasterScript.forceToRice(0.1f, true, force_ratio, 0, 0, 0));
                StartCoroutine(foodMasterScript.forceToRice(0.2f, false, force_ratio, 4.0f, 0, 0));
                StartCoroutine(foodMasterScript.forceToRice(0.27f, false, force_ratio, 0, 0, 0));
                audio_source_se.PlayOneShot(audio_clip_se);
                
                // 元のテキストを消す
                //chahan_text.enabled = false
                //Animated_Text.text = "Too strong...";
                chahan_text.text = "Too strong...";
            }else if (acc.z + 1 > TH_middle){
                gesture_judge_flag  = false;
                move_flyingpan_flag = true;
                move_option = 2;
                
                force_ratio = 1.3f;
                Debug.Log("Middle");
                Debug.Log(acc.z + 1);
                
                // 具材の物理演算
                StartCoroutine(foodMasterScript.forceToRice(0.0f, false, force_ratio, -2.5f, 0, 0));
                StartCoroutine(foodMasterScript.forceToRice(0.1f, true, force_ratio, 0, 0, 0.2f));
                StartCoroutine(foodMasterScript.forceToRice(0.2f, false, force_ratio, 2.0f, 0, 0));
                audio_source_se.PlayOneShot(audio_clip_se);
                StartCoroutine(foodMasterScript.forceToRice(0.27f, false, force_ratio, 0, 0, 0));
                // 元のテキストを消す
                //chahan_text.enabled = false;
                chahan_text.text = "Very Good!!!";

                if(game_phase_management == 3){                   
                    // score計算
                    score = foodMasterScript.calculatingScore(count_lower);
                    Debug.Log(score);
                    // score保存
                    PlayerPrefs.SetInt("score", (int)score);
                    PlayerPrefs.Save();
                    //シーン遷移
                    StartCoroutine(changeScene(1.0f));
                }
                count_middle++;

                if (count_middle % 3 == 0){
                    StartCoroutine(foodMasterScript.riceColorChange(0.5f, game_phase_management));
                    game_phase_management++;
                }
                
            }else if (acc.z + 1 > TH_small){
                gesture_judge_flag  = false;
                move_flyingpan_flag = true;
                move_option = 1;
                count_lower++;
                force_ratio = 1.0f;
                Debug.Log("Small");
                audio_source_se.PlayOneShot(audio_clip_se);
                // 具材の物理演算
                StartCoroutine(foodMasterScript.forceToRice(0.0f, false, force_ratio, -1.5f, 0, 0));
                StartCoroutine(foodMasterScript.forceToRice(0.1f, true, force_ratio, 0, 0, 0.2f));
                StartCoroutine(foodMasterScript.forceToRice(0.2f, false, force_ratio, 1.0f, 0, 0));
                StartCoroutine(foodMasterScript.forceToRice(0.27f, false, force_ratio, 0, 0, 0));
                // 元のテキストを消す
                //chahan_text.enabled = false;
                chahan_text.text = "Too week...";

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

    public void IngredientButtonOnClick()
    {
        Debug.Log(adding_ingredient_list.Count);
        foodMasterScript.put_ingredient(ingredient_index);
        count_ingredient++;
        if (count_ingredient >= 3){
            put_ingredient_button.interactable = false;
        }
    }
    public void LeftButtonOnClick()
    {
        ingredient_index -= 1;
        if (ingredient_index < 0){
            ingredient_index = adding_ingredient_list.Count;
        }
        put_ingredient_button.GetComponent<Image>().sprite = ingredient_sprite_list[ingredient_index]; 
        Debug.Log("left");
    }

    public void RightButtonOnClick()
    {
        ingredient_index += 1;
        if (ingredient_index > adding_ingredient_list.Count){
            ingredient_index = 0;
        }
        put_ingredient_button.GetComponent<Image>().sprite = ingredient_sprite_list[ingredient_index];       
        Debug.Log("right");
    }
    

    IEnumerator changeState(float delay){
        yield return new WaitForSeconds(delay);
        gesture_judge_flag  = true;
        move_flyingpan_flag = false;
        move_flyingpan_mode = 0;
        audio_source_se.Stop();

        //表示テキストの変更
        int times = 10 - count_middle;
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