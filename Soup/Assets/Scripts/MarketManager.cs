using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemPrefab
{
    public GameObject PosObject;
    public GameObject ObjectPrefab;
}


public class MarketManager : MonoBehaviour
{

    Dictionary<Ingredient, int> ingredient_to_put;
    public List<ItemPrefab> list_prefab;
    public int minimumNumberVegetablesToBuy;

    public GameObject leftArrow;
    public GameObject rightArrow;

    private void Start() {
        Debug.Log(" Start() " );
        float instancy = 0;
        //Inventaire_2.Instance.loadFile();
        ingredient_to_put = GameManager.instance.InitMarket();
        minimumNumberVegetablesToBuy =minimumNumberVegetablesToBuy - GameManager.instance.InitNbVegeMarket();
        GameObject PosObject = GameObject.FindWithTag("PosCarrots");
        Object ObjectPrefab = Resources.Load("carrotPrefab");
        Debug.Log("minimumNumberVegetablestoBuy : " + minimumNumberVegetablesToBuy);

        
        foreach (KeyValuePair<Ingredient, int> ingredient in ingredient_to_put)  
        {  
            foreach (ItemPrefab item in list_prefab)
            { 
                if(ingredient.Key.nom == item.ObjectPrefab.GetComponent<Legume>().nom){
                    PosObject = item.PosObject;
                    ObjectPrefab = item.ObjectPrefab;
                    break;
                }
            }
            Debug.Log("je suis l'ingredient : " + ingredient.Key.nom + " nous sommes : " + ingredient.Value);
            instancy = 0;
            while(instancy < ingredient.Value){
                Instantiate(ObjectPrefab, PosObject.transform.position + new Vector3(0, instancy * 0.35f, 0), Quaternion.identity);
                instancy++;
            }
        } 
    }
    

    private void Update() {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hit;
        if(Input.GetMouseButtonUp(0) && Physics.Raycast(ray, out hit, Mathf.Infinity)){
            if(hit.collider.gameObject.GetComponent<Legume>() != null){
                Destroy(hit.collider.gameObject.GetComponent<Legume>());
                Inventaire.instance.AddLegume(hit.collider.gameObject.GetComponent<Legume>());
                hit.collider.gameObject.transform.position = GameObject.FindWithTag("PosBasket").transform.position;
            }
        }
/*
        if(Camera.main.transform.position.z > 0){
            Debug.Log("left set");

            leftArrow.SetActive(true);
        }else{
             Debug.Log("left deset");
            leftArrow.SetActive(false);
        }
        if(Camera.main.transform.position.z < 10){
             Debug.Log("right set");
            rightArrow.SetActive(true);
        }else{
             Debug.Log("right deset");
            rightArrow.SetActive(false);
        }
        */

    }


}
