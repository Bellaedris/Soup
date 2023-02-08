using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventaire : MonoBehaviour
{
    public List<Legume> inventaireLegumes;
    public List<Ingredient> inventaireIngredients;

    public Inventaire()
    {
        inventaireLegumes = new List<Legume>(); 
        inventaireIngredients = new List<Ingredient>();    
    }

    public void AddLegume(Legume legume)
    {
        inventaireLegumes.Add(legume);
    }

    public void AddIngredient(Ingredient ingredient)
    {
        inventaireIngredients.Add(ingredient);
    }
}
