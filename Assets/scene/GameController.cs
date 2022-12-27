using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

// get sensor data
public class GameController: MonoBehaviour
{
    // the variance of getting sensor data.
    public Vector3 acc;
    public Gyroscope gyro;
    //threshold for deciding the flyingpan animation.
    static double TH_large = 3;
    static double TH_middle = 1.2;
    static double TH_small = 1.1;
    // management variances
    bool gesture_judge_flag = true; // ジェスチャを判定する状態か否か
    public int move_option = 0; // 動かし方を管理する変数
    public int game_phase_management = 0; //ゲームのフェーズ管理
    //[SerializeField] TextMeshProUGUI chahan_text;
    [SerializeField] Text chahan_text;// text
    [SerializeField] Text Animated_Text;//
    public float force_ratio;
    float delay_time = 0.0f;
    // Animator
    private Animator flyingpan_anim;
    private Animator text_anim;
    // RiceMaster
    private RiceMaster riceMasterScript;
    public int count_lower = 0;//焦げた回数をカウント
    // Start is called before the first frame update

    public double score;

    void Start()
    {
        // 入力にジャイロをONにする
        Input.gyro.enabled = true;
        Input.compass.enabled = true; 
        // get the flyingpan_animator
        flyingpan_anim = gameObject.GetComponent<Animator>();
        text_anim = GameObject.Find("Canvas/AnimatedText").GetComponent<Animator>();
        // get the RiceMaster
        riceMasterScript= GameObject.Find("GameObject").GetComponent<RiceMaster>();
        // text_anim.SetBool("TextAnimationFlag", false);
        Animated_Text.text = "";

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        acc = Input.acceleration;
        gyro = Input.gyro;
        //Debug.Log(acc.magnitude);
        if(gesture_judge_flag){
            
            if (acc.magnitude > TH_large){
                //Debug.Log("large");
                //Debug.Log("large" + fly_level);
                gesture_judge_flag  = false;
                move_option = 3;
                delay_time = 1.0f;
                force_ratio = 2.0f;
                // 物理演算
                StartCoroutine(riceMasterScript.forceToRice(delay_time, false, force_ratio, -1.8f, 0, 0));
                StartCoroutine(riceMasterScript.forceToRice(delay_time + 0.2f, true, force_ratio, 0, 0, 0));
                StartCoroutine(riceMasterScript.forceToRice(delay_time + 0.4f, false, force_ratio, 0.5f, 0, 0));
                // フライパンアニメーション
                flyingpan_anim.SetBool("AnimationFlag_large", true);
                StartCoroutine(changeAnimatorState(2.5f, "AnimationFlag_large", false));
                // 元のテキストを消す
                chahan_text.enabled = false;
                Animated_Text.text = "Too strong...";
                // テキストアニメーション
                Animated_Text.enabled = true;
                text_anim.SetTrigger("OnceAnim");
                
            }else if (acc.magnitude > TH_middle){
                gesture_judge_flag  = false;
                move_option = 2;
                delay_time = 0.8f;
                force_ratio = 1.3f;
                StartCoroutine(riceMasterScript.forceToRice(delay_time, false, force_ratio, -1.8f, 0, 0));
                StartCoroutine(riceMasterScript.forceToRice(delay_time + 0.2f, true, force_ratio, 0, 0, 0));
                StartCoroutine(riceMasterScript.forceToRice(delay_time + 0.4f, false, force_ratio, 0.5f, 0, 0));
                // フライパンアニメーション
                flyingpan_anim.SetBool("AnimationFlag_middle", true);
                StartCoroutine(changeAnimatorState(2.5f, "AnimationFlag_middle", false));
                Debug.Log(game_phase_management);
                // 元のテキストを消す
                chahan_text.enabled = false;
                Animated_Text.text = "GOOD!";
                // テキストアニメーション
                Animated_Text.enabled = true;
                text_anim.SetTrigger("OnceAnim");

                // 終了判定
                if(game_phase_management == 2){                   
                    //シーン遷移
                    score = riceMasterScript.calculatingScore(count_lower);
                    Debug.Log(score);
                    StartCoroutine(changeScene(2.5f));
                }else{
                    StartCoroutine(riceMasterScript.riceColorChange(2.5f, game_phase_management));
                }
                game_phase_management++;
                
                
            }else if (acc.magnitude > TH_small){

                //Debug.Log("small");
                count_lower++;
                gesture_judge_flag  = false;
                move_option = 1;
                delay_time = 0.6f;
                force_ratio = 1.0f;
                // 物理演算
                StartCoroutine(riceMasterScript.forceToRice(delay_time, false, force_ratio, -1.8f, 0, 0));
                StartCoroutine(riceMasterScript.forceToRice(delay_time + 0.2f, true, force_ratio, 0, 0, 0));
                StartCoroutine(riceMasterScript.forceToRice(delay_time + 0.4f, false, force_ratio, 0.5f, 0, 0));
                // フライパンアニメーション
                flyingpan_anim.SetBool("AnimationFlag_small", true);
                StartCoroutine(changeAnimatorState(2.5f, "AnimationFlag_small", false));
                
                // 元のテキストを消す
                chahan_text.enabled = false;
                Animated_Text.text = "Too Weak...";
                // テキストアニメーション
                Animated_Text.enabled = true;
                text_anim.SetTrigger("OnceAnim");
            }else{

            }
        }
    }

    IEnumerator changeAnimatorState(float delay, string text, bool flag){
        yield return new WaitForSeconds(delay);
        flyingpan_anim.SetBool(text, flag);
        gesture_judge_flag  = !flag;
        int times = 3 - game_phase_management;
        chahan_text.enabled = true;
        chahan_text.text = "Shake your smartphone\n(" + times + "times)";
        // textAnimation
        text_anim.SetTrigger("AnimatorBack");
        Animated_Text.enabled = false;
    }
    IEnumerator changeScene(float delay)
    {
        //delay秒待つ
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("endScene");
    }
    
}


