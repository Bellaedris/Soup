using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventaire_2 : MonoBehaviour
{
    public static Inventaire_2 instance;

    public List<Legume> inventaireLegumes;
    public List<Ingredient> inventaireIngredients;
   // Méthode d'accès statique (point d'accès global)
    //public static Inventaire_2 Instance { get; private set; }

     void Awake()
    {
        Debug.Log("Awake");

         if (instance != null && instance != this){
            Debug.Log("destroy");
            Destroy(gameObject);    // Suppression d'une instance précédente (sécurité...sécurité...)
         }
 
         instance = new Inventaire_2();


    }
    public Inventaire_2()
    {
        Debug.Log("Creation");
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

    //méthode appelée par les "clients" de cette classe
    public void loadFile(){ 
        Debug.Log("loadFile");

         //votre code 
    }

}
