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
    public string guest;
    public Character[] characterList;

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
        if(guest.Equals(""))
        {
            Debug.Log("Pick a guest please");
        } else
        {
            SceneManager.LoadScene(1);
        }
    }

    public void loadDinnerScene(Soup finishedSoup)
    {
        string s = TestRecipe();
        TestPreference(s);
        Debug.Log("Name soup : " + s);
        if (guest.Equals(""))
        {
            Debug.Log("Pick a guest please");
        }
        else
        {
            // adds a soup gameobject next to the manager that will live through the scene change
            Soup soup;
            if (gameObject.GetComponent<Soup>() == null) 
                soup = gameObject.AddComponent<Soup>() as Soup;
            else
                soup = gameObject.GetComponent<Soup>() as Soup;
            
            soup.ingredients = new List<Ingredient>();
            soup.nbLegumes = finishedSoup.nbLegumes;
            soup.colors = new List<Color>();

            // quickfix, adds scripts to the gameobject to pass soup data to next scene
            // for some reason, colors were just fine to add as we did but ingredients couldn't.
            // this is most likely due to the fact ingredients are monobehaviour.
            foreach(var ing in finishedSoup.ingredients)
            {
                Ingredient newIng = gameObject.AddComponent<Ingredient>() as Ingredient;
                newIng.nom = ing.nom;
            }

            foreach(var col in finishedSoup.colors)
            {
                soup.colors.Add(new Color(col.r, col.g, col.b));
            }

            SceneManager.LoadScene(4);
        }
    }

    public void loadMarketScene()
    {
        SceneManager.LoadScene(2);      
    }

    public void loadMorningScene()
    {
        SceneManager.LoadScene(6);
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
