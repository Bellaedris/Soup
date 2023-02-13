using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SoupUIController : MonoBehaviour
{
    private Soup soup;
    private Inventaire inventory;
    public GameObject soupSurface;
    public ParticleSystem soupBubbles;
    public GameObject[] legumes;
    public GameObject inventoryItem;
    [SerializeField] private Canvas canvas;

    private void Start()
    {
        inventory = new Inventaire();
        soup = new Soup();
        Debug.Log("New Controller");
        generateInventoryUI();
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

    public void RemoveVegFromInv(Legume leg)
    {
        Inventaire inventaire = Inventaire.instance;

        int currentValue = inventaire.inventaireLegumes[leg];
        inventaire.inventaireLegumes[leg] = currentValue - 1;
        UpdateInventoryUI();

    }
    
    private void UpdateInventoryUI()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("inventoryItem");
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
        generateInventoryUI();
    }
    

    public void AddLegToInv()
    {
        Debug.Log("feur");   
    }

    public void generateInventoryUI()
    {
        Debug.Log("Generate Inventory");
        
        Inventaire inventaire = Inventaire.instance;
        GameObject itemSpawner = GameObject.FindGameObjectWithTag("ItemSpawner");
        foreach (KeyValuePair<Legume, int> kvp in inventaire.inventaireLegumes)
        {
            Debug.Log(kvp.Key.nom);
            if (kvp.Value > 0)
            {
                GameObject newItem = createInventoryItem(itemSpawner.transform, kvp.Value, kvp.Key);
                newItem.transform.parent = itemSpawner.transform;
            }
        }
    }
    public GameObject createInventoryItem(Transform itemSpawner, int numberOfIngredient, Legume legume)
    {
        GameObject newItem;
        newItem = Instantiate(inventoryItem, itemSpawner);
        newItem.AddComponent<Legume>();
        newItem.GetComponent<Legume>().couleur = legume.couleur;
        newItem.GetComponent<Legume>().name = legume.name;
        newItem.GetComponent<Legume>().nom = legume.nom;
        newItem.GetComponent<Legume>().objet = legume.objet;
        newItem.GetComponent<Legume>().isMixed = legume.isMixed;
        newItem.GetComponent<DragDrop>().canvas = canvas;
        newItem.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { AddLegToSoup(legume); });
        newItem.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { RemoveVegFromInv(legume); });
        newItem.transform.GetChild(1).GetComponent<MeshFilter>().mesh = legume.objet;
        newItem.transform.GetChild(2).GetComponent<TMP_Text>().text = "x" + numberOfIngredient;
        return newItem;
    }
} 
