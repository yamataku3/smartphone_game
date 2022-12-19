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
    public Material color1;
    public Material color2;
    float delay_time = 0.0f;
    bool move_rice_flag = true;
    float option = 0.0f;
    int fly_level = 0;

    void Start()
    {
        rices = new GameObject[riceN];
        for (int count = 0; count < riceN;)
        {
            float rand_x = Random.Range(-0.8f, 0.8f);
            float rand_y = Random.Range(-0.8f, 0.8f);
            if(rand_x*rand_x + rand_y*rand_y < 0.6)
            {
                rices[count] = Instantiate(rice, new Vector3(rand_x, rand_y, 0.7f), Quaternion.identity);
                rices[count].name = "Rice" + count.ToString();
                count++;
            }
            
        }
        GameObject fryingpan = GameObject.Find("OurFlyPan");
        //GameObject fryingpan = GameObject.Find("Cube");
        sensorscript = fryingpan.GetComponent<getSensor>();
    }
    // Update is called once per frame
    void Update(){
        if (move_rice_flag){          
            if (sensorscript.move_option == 3){
                Debug.Log("large" + fly_level);
                option = 2.0f;
                move_rice_flag = false;
                delay_time = 1.0f;
                Invoke(nameof(forceToRiceStart), delay_time);
                Invoke(nameof(forceToRice), delay_time + 0.2f);
                Invoke(nameof(forceToRiceEnd), delay_time + 0.4f);

                sensorscript.move_option = 0;
                Invoke(nameof(changeFlag), 2.5f);
            }
            else if (sensorscript.move_option == 2){   
                Debug.Log("middle" + fly_level);
                option = 1.5f;
                move_rice_flag = false;
                delay_time = 0.8f;
                Invoke(nameof(forceToRiceStart), delay_time);
                Invoke(nameof(forceToRice), delay_time + 0.2f);
                Invoke(nameof(forceToRiceEnd), delay_time + 0.4f);
                sensorscript.move_option = 0;
                Invoke(nameof(changeFlag), 2.5f);
                if (fly_level == 0)
                {
                    Invoke(nameof(fryRice1), 2.5f);
                    fly_level++;
                }else if (fly_level == 1)
                {
                    Invoke(nameof(fryRice2), 2.5f);
                    fly_level++;
                }else if (fly_level == 2)
                {
                    Debug.Log("finish!!!");
                }
                
            }
            else if (sensorscript.move_option == 1){
                move_rice_flag = false;
                option = 1.0f;
                Debug.Log("small" + fly_level);
                delay_time = 0.6f;
                Invoke(nameof(forceToRiceStart), delay_time);
                Invoke(nameof(forceToRice), delay_time + 0.2f);
                Invoke(nameof(forceToRiceEnd), delay_time + 0.4f);
                sensorscript.move_option = 0;
                Invoke(nameof(changeFlag), 2.5f);
            }
            else{
                option = 0.0f;
            }
            
        }
    }
    
    void fryRice1()
    {
        for (int i = 0; i < riceN; i++)
        {
            rices[i].GetComponent<Renderer>().material.color = color1.color;
        }
    }

    void fryRice2()
    {
        for (int i = 0; i < riceN; i++)
        {
            rices[i].GetComponent<Renderer>().material.color = color2.color;
        }
    }

    void forceToRice(){
        for (int i = 0; i < riceN; i++)
        {
            float force_x = Random.Range(-0.3f, 0.3f) * option;
            float force_y = Random.Range(-0.3f, 0.3f) * option;
            float force_z = Random.Range(2.0f, 4.0f) * option;
            Vector3 force = new Vector3(force_x, force_y, force_z);  // 力を設定
            rices[i].GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }
    }
    void forceToRiceStart()
    {
        Vector3 force_start = new Vector3(-1.8f, 0, 0);
        for (int i = 0; i < riceN; i++)
        {
            rices[i].GetComponent<Rigidbody>().AddForce(force_start, ForceMode.Impulse);
        }
    }
    void forceToRiceEnd()
    {
        Vector3 force_end = new Vector3(0.5f, 0, 0);
        
        for (int i = 0; i < riceN; i++)
        {
            rices[i].GetComponent<Rigidbody>().AddForce(force_end, ForceMode.Impulse);
        }
    }
    public void changeFlag()
    {
        move_rice_flag = true;
    }
}
