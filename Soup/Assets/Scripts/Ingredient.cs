using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public string nom;
    public Mesh objet;

    public Ingredient(string nom, Mesh objet)
    {
        this.nom = nom;
        this.objet = objet;
    }
}


