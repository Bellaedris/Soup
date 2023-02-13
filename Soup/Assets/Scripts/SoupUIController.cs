using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SoupUIController : MonoBehaviour
{
    private Soup soup;
    private Inventaire inventory;
    private Bounds soupBounds;
    private Renderer soupRenderer;

    public GameObject soupSurface;
    public ParticleSystem soupBubbles;
    public GameObject[] legumes;
    public GameObject inventoryItem;
    [SerializeField] private Canvas canvas;

    private void Start()
    {
        inventory = new Inventaire();
        soup = new Soup();
        soupRenderer = soupSurface.GetComponent<Renderer>();
        soupBounds = soupRenderer.bounds;
        Debug.Log("New Controller");
        generateInventoryUI();

        Debug.Log("soup bounds: " + soupBounds);
    }

    public void AddLegToSoup(Legume legume)
    {
        soup.AddLegume(legume);
        soupRenderer.material.SetColor("_Color", soup.computeColor());

        soupBubbles.GetComponent<Renderer>().material.SetColor("_Color", soup.computeColor());
        soupBubbles.startColor = soup.computeColor();
        AddMixedBitsToSoup(legume);
    }

    public void AddIngToSoup(Ingredient ingredient)
    {
        soup.AddIngredient(ingredient);
    }

    public void AddMixedBitsToSoup(Legume veg)
    {
        for(int i = 0; i < Random.Range(1, 1); i++)
        {
            Vector3 spawnPos = new Vector3 (
                Random.Range(soupBounds.min.x, soupBounds.max.x) * soupRenderer.transform.localScale.x,
                soupRenderer.transform.position.y + .5f,
                Random.Range(soupBounds.min.y, soupBounds.max.y) * soupRenderer.transform.localScale.x
            );
            Instantiate(veg.mixedObject, Vector3.back, Quaternion.identity, soupRenderer.transform).transform.localPosition = spawnPos; 
            
        }
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
        newItem.AddComponent<Legume>();
        newItem.GetComponent<DragDrop>().canvas = canvas;
        newItem.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { AddLegToSoup(legume); });
        newItem.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { RemoveVegFromInv(legume); });
        newItem.transform.GetChild(1).GetComponent<MeshFilter>().mesh = legume.objet;
        newItem.transform.GetChild(2).GetComponent<TMP_Text>().text = "x" + numberOfIngredient;
        return newItem;
    }
    
} 
