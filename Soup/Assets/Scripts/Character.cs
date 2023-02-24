using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Rendering.InspectorCurveEditor;

public class Character : MonoBehaviour
{
    public string name;

    public List<Ingredient> favIngredients;
    public List<bool> isFavIngredientsKnown;

    [Tooltip("Favorite soup recipe")]
    public Soup favSoup;
    public bool IsFavSoupKnown;

    public GameObject friend;
    [Tooltip("Sprite displayed when the character is selected")]
    public Sprite[] emotionSprites;
    public Sprite selectedSprite;

    [SerializeField]
    private int affection = 0;

    public void updateAffection(int i)
    {
        affection += i;
    }

    public int getAffection()
    {
        return affection;
    }
    public void updateSprite(int i)
    {
        GetComponent<SpriteRenderer>().sprite = emotionSprites[i];
    }

    private void OnMouseOver() {
        GetComponent<SpriteRenderer>().sprite = emotionSprites[1];
    }
    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        GetComponent<SpriteRenderer>().sprite = emotionSprites[0];
    }
}
