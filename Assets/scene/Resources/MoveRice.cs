using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRice : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();  // rigidbodyを取得
        Vector3 force = new Vector3(0.0f, 0.0f, 0.01f);  // 力を設定
        rb.AddForce(force, ForceMode.Impulse);
    }
}
