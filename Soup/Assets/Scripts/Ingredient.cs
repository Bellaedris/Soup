using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public string nom;
    public GameObject objet;

    public Ingredient(string nom, GameObject objet)
    {
        this.nom = nom;
        this.objet = objet;
    }
}


