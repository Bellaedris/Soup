using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ErrorNotificationController errorNotificationController;

    public string guest;
    public Character[] characterList;

    public Character[] character;
    public Soup[] soups;

    public List<Ingredient> ingredientsSoup;
    public Animator Transition;

    private void Awake() {
        if(instance!=null){
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);//le GameObject qui porte ce script ne sera pas détruit

    }

    public Dictionary<Ingredient, int> InitMarket(){
        Dictionary<Ingredient, int> ingredientInMarket = new  Dictionary<Ingredient, int>();

        foreach (KeyValuePair<Legume, int> legume in Inventaire.instance.inventaireLegumes)  
        {  
            ingredientInMarket[legume.Key] = Inventaire.instance.maxIngredientInventory - legume.Value;
        } 

        foreach (KeyValuePair<Ingredient, int> ingredient in Inventaire.instance.inventaireIngredients)  
        {  
            ingredientInMarket[ingredient.Key] = Inventaire.instance.maxIngredientInventory - ingredient.Value;
        } 

        return ingredientInMarket;
    }

    public void loadKitchenScene() 
    {        
        StartCoroutine(LoadScene("KitchenUI"));        
    }
    
    public IEnumerator LoadScene(string scene){
        Transition.SetTrigger("Fade in");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);
        Transition.SetTrigger("Fade out");
        yield return new WaitForSeconds(1f);
    }
    public void loadDinnerScene(Soup finishedSoup)
    {
        
        if (guest == null || guest.Equals(""))
        {
            errorNotificationController.showNotification("Pick a guest please");
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
            StartCoroutine(LoadScene("Dinner")); 
        }
    }

    public void loadMarketScene()
    {
        StartCoroutine(LoadScene("Marché"));      
    }

    public void loadMorningScene()
    {
        StartCoroutine(LoadScene("MorningSceneWakyWaky"));      
    }


    public string TestRecipe()
    {
        Ingredient[] ingredients = this.GetComponents<Ingredient>();
        foreach (Soup s in soups)
        {
            List<Ingredient> ingredientsRecipe = s.GetIngredients().ToList();
            if (ingredients.Length == ingredientsRecipe.Count)
            {
                foreach (Ingredient isoup in ingredients)
                {
                    for (int i = 0; i < ingredientsRecipe.Count; i++)
                        if (isoup.nom.Equals(ingredientsRecipe[i].nom))
                            ingredientsRecipe.RemoveAt(i);
                    if (ingredientsRecipe.Count == 0)
                        return s.name;
                }
            }
        }
        return "commun";
    }

    public Sprite TestPreference(string soupName)
    {
        Ingredient[] ingredients = this.GetComponents<Ingredient>();

        foreach (Character c in character)
        {
            if (guest == c.name)
            {                
                foreach (Ingredient isoup in ingredients)
                {
                    for (int i = 0; i < c.favIngredients.Count; i++)
                    {
                        if (c.favSoup.name.Equals(soupName))
                        {
                            c.IsFavSoupKnown = true;
                            Debug.Log("affection + 5");
                            c.updateAffection(5);
                            c.PlayEmotionParticle("LoveParticles");
                            GameObject.FindObjectOfType<AudioManager>().Play("Soup Jingle");
                            return c.emotionSprites[2];
                        }
                        if (isoup.nom.Equals(c.favIngredients[i].nom))
                        {
                            c.isFavIngredientsKnown[i] = true;
                            Debug.Log("affection + 3");
                            c.updateAffection(3);
                            c.PlayEmotionParticle("HappyParticles");
                            GameObject.FindObjectOfType<AudioManager>().Play("Soup Jingle");
                            return c.emotionSprites[1];
                        }                        
                    }                    
                }
                c.updateAffection(1);
                c.PlayEmotionParticle("HappyParticles");
                GameObject.FindObjectOfType<AudioManager>().Play("Soup Jingle");
                return c.emotionSprites[0];
            }
        }
        Debug.Log("Character not found in testPreference");
        return character[0].emotionSprites[0];
    }

    public Sprite ChangeFaceWhenEatingSoup()
    {
        string s = TestRecipe();        
        Debug.Log("Name soup : " + s);
        return TestPreference(s);

    }

}















