using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int maxIngredientInventory;
    public Guest guest;
    public List<Character> characterList;

    public Character[] character;

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
            ingredientInMarket[ingredient.Key] = Inventaire.instance.maxIngredientInventory - ingredient.Value;
        } 

        return ingredientInMarket;
    }

    public void loadCuisineScene() 
    {
        SceneManager.LoadScene(1);
        
        if(instance.guest.characterName.Equals("") || instance.guest == null )
        {
            Debug.Log("Pick a guest please");
        } else
        {
            SceneManager.LoadScene("KitchenUI");
        }
        
    }

    public void loadDinnerScene()
    {
        if (instance.guest == null || instance.guest.characterName.Equals(""))
        {
            Debug.Log("Pick a guest please");
        }
        else
        {
            SceneManager.LoadScene("Dinner");
        }
    }

}
