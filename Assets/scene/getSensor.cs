using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getSensor : MonoBehaviour
{
    Vector3 acc;
    Gyroscope gyro;
    Vector3 ret;
    
    static int TH = 4;
    bool flag = true; 
    // Start is called before the first frame update
    void Start()
    {
        // 入力にジャイロをONにする
        Input.gyro.enabled = true;
        // 入力にコンパスをONにする
        Input.compass.enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        acc = Input.acceleration;
        gyro = Input.gyro;
        ret = Input.compass.rawVector;
        if(acc.magnitude > TH){
            if(flag == true){
                Debug.Log("OK");
                flag = false;
            }
        }else{
            flag = true;
        }
        Debug.Log(gyro.attitude);
        transform.rotation = gyro.attitude;
        

        
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
}

