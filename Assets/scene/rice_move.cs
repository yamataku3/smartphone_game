using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rice_move : MonoBehaviour
{
    [SerializeField] GameObject prefab_A;
    // Start is called before the first frame update
    void Start()
    {
        // Cubeプレハブを元に、インスタンスを生成、
        for (int i = 0; i < 1; i++){
            Instantiate (prefab_A, new Vector3(0.0f,1.0f,0.0f), Quaternion.identity);
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
