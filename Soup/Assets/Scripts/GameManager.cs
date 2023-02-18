using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int maxIngredientInventory;
    public Guest guest;
    public List<Character> characterList;

    public Character[] character;
    public Soup[] soups;

    public List<Ingredient> ingredientsSoup;

    public GameObject characterBook;
    public GameObject recipeBook;

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
        Debug.Log("loadDinnerScene : "+ ingredientsSoup.Count);
        string s = TestRecipe();
        TestPreference(s);
        Debug.Log("Name soup : " + s);
        if (instance.guest.characterName.Equals(""))
        {
            Debug.Log("Pick a guest please");
        }
        else
        {
            SceneManager.LoadScene(4);
        }
    }

    public string TestRecipe()
    {
        foreach (Soup s in soups)
        {
            List<Ingredient> ingredientsRecipe = s.GetIngredients().ToList();
            if (ingredientsSoup.Count == ingredientsRecipe.Count)
            {
                foreach (Ingredient isoup in  ingredientsSoup)
                {
                    for (int i = 0; i < ingredientsRecipe.Count; i++)
                        if (isoup.name.Equals(ingredientsRecipe[i].name))
                            ingredientsRecipe.RemoveAt(i);
                    if (ingredientsRecipe.Count == 0)
                        return s.name;
                }
                
            }
        }
        return "commun";
    }

    public void TestPreference(string soupName)
    {
        if (character[0].favSoup.name.Equals(soupName))
            character[0].IsFavSoupKnown = true;
        foreach (Ingredient isoup in ingredientsSoup)
            for (int i = 0; i < character[0].favIngredients.Count; i++)
                if (isoup == character[0].favIngredients[i])
                    character[0].isFavIngredientsKnown[i] = true;
    }

    public void ToggleCharacterBook()
    {
        if (recipeBook.activeSelf)
            recipeBook.SetActive(false);
        characterBook.SetActive(!characterBook.activeSelf);
    }

    public void ToggleRecipeBook()
    {
        if (characterBook.activeSelf)
            characterBook.SetActive(false);
        recipeBook.SetActive(!recipeBook.activeSelf);
    }
}
