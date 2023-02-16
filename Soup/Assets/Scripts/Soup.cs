using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Soup : MonoBehaviour
{
    [SerializeField]
    private List<Ingredient> ingredients;
    private int nbLegumes;
    private List<Color> colors;
    private void Start()
    {
        Debug.Log("Soupe créée");
        ingredients = new List<Ingredient>();
        nbLegumes = 0;
        colors = new List<Color>();
    }

    public Soup()
    {
        ingredients = new List<Ingredient>();
        nbLegumes = 0;
        colors = new List<Color>();
    }

    public Color computeColor()
    {
        float r = 0, g = 0 , b = 0;
        int sizeIngredients = ingredients.Count;

        foreach(Color color in colors)
        {
            r += color.r;
            g += color.g;
            b += color.b;
        }

        return new Color(r / sizeIngredients, g / sizeIngredients, b / sizeIngredients);

    }

    public void AddLegume(Legume legume)
    {
        nbLegumes++;
        if (legume.isMixed)
        {
            Color color = legume.couleur;
            colors.Add(color);
        }
        ingredients.Add(legume);
    }

    public void AddIngredient(Ingredient ingredient)
    {
        ingredients.Add(ingredient);        
    }

    public List<Ingredient> GetIngredients()
    {
        return ingredients;
    }

    public bool containsIngredient(Ingredient i)
    {
        return ingredients.Contains(i);
    }
}
