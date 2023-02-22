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
    public Sprite selectedSprite;

    [SerializeField]
    private int affection = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateAffection(int i)
    {
        affection += i;
    }

    public int getAffection()
    {
        return affection;
    }
}
