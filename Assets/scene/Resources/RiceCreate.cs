using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RiceCreate : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject rice;
    int riceN = 1000;
    GameObject[] rices;
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
        
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        
        for (int i = 0; i < riceN; i++)
        {
            Vector3 force = new Vector3(0.2f, 0.1f, 3.0f);  // 力を設定
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
}
