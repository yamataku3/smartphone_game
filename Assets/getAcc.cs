using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getAcc : MonoBehaviour
{
    Vector3 dir;
    static int TH = 4;
    bool flag = true; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dir = Input.acceleration;
        if(dir.magnitude > TH){
            if(flag == true){
                Debug.Log("OK");
                flag = false;
            }
        }else{
            flag = true;
        }

        
    }
}
