using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int maxIngredientInventory;
    public Guest guest;

    private void Awake() {
        if(instance!=null){
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);//le GameObject qui porte ce script ne sera pas détruit

    }

    public int InitNbVegeMarket(){
        int nbVegetables = 0;
        foreach (KeyValuePair<Ingredient, int> ingredient in Inventaire.instance.inventaireIngredients)  
        {  
            if(ingredient.Key.GetComponent<Legume>() != null){
                nbVegetables += ingredient.Value;
            }
        } 
        return nbVegetables;
    }
    public Dictionary<Ingredient, int> InitMarket(){
        Dictionary<Ingredient, int> ingredientInMarket = new  Dictionary<Ingredient, int>();
        foreach (KeyValuePair<Ingredient, int> ingredient in Inventaire.instance.inventaireIngredients)  
        {  
            ingredientInMarket[ingredient.Key] = maxIngredientInventory - ingredient.Value;
        } 

        return ingredientInMarket;
    }

    public void loadCuisineScene() 
    {
        if(instance.guest.characterName.Equals(""))
        {
            Debug.Log("Pick a guest please");
        } else
        {
            SceneManager.LoadScene(1);
        }
    }

    public void loadDinnerScene()
    {
        if (instance.guest.characterName.Equals(""))
        {
            Debug.Log("Pick a guest please");
        }
        else
        {
            SceneManager.LoadScene(4);
        }
    }

}
