using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventaire : MonoBehaviour
{
    public static Inventaire instance;
    public Dictionary<Legume, int> inventaireLegumes;
    public Dictionary<Ingredient, int> inventaireIngredients;
    public List<Ingredient> listInventaireIngredients;
    public List<int> listInventaireIngredientsNumber;

    public int maxIngredientInventory;

    void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);//le GameObject qui porte ce script ne sera pas détruit
    }
    private void Start() {
        for(int i =0; i< listInventaireIngredients.Count ; i++){
            if(listInventaireIngredients[i].GetComponent<Legume>()  != null ){
                inventaireLegumes[listInventaireIngredients[i].GetComponent<Legume>()] = listInventaireIngredientsNumber[i];
            }
            inventaireIngredients[listInventaireIngredients[i]] = listInventaireIngredientsNumber[i];
        }
    }

    public Inventaire()
    {
        inventaireLegumes = new Dictionary<Legume,int>(); 
        inventaireIngredients = new Dictionary<Ingredient,int>();    
    }

    public void AddLegume(Legume legume)
    {
        foreach (KeyValuePair<Legume, int> ingredient in inventaireLegumes)  
        {  
            if(ingredient.Key.nom == legume.nom){
                inventaireLegumes[ingredient.Key] ++;
                inventaireIngredients[ingredient.Key]++;
                return;
            }
        } 
        
    }

    public void AddIngredient(Ingredient toAdd)
    {
        foreach (KeyValuePair<Ingredient, int> ingredient in inventaireIngredients)  
        {  
            if(ingredient.Key.nom == toAdd.nom){
                inventaireIngredients[ingredient.Key]++;
                return;
            }
        }   
    }
}