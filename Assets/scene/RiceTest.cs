using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RiceTest : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject rice;

    void Start()
    {
        // Cube�v���n�u��GameObject�^�Ŏ擾
        //GameObject obj = (GameObject)Resources.Load("rice");
        // Cube�v���n�u�����ɁA�C���X�^���X�𐶐��A
        for(int count=0; count<100;)
        {
            float rand_x = Random.Range(-0.8f, 0.8f);
            float rand_z = Random.Range(-0.8f, 0.8f);
            if(rand_x*rand_x + rand_z*rand_z < 0.64)
            {
                Instantiate(rice, new Vector3(rand_x, 0.0f, rand_z), Quaternion.identity);
                count++;
            }
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
