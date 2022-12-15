using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RiceMaster : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject rice;
    int riceN = 1000;
    GameObject[] rices;
    getSensor sensorscript;

    bool move_rice_flag = true;
    int option = 0;
    void Start()
    {
        rices = new GameObject[riceN];
        for (int count = 0; count < riceN;)
        {
            float rand_x = Random.Range(-0.8f, 0.8f);
            float rand_y = Random.Range(-0.8f, 0.8f);
            if(rand_x*rand_x + rand_y*rand_y < 0.6)
            {
                rices[count] = Instantiate(rice, new Vector3(rand_x, rand_y, 0.5f), Quaternion.identity);
                rices[count].name = "Rice" + count.ToString();
                count++;
            }
            
        }
        GameObject fryingpan = GameObject.Find("fryingpan");
        //GameObject fryingpan = GameObject.Find("Cube");
        sensorscript = fryingpan.GetComponent<getSensor>();
    }
    // Update is called once per frame
    void Update(){
        if (move_rice_flag){
            option = sensorscript.move_option;
            move_rice_flag = false;
            Invoke(nameof(forceToRice), 0.1f);
            sensorscript.move_option = 0;
            Invoke(nameof(changeFlag), 2.5f);
            /*
            if (sensorscript.move_option == 3){
                Debug.Log("large");
                move_rice_flag = false;
                Invoke(nameof(forceToRice), 2.5f);
                sensorscript.move_option = 0;
                Invoke(nameof(changeFlag), 2.5f);
            }else if (sensorscript.move_option == 2){   
                Debug.Log("middle");
                move_rice_flag = false;
                Invoke(nameof(forceToRice), 2.5f);
                sensorscript.move_option = 0;
                Invoke(nameof(changeFlag), 2.5f);
            }else if (sensorscript.move_option == 1){
                move_rice_flag = false;
                Debug.Log("small");
                Invoke(nameof(forceToRice), 2.5f);
                sensorscript.move_option = 0;
                Invoke(nameof(changeFlag), 2.5f);
            }else{

            }
            */
        }
    }
    
    void forceToRice(){
        int k = option;
        for (int i = 0; i < riceN; i++)
        {
            float force_x = Random.Range(-0.3f, 0.3f) * k;
            float force_y = Random.Range(-0.3f, 0.3f) * k;
            float force_z = Random.Range(0.0f, 2.0f) * k;
            Vector3 force = new Vector3(force_x, force_y, force_z);  // 力を設定
            rices[i].GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }    
        /*
         * 
        rice = GameObject.Find("Rice1");
        Vector3 force = new Vector3(0.2f, 0.1f, 3.0f);  // 力を設定
        rice.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        Debug.Log("ok");
        */

    }
    public void changeFlag()
    {
        move_rice_flag = true;
    }
}
