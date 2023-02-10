using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject character;

    public List<Ingredient> favIngredients;
    public List<bool> isFavIngredientsKnown;

    public Soup favSoup;
    public bool IsFavSoupKnown;

    public GameObject friend;

    [SerializeField]
    private int affection = 0;

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateAffection(int i)
    {
        affection += i;
    }
}
