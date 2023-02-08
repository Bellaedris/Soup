using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legume : Ingredient
{
    public bool isMixed;
    public Color couleur;
    public GameObject mixedObject;

    public Legume(string nom, Mesh objet, bool isMixed, Color couleur) : base(nom, objet)
    {
        this.isMixed = isMixed;
        this.couleur = couleur;
    }
}
