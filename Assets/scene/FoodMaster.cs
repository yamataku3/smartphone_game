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
    [SerializeField] GameObject egg;
    int riceN = 400;
    int greenonionN = 100;
    int meatN = 50;
    int eggN = 200;
    public List<GameObject> ingredient_list = new List<GameObject>();
    public List<GameObject> ingredient_prefab_list = new List<GameObject>();
    public Material color1;
    public Material color2;
    public double score;
    int[] ingredientN = new int[3];
    string[] ingredient_name = {"greenonion", "meat", "egg"};



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
        ingredient_prefab_list.Add(greenonion);
        ingredientN[0] = greenonionN;
        ingredient_prefab_list.Add(meat);
        ingredientN[1] = meatN;
        ingredient_prefab_list.Add(egg);
        ingredientN[2] = eggN;
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
         
         float rand_x = Random.Range(-0.5f, 0.5f);
            float rand_y = Random.Range(-0.5f, 0.5f);   rice_color = color2;  
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
            float rand_x = Random.Range(-0.5f, 0.5f);
            float rand_y = Random.Range(-0.5f, 0.5f);
            //float rand_x = Random.Range(-0.8f, 0.8f);
            //float rand_y = Random.Range(-0.8f, 0.8f);
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
            float rand_x = Random.Range(-0.5f, 0.5f);
            float rand_y = Random.Range(-0.5f, 0.5f);
            //float rand_x = Random.Range(-0.8f, 0.8f);
            //float rand_y = Random.Range(-0.8f, 0.8f);
            if (rand_x * rand_x + rand_y * rand_y < 0.6)
            {
                ingredient_list.Add(Instantiate(greenonion, new Vector3(rand_x, rand_y, 0.9f), Quaternion.identity));
                ingredient_list[count].name = "GreenOnion" + count.ToString();
                count++;
            }
        }
    }

    public void put_egg()
    {
        for (int count = 0; count < eggN;)
        {
            float rand_x = Random.Range(-0.5f, 0.5f);
            float rand_y = Random.Range(-0.5f, 0.5f);
            //float rand_x = Random.Range(-0.8f, 0.8f);
            //float rand_y = Random.Range(-0.8f, 0.8f);
            if (rand_x * rand_x + rand_y * rand_y < 0.6)
            {
                ingredient_list.Add(Instantiate(egg, new Vector3(rand_x, rand_y, 0.9f), Quaternion.identity));
                ingredient_list[count].name = "Egg" + count.ToString();
                count++;
            }
        }
    }

    public void put_ingredient(int ingredient_num)
    {
        for (int count = 0; count < ingredientN[ingredient_num];)
        {
            float rand_x = Random.Range(-0.5f, 0.5f);
            float rand_y = Random.Range(-0.5f, 0.5f);
            //float rand_x = Random.Range(-0.8f, 0.8f);
            //float rand_y = Random.Range(-0.8f, 0.8f);
            if (rand_x * rand_x + rand_y * rand_y < 0.6)
            {
                ingredient_list.Add(Instantiate(ingredient_prefab_list[ingredient_num], new Vector3(rand_x, rand_y, 0.9f), Quaternion.identity));
                ingredient_list[count].name = ingredient_name[ingredient_num] + count.ToString();
                count++;
            }
        }
    }
}
