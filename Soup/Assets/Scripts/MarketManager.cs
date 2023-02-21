using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct ItemPrefab
{
    public GameObject PosObject;
    public GameObject ObjectPrefab;
}


public class MarketManager : MonoBehaviour
{
    Dictionary<Ingredient, int> ingredient_to_put;
    public List<ItemPrefab> list_prefab;
    public int minimumNumberVegetablesToBuy;
    public List<Sprite> listGuest;
    public Image currentGuest;

    private void Start() {
        float instancy = 0;
        //Inventaire_2.Instance.loadFile();
        ingredient_to_put = GameManager.instance.InitMarket();
        minimumNumberVegetablesToBuy =minimumNumberVegetablesToBuy - GameManager.instance.InitNbVegeMarket();
        GameObject PosObject = GameObject.FindWithTag("PosCarrots");
        Object ObjectPrefab = Resources.Load("carrotPrefab");

        foreach (KeyValuePair<Ingredient, int> ingredient in ingredient_to_put)  
        {  
            foreach (ItemPrefab item in list_prefab)
            { 
                if(ingredient.Key.nom == item.ObjectPrefab.GetComponent<Legume>().nom){
                    PosObject = item.PosObject;
                    ObjectPrefab = item.ObjectPrefab;
                    break;
                }
            }
            instancy = 0;
            while(instancy < ingredient.Value){
                Instantiate(ObjectPrefab, PosObject.transform.position + new Vector3(0, instancy, 0), Quaternion.identity);
                instancy++;
            }
        } 
    }
    

    private void Update() {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hit;
        if(Input.GetMouseButtonUp(0) && Physics.Raycast(ray, out hit, Mathf.Infinity)){
            //if the cursor hits an ingredient, add it to the inventory
            if(hit.collider.gameObject.GetComponent<Legume>() != null){
                hit.collider.gameObject.layer = 3;
                Destroy(hit.collider.gameObject.GetComponent<Legume>());
                Inventaire.instance.AddLegume(hit.collider.gameObject.GetComponent<Legume>());
                hit.collider.gameObject.transform.position = GameObject.FindWithTag("PosBasket").transform.position;
            }
            // if the cursor hits a character, set the guest 
            if(hit.collider.gameObject.GetComponent<Character>() != null)
            {
                if (GameManager.instance.guest == null) 
                {
                    onChangeGuest(hit.collider.gameObject.GetComponent<Character>().name);
                }
                else if (!GameManager.instance.guest.Equals(hit.collider.gameObject.name))
                {
                    onChangeGuest(hit.collider.gameObject.GetComponent<Character>().name);
                }
            }
        }
    }

    // change the current guest
    private void onChangeGuest(string guestName)
    {
        GameManager.instance.guest = guestName;
        changeImage(guestName);
    }

    // change the current selected guest visual
    private void changeImage(string guestName)
    {
        foreach(Character ch in GameManager.instance.characterList)
        {
            if(ch.name.Equals(guestName))
            {
                currentGuest.sprite = ch.selectedSprite;
            }
        }
    }

    public void LoadKitchenScene()
    {
        GameManager.instance.loadKitchenScene();
    }
}
