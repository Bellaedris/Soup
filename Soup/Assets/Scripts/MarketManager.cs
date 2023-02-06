using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketManager : MonoBehaviour
{

    Dictionary<Ingredient, int> ingredient_to_put;
    private void Start() {
        Debug.Log(" Start() " );
        float instancy = 0;
        //Inventaire_2.Instance.loadFile();
        ingredient_to_put = GameManager.instance.InitMarket();

        GameObject PosObject = GameObject.FindWithTag("PosCarrots");
        Object ObjectPrefab = Resources.Load("carrotPrefab");
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
            instancy = 0;
            while(instancy < ingredient.Value){
                Instantiate(ObjectPrefab, PosObject.transform.position + new Vector3(0, instancy * 0.2f, 0), Quaternion.identity);
                instancy++;
            }
        } 

        //GameObject Carrot_prefab = GameObject.FindWithTag("Carrot");



        
        //Inventaire.instance.inventaireIngredients;
    }


    private void Update() {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hit;
        if(Input.GetMouseButtonUp(0) && Physics.Raycast(ray, out hit, Mathf.Infinity)){
            if(hit.collider.gameObject.GetComponent<Legume>() != null){
                
                Inventaire.instance.AddLegume(hit.collider.gameObject.GetComponent<Legume>());
                hit.collider.gameObject.transform.position = GameObject.FindWithTag("PosBasket").transform.position;
                //Destroy(hit.collider.gameObject);
            }

        }
    }
}
