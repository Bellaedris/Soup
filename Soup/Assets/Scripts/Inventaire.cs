using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventaire : MonoBehaviour
{
    public static Inventaire instance = null;
    public List<Legume> inventaireLegumes;
    public List<Ingredient> inventaireIngredients;



     void Awake()
    {
        Debug.Log("Awake");

         if (instance != null && instance != this){
            Debug.Log("destroy");
            Destroy(gameObject);    // Suppression d'une instance précédente (sécurité...sécurité...)
         }
 
         instance = new Inventaire();


    }
    public Inventaire()
    {
        inventaireLegumes = new List<Legume>(); 
        inventaireIngredients = new List<Ingredient>();    
    }

    public void AddLegume(Legume legume)
    {
        inventaireLegumes.Add(legume);
        Debug.Log("on rajouter : " + legume.nom);
    }

    public void AddIngredient(Ingredient ingredient)
    {
        inventaireIngredients.Add(ingredient);
    }
}
