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
    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hit;
        if(Input.GetMouseButtonUp(0) && Physics.Raycast(ray, out hit, Mathf.Infinity)){
            if(hit.collider.gameObject.GetComponent<Legume>() != null){
                AddLegume(hit.collider.gameObject.GetComponent<Legume>());
            Debug.Log("tu as clicker sur moi");
            //Destroy(this);
            Destroy(hit.collider.gameObject);

            
            }

        }
    }
}
