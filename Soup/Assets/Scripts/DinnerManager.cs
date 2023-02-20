using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinnerManager : MonoBehaviour
{

    public GameObject guestPrefab;
    public Soup soup;

    public GameObject soupSurface;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.guest.Equals(""))
        {
            Debug.Log("Lonely...");
        } else
        {
            Debug.Log("Guest : " + GameManager.instance.guest);
            string guestName = GameManager.instance.guest;
            guestPrefab = (GameObject)Resources.Load("prefab/Characters/"+guestName, typeof(GameObject));
            guestPrefab.transform.localScale = Vector3.one * 0.09f;
            Instantiate(guestPrefab, new Vector3(-20.14f, 1.06f, -6.2f), Quaternion.Euler(0f, 90f, 0f));

            soup = GameManager.instance.GetComponent<Soup>();
            soupSurface.GetComponent<Renderer>().material.SetColor("_Color", soup.computeColor()); 
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
