using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SoupUIController : MonoBehaviour
{
    private Soup soup;
    private Inventaire inventory;
    public GameObject soupSurface;
    public ParticleSystem soupBubbles;
    public GameObject[] legumes;

    private void Start()
    {
        inventory = new Inventaire();
        soup = new Soup();
        Debug.Log("New COntroller");
    }

    public void AddLegToSoup(Legume legume)
    {
        soup.AddLegume(legume);
        Renderer renderer = soupSurface.GetComponent<Renderer>();
        renderer.material.SetColor("_Color", soup.computeColor());

        soupBubbles.GetComponent<Renderer>().material.SetColor("_Color", soup.computeColor());
        soupBubbles.startColor = soup.computeColor();
    }

    public void AddIngToSoup(Ingredient ingredient)
    {
        soup.AddIngredient(ingredient);
    }

    public void AddLegToInv(Legume legume)
    {

    }
}
