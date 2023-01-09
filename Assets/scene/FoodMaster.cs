using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class FoodMaster : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject rice;
    [SerializeField] GameObject greenonion;
    [SerializeField] GameObject meat;
    int riceN = 400;
    int greenonionN = 100;
    int meatN = 50;
    public List<GameObject> ingredient_list = new List<GameObject>();
    public Material color1;
    public Material color2;
    public double score;
    public DetectionFallenObject detectionFallenObjectScript;
    void Start()
    {
        // generate rice
        //rices = new GameObject[riceN];
        for (int count = 0; count < riceN;)
        {
            float rand_x = Random.Range(-0.8f, 0.8f);
            float rand_y = Random.Range(-0.8f, 0.8f);
            if(rand_x*rand_x + rand_y*rand_y < 0.6)
            {
                ingredient_list.Add(Instantiate(rice, new Vector3(rand_x, rand_y, 0.7f), Quaternion.identity));
                ingredient_list[count].name = "Rice" + count.ToString();
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
            ingredient_list[i].GetComponent<Renderer>().material.color = rice_color.color;
        }
    }

    public IEnumerator forceToRice(float delay, bool random_flag, float option, float x, float y, float z){
        Vector3 force;
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < ingredient_list.Count; i++)
        {
            if(random_flag){
                float force_x = Random.Range(-0.3f, 0.3f);
                float force_y = Random.Range(-0.3f, 0.3f);
                float force_z = Random.Range(1.0f, 2.0f);
                //float force_z = Random.Range(2.0f, 4.0f);
                force = new Vector3(force_x * option + x, force_y * option + y, force_z * option + z);  // 力を設定
            }else{
                force = new Vector3(x, y, z);  // 力を設定
            }
            ingredient_list[i].GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }
    }

    //得点を計算
    public double calculatingScore(int i)
    {
        score = 100 - (double)detectionFallenObjectScript.count_fallen_object / (double)ingredient_list.Count * 100 - 5 * i;
        return score;
    }

    public void put_meat()
    {
        for (int count = 0; count < meatN;)
        {
            float rand_x = Random.Range(-0.8f, 0.8f);
            float rand_y = Random.Range(-0.8f, 0.8f);
            if (rand_x * rand_x + rand_y * rand_y < 0.6)
            {
                ingredient_list.Add(Instantiate(meat, new Vector3(rand_x, rand_y, 0.9f), Quaternion.identity));
                ingredient_list[count].name = "Meat" + count.ToString();
                count++;
            }
        }
    }

    public void put_greenonion()
    {
        for (int count = 0; count < greenonionN;)
        {
            float rand_x = Random.Range(-0.8f, 0.8f);
            float rand_y = Random.Range(-0.8f, 0.8f);
            if (rand_x * rand_x + rand_y * rand_y < 0.6)
            {
                ingredient_list.Add(Instantiate(greenonion, new Vector3(rand_x, rand_y, 0.9f), Quaternion.identity));
                ingredient_list[count].name = "GreenOnion" + count.ToString();
                count++;
            }
        }
    }
}
