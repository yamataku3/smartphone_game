using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using System.Text.RegularExpressions;


public class FoodMaster : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject rice;
    [SerializeField] GameObject greenonion;
    [SerializeField] GameObject meat;
    [SerializeField] GameObject egg;
    [SerializeField] GameObject shrimp;
    [SerializeField] GameObject kimchi;
    [SerializeField] GameObject crab;
    int riceN = 400;
    int greenonionN = 100;
    int meatN = 50;
    int eggN = 200;
    int shrimpN = 30;
    int kimchiN = 30;
    int crabN = 30;
    public List<GameObject> ingredient_list = new List<GameObject>();//実際に何が入っているか
    public List<GameObject> ingredient_prefab_list = new List<GameObject>();//prefabのリスト
    public Material color1;
    public Material color2;
    public Material color_kimchi0;
    public Material color_kimchi1;
    public Material color_kimchi2;
    public double score;
    int[] ingredientN;
    string[] ingredient_name = {"greenonion", "meat", "egg", "shrimp", "crab", "kimchi"};
    public bool[] is_each_ingredient_contains;
    public int ingredient_count = 0;//食材の数　( 米200 + ネギ100で300みたいな)

    public DetectionFallenObject detectionFallenObjectScript;
    void Start()
    {
        ingredientN = new int[ingredient_name.Length];

        is_each_ingredient_contains = new bool[6]{false, false, false, false, false, false};
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
                ingredient_count++;
                count++;

            }
        }
        ingredient_prefab_list.Add(greenonion);
        ingredientN[0] = greenonionN;
        ingredient_prefab_list.Add(meat);
        ingredientN[1] = meatN;
        ingredient_prefab_list.Add(egg);
        ingredientN[2] = eggN;
        ingredient_prefab_list.Add(shrimp);
        ingredientN[3] = shrimpN;
        ingredient_prefab_list.Add(crab);
        ingredientN[4] = crabN;
        ingredient_prefab_list.Add(kimchi);
        ingredientN[5] = kimchiN;
        
    }


    // Update is called once per frame
    void Update(){
    }
    
    
    public IEnumerator riceColorChange(float delay, int state)
    {
        //delay秒待つ
        Material rice_color;
        yield return new WaitForSeconds(delay);
        if(state == 0){
            if (is_each_ingredient_contains[is_each_ingredient_contains.Length - 1]){
                rice_color = color_kimchi1;
            }else{
                rice_color = color1;
            }
        }else if (state == 1){  
            if (is_each_ingredient_contains[is_each_ingredient_contains.Length - 1]){
                rice_color = color_kimchi2;
            }else{
                rice_color = color2;
            }
        }else{
            if (is_each_ingredient_contains[5]){
                rice_color = color_kimchi0;
            }
            rice_color = ingredient_list[0].GetComponent<Renderer>().material;
        }
        for (int i = 0; i < riceN; i++)
        {
            ingredient_list[i].GetComponent<Renderer>().material.color = rice_color.color;
        }
    }

    public IEnumerator forceToRice(float delay, bool random_flag, float option, float x, float y, float z){
        Vector3 force;
        yield return new WaitForSeconds(delay);
        
        List<int> indexList = new List<int>();

        for (int i = 0; i < ingredient_list.Count; i++){
            indexList.Add(i);
        }

        while(indexList.Count > 0){
            int index = Random.Range(0, indexList.Count);

            if(random_flag){
                float force_x = Random.Range(-0.3f, 0.3f);
                float force_y = Random.Range(-0.3f, 0.3f);
                float force_z = Random.Range(1.0f, 2.0f);
                //float force_z = Random.Range(2.0f, 4.0f);
                force = new Vector3(force_x * option + x, force_y * option + y, force_z * option + z);  // 力を設定
            }else{
                force = new Vector3(x, y, z);  // 力を設定
            }
            ingredient_list[indexList[index]].GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
            indexList.RemoveAt(index);
        }
    }

    //得点を計算
    public double calculatingScore(int i)
    {
        score = 100 - (double)detectionFallenObjectScript.count_fallen_object / (double)ingredient_list.Count * 100 - 5 * i;
        return System.Math.Max(score, 0);;
    }

    public void put_ingredient(int ingredient_index)
    {
        for (int count = 0; count < ingredientN[ingredient_index];)
        {
            float rand_x = Random.Range(-0.5f, 0.5f);
            float rand_y = Random.Range(-0.5f, 0.5f);
            //float rand_x = Random.Range(-0.8f, 0.8f);
            //float rand_y = Random.Range(-0.8f, 0.8f);
            if (rand_x * rand_x + rand_y * rand_y < 0.6)
            {
                ingredient_list.Add(Instantiate(ingredient_prefab_list[ingredient_index], new Vector3(rand_x, rand_y, 0.9f), Quaternion.identity));
                ingredient_list[ingredient_count].name = ingredient_name[ingredient_index] + ingredient_count.ToString();
                ingredient_count++;
                count++;
            }
        }
        is_each_ingredient_contains[ingredient_index] = true;
        //ingredient_list = ingredient_list.OrderBy(a => Guid.NewGuid()).ToList();
    }

    public string finished_fried_rice_type_decision()
    {
        string fried_rice_type = "normal_fried_rice";
        if (is_each_ingredient_contains[3] && is_each_ingredient_contains[4]){
            if (is_each_ingredient_contains[5]){
                fried_rice_type = "seafood_kimchi_fried_rice";
            }else{
                fried_rice_type = "seafood_fried_rice";
            }    
        }else if (is_each_ingredient_contains[5]){
            if (is_each_ingredient_contains[1]){
                fried_rice_type = "pig_kimchi_fried_rice";
            }else{
                fried_rice_type = "kimchi_fried_rice";
            }
        }else if (is_each_ingredient_contains[3]){
            fried_rice_type = "shrimp_fried_rice";
        }else if (is_each_ingredient_contains[4]){
            fried_rice_type = "crab_fried_rice";
        }else if (is_each_ingredient_contains[0] && is_each_ingredient_contains[1] && is_each_ingredient_contains[2]){
            fried_rice_type = "gomoku_fried_rice";
        }
        return fried_rice_type;
    }
}
