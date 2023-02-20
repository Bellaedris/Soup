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

    private void OnMouseOver() {
        gameObject.layer = 7;
    }
    private void OnMouseEnter() {
        gameObject.layer = 0;
    }
    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        gameObject.layer = 0;
    }

    public Ingredient Clone()
    {
        return new Ingredient(nom, objet);
    }
}


