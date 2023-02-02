using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SoupUIController : MonoBehaviour
{
    private Soup soup;
    public GameObject soupSurface;
    public GameObject[] legumes;

    private void Start()
    {
        soup = new Soup();
        Debug.Log("New COntroller");
    }

    public void AddLeg(Legume legume)
    {
        soup.AddLegume(legume);
        Renderer renderer = soupSurface.GetComponent<Renderer>();
        renderer.material.color = soup.computeColor();
    }
}
