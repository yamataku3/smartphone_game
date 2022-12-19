using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;


// get sensor data
public class GameController: MonoBehaviour
{
    // the variance of getting sensor data.
    public Vector3 acc;
    public Gyroscope gyro;
    //threshold for deciding the animation.
    static double TH_large = 2.5;
    static double TH_middle = 2;
    static double TH_small = 1.5;
    // management variances
    bool gesture_judge_flag = true; // ジェスチャを判定する状態か否か
    public int move_option = 0; // 動かし方を管理する変数
    
    // Animator
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        // 入力にジャイロをONにする
        Input.gyro.enabled = true;
        Input.compass.enabled = true; 
        anim = gameObject.GetComponent<Animator>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        acc = Input.acceleration;
        gyro = Input.gyro;
        //Debug.Log(acc.magnitude);
        if (acc.magnitude > TH_large){
            if(gesture_judge_flag  == true){
                //Debug.Log("large");
                gesture_judge_flag  = false;
                move_option = 3;
                anim.SetBool("AnimationFlag_large", true);
                Invoke(nameof(changeStateLarge), 1.5f);
            }
        }else if (acc.magnitude > TH_middle){
            if (gesture_judge_flag == true)
            {
                //.Log("middle");
                gesture_judge_flag  = false;
                move_option = 2;
                anim.SetBool("AnimationFlag_middle", true);
                Invoke(nameof(changeStateMiddle), 1.5f);
            }
        }else if (acc.magnitude > TH_small){
            if (gesture_judge_flag  == true)
            {
                //Debug.Log("small");
                gesture_judge_flag  = false;
                move_option = 1;
                anim.SetBool("AnimationFlag_small", true);
                Invoke(nameof(changeStateSmall), 1.5f);
            }
        }else{
            if(acc.magnitude < 1.1 && acc.magnitude > 0.9)
            {
                //flag = true;
            }
        }
        
    }
    // Animator内の
    void changeStateLarge(){
        anim.SetBool("AnimationFlag_large", false);
        gesture_judge_flag  = true;
    }

    void changeStateMiddle()
    {
        anim.SetBool("AnimationFlag_middle", false);
        gesture_judge_flag  = true;
    }

    void changeStateSmall()
    {
        anim.SetBool("AnimationFlag_small", false);
        gesture_judge_flag  = true;
    }
}


