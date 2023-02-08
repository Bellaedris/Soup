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
        Debug.Log("Mouse is over " + nom);

    }
    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on " + nom);
    }
}


