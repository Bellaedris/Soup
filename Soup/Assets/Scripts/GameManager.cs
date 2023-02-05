using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int maxIngredientInventory;

    private void Awake() {
        Debug.Log(" Awake() " );
        if(instance!=null){
            Debug.Log("gamemanager existe deja");
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);//le GameObject qui porte ce script ne sera pas détruit


    }

    public Dictionary<Ingredient, int> InitMarket(){
        Debug.Log(" InitMarket() " );

        Debug.Log("gamemanager 2 : " + Inventaire.instance.inventaireIngredients.Count);

        Dictionary<Ingredient, int> ingredientInMarket = new  Dictionary<Ingredient, int>();
        foreach (KeyValuePair<Ingredient, int> ingredient in Inventaire.instance.inventaireIngredients)  
        {  
            ingredientInMarket[ingredient.Key] = maxIngredientInventory - ingredient.Value;
        } 

        return ingredientInMarket;
    }
}
