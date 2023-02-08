using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketManager : MonoBehaviour
{

    Dictionary<Ingredient, int> ingredient_to_put;
    public int minimumNumberVegetablesToBuy;
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
            Debug.Log("je suis l'ingredient : " + ingredient.Key.nom + " nous sommes : " + ingredient.Value);
            if(ingredient.Key.nom == "Carrot"){
                PosObject = GameObject.FindWithTag("PosCarrots");
                ObjectPrefab = Resources.Load("carrotPrefab");
            }
            else if(ingredient.Key.nom == "Tomato"){
                PosObject = GameObject.FindWithTag("PosTomato");
                ObjectPrefab = Resources.Load("tomatoPrefab");
            }
            else if(ingredient.Key.nom == "Broccoli"){
                PosObject = GameObject.FindWithTag("PosBroccoli");
                ObjectPrefab = Resources.Load("broccoliPrefab");
            }
            else if(ingredient.Key.nom == "Leak"){
                PosObject = GameObject.FindWithTag("PosLeak");
                ObjectPrefab = Resources.Load("LeakPrefab");
            }
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
    }
}
