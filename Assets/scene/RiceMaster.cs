using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class RiceMaster : MonoBehaviour
{
    
    // Start is called before the first frame update
    [SerializeField] GameObject rice;
    int riceN = 1000;
    GameObject[] rices;
    public Material color1;
    public Material color2;
    void Start()
    {
        // generate ri
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
    }


    // Update is called once per frame
    void Update(){
    }
    
    
    public IEnumerator riceColorChange(float delay, int state)
    {
        Debug.Log("test");
        //delay秒待つ
        Material rice_color;
        yield return new WaitForSeconds(delay);
        if(state == 0){
            rice_color = color1;
        }else{
            rice_color = color2;  
        }
        for (int i = 0; i < riceN; i++)
        {
            rices[i].GetComponent<Renderer>().material.color = rice_color.color;
        }
    }

    public IEnumerator forceToRice(float delay, bool random_flag, float option, float x, float y, float z){
        Vector3 force;
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < riceN; i++)
        {
            if(random_flag){
                float force_x = Random.Range(-0.3f, 0.3f);
                float force_y = Random.Range(-0.3f, 0.3f);
                float force_z = Random.Range(2.0f, 4.0f);
                force = new Vector3(force_x * option , force_y * option , force_z * option);  // 力を設定
            }else{
                force = new Vector3(x, y, z);  // 力を設定
            }
            rices[i].GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }
    }
}
