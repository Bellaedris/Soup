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

    private bool next;
    private bool previous;

    public void Next()
    {
        next = true;
    }

    public void Previous()
    {
        previous = true;
    }
    void Start()
    {
        Debug.Log("Guest : " + GameManager.instance.character);
        characters = GameManager.instance.character;
        Debug.Log("name 0 : " + GameManager.instance.character[0].name);

        if (!canvas) canvas = GetComponentInParent<Canvas>();
        if (!canvas) Debug.LogError("Book should be a child to canvas");

        Left.gameObject.SetActive(true);
        Right.gameObject.SetActive(true);
        Right.sprite = bookPages[0]; //Cover
        Left.sprite = background;
        Shadow.enabled = false;
        next = false;
    }

    void Update()
    {
       if (next && currentPage < bookPages.Length)           
        //if (Input.GetKeyDown(KeyCode.Z) && currentPage + 1 < bookPages.Length)
        {
            next = false;
            UpdateBookRTLToPoint();
        }
        if (previous && currentPage > 1)
        //if (Input.GetKeyDown(KeyCode.A) && currentPage > 1)
        {
            previous = false;
            UpdateBookLTRToPoint();
        }
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
                if (characters[currentPage - 1].isFavIngredientsKnown[i]) // For Right page
                    RCache[i].enabled = false;
                else
                    RCache[i].enabled = true;

                if (characters[currentPage - 2].isFavIngredientsKnown[i]) // For Left page
                    LCache[i].enabled = false;
                else
                    LCache[i].enabled = true;
            }
            Left.sprite = bookPages[currentPage - 1];
            // Display the soup you like best that you know
            if (characters[currentPage - 1].IsFavSoupKnown) // For Right page
                RCache[2].enabled = false;
            else
                RCache[2].enabled = true;

            if (characters[currentPage - 2].IsFavSoupKnown) // For Left page
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
                if (characters[currentPage - 1].isFavIngredientsKnown[i])
                    RCache[i].enabled = false;
                else
                    RCache[i].enabled = true;
            }
            // Display the soup you like best that you know Right
            if (characters[currentPage - 1].IsFavSoupKnown)
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
            if (characters[currentPage - 2].isFavIngredientsKnown[i])
                LCache[i].enabled = false;
            else
                LCache[i].enabled = true;
        }
        // Display the soup you like best that you know Left
        if (characters[currentPage - 2].IsFavSoupKnown)
            LCache[2].enabled = false;
        else
            LCache[2].enabled = true;
    }    
}
