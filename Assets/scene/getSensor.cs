using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getSensor : MonoBehaviour
{
    Vector3 acc;
    Gyroscope gyro;
    Vector3 ret;
    Quaternion curCorrection = Quaternion.identity;
    Quaternion aimCorrection = Quaternion.identity;
    double old_CompassTime = 0;
    
    static int TH = 2;
    bool flag = true; // 終了判定

    private Animator anim;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        // 入力にジャイロをONにする
        Input.gyro.enabled = true;
        // 入力にコンパスをONにする
        Input.compass.enabled = true;
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(10000, 100000, 100000);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        acc = Input.acceleration;
        gyro = Input.gyro;
        ret = Input.compass.rawVector;
        
        if(acc.magnitude > TH){
            if(flag == true){
                Debug.Log("OK");
                flag = false;
                anim.SetBool("AnimationFlag", true);
                
                //Invoke(nameof(changeState), 1.0f);
            }else{

            }
        }else{
            flag = true;
            
        }

        /*
        
        Quaternion gorientation = changeAxis (Input.gyro.attitude);

        if (Input.compass.timestamp > old_CompassTime) {
            old_CompassTime = Input.compass.timestamp;

            Vector3 campassV = GetCompassRawVector();
            Vector3 gravityV = Input.gyro.gravity.normalized;
            Vector3 northV = campassV - Vector3.Dot (gravityV, campassV) * gravityV;

            Quaternion corientation = 
                changeAxis (Quaternion.Inverse (Quaternion.LookRotation (northV, -gravityV)));

            Quaternion tcorrection = 
                corientation * Quaternion.Inverse (gorientation) *　Quaternion.Euler (0, 0, 0);

            if (!isNaN (tcorrection)) {
                aimCorrection = tcorrection;
            }
        }

        if (Quaternion.Angle (curCorrection, aimCorrection) < 45) {
            curCorrection = Quaternion.Slerp (curCorrection, aimCorrection, 0.02f);
        } else {
            curCorrection = aimCorrection;
        }

        transform.localRotation = curCorrection * gorientation;
        */
        
    }
    void changeState(){
        anim.SetBool("AnimationFlag", false);
        Debug.Log("test");
    }
    static Vector3 GetCompassRawVector() {
        Vector3 ret = Input.compass.rawVector;
        if (Application.platform == RuntimePlatform.Android) 
        {
            // Androidでは、rawVectorの軸を変換
            switch (Screen.orientation) {
                case ScreenOrientation.LandscapeLeft:
                    ret = new Vector3 (-ret.y, ret.x, ret.z);
                    break;

                case ScreenOrientation.LandscapeRight:
                    ret = new Vector3 (ret.y, -ret.x, ret.z);
                    break;

                case ScreenOrientation.PortraitUpsideDown:
                    ret = new Vector3 (-ret.x, -ret.y, ret.z);
                    break;
            }
        }
        return ret;
    }
    static Quaternion changeAxis (Quaternion q)
    {
        var euler = q.eulerAngles;
        return Quaternion.Euler(-euler.x, -euler.y, euler.z);
    }

    static bool isNaN (Quaternion q)
    {
        bool ret =
            float.IsNaN (q.x) || float.IsNaN (q.y) ||
            float.IsNaN (q.z) || float.IsNaN (q.w) ||
            float.IsInfinity (q.x) || float.IsInfinity (q.y) ||
            float.IsInfinity (q.z) || float.IsInfinity (q.w);

        return ret;
    }
    /*
    public void moveByAddForce(){
        rigidbody.AddForce(rigidbody.mass * Vector3.right * speed / Time.fixedDeltaTime, ForceMode.Force);

    }
    */
}

