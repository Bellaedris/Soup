using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor.U2D;
using UnityEngine.U2D;
using UnityEngine.Purchasing;
using UnityEngine.TextCore.Text;
using System.Collections.Generic;
using System.Linq;

[ExecuteInEditMode]
public class BookPerso : MonoBehaviour
{
    public Canvas canvas;
    [SerializeField]
    RectTransform BookPanel;
    public Sprite background;
    public Sprite[] bookPages;
    public Sprite[] whitePages;
    //represent the index of the sprite shown in the right page
    public int currentPage = 0;
    public int TotalPageCount
    {
        get { return bookPages.Length; }
    }

    private Character[] characters;
    public Image Shadow;
    public Image ShadowLTR;
    public Image Left;
    public Image Right;
    public Image[] LCache;
    public Image[] RCache;

    public class Character
    {

        public Dictionary<string, bool> favoriteFood = new Dictionary<string, bool>();
        public Dictionary<string, bool> favoriteSoupe = new Dictionary<string, bool>();

        public Character()
        {
            string text = "azerty";
            string text2 = "azerty2";
            favoriteFood.Add(text, true);
            favoriteFood.Add(text2, false);
            favoriteSoupe.Add(text, false);
        }

    };
    void Start()
    {
        characters = new Character[bookPages.Length - 1];
        for(int i=0; i<characters.Length; i++)
        {
            characters[i] = new Character();
        }
        characters[3].favoriteFood["azerty"] = false;
        characters[2].favoriteFood["azerty2"] = true;
        characters[2].favoriteSoupe["azerty"] = true;
        characters[0].favoriteSoupe["azerty"] = true;

        if (!canvas) canvas = GetComponentInParent<Canvas>();
        if (!canvas) Debug.LogError("Book should be a child to canvas");

        Left.gameObject.SetActive(true);
        Right.gameObject.SetActive(true);
        Right.sprite = bookPages[0]; //Cover
        Left.sprite = background;
        Shadow.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && currentPage < bookPages.Length)
            UpdateBookRTLToPoint();
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentPage > 1)
            UpdateBookLTRToPoint();
    }
    public void UpdateBookLTRToPoint() // Turning a page back
    {
        currentPage -= 2;       
        Right.sprite = bookPages[currentPage];
        ShadowLTR.enabled = true;
        if (currentPage > 1) // If we are not on the cover
        {
            // Display the vegetables you like best that you know
            for (int i = 0; i < 2; i++) 
            {
                if (characters[currentPage - 1].favoriteFood.Values.ElementAt(i)) // For Right page
                    RCache[i].enabled = false;
                else
                    RCache[i].enabled = true;

                if (characters[currentPage - 2].favoriteFood.Values.ElementAt(i)) // For Left page
                    LCache[i].enabled = false;
                else
                    LCache[i].enabled = true;
            }
            Left.sprite = bookPages[currentPage - 1];
            // Display the soup you like best that you know
            if (characters[currentPage - 1].favoriteSoupe.Values.ElementAt(0)) // For Right page
                RCache[2].enabled = false;
            else
                RCache[2].enabled = true;

            if (characters[currentPage - 2].favoriteSoupe.Values.ElementAt(0)) // For Left page
                LCache[2].enabled = false;
            else
                LCache[2].enabled = true;
        }
        else // If we are on the cover
        {
            Left.sprite = background;
            Shadow.enabled = false;
            for (int i = 0; i < RCache.Length; i++)
            {
                RCache[i].enabled = false;
                LCache[i].enabled = false;
            }
        }
    }
    public void UpdateBookRTLToPoint()
    {
        currentPage += 2;
        if (currentPage < bookPages.Length) //If you are not on the last page
        {
            Right.sprite = bookPages[currentPage];
            // Display the vegetables you like best that you know
            for (int i = 0; i < 2; i++) // For Rigth page
            {
                if (characters[currentPage - 1].favoriteFood.Values.ElementAt(i))
                    RCache[i].enabled = false;
                else
                    RCache[i].enabled = true;
            }
            // Display the soup you like best that you know Right
            if (characters[currentPage - 1].favoriteSoupe.Values.ElementAt(0))
                RCache[2].enabled = false;
            else
                RCache[2].enabled = true;
            Shadow.enabled = true;            
        }
        else // If you are on the last page
        {
            Right.sprite = background;
            ShadowLTR.enabled = false;
            for (int i = 0; i < RCache.Length; i++)
            {
                RCache[i].enabled = false;
            }
        }
        Left.sprite = bookPages[currentPage - 1];
        // Display the vegetables you like best that you know
        for (int i = 0; i < 2; i++) // For Left Page
        {
            if (characters[currentPage - 2].favoriteFood.Values.ElementAt(i))
                LCache[i].enabled = false;
            else
                LCache[i].enabled = true;
        }
        // Display the soup you like best that you know Left
        if (characters[currentPage - 2].favoriteSoupe.Values.ElementAt(0))
            LCache[2].enabled = false;
        else
            LCache[2].enabled = true;
    }    
}
