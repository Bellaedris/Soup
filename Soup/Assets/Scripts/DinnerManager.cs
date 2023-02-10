using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinnerManager : MonoBehaviour
{

    public GameObject guestPrefab;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.guest.characterName.Equals(""))
        {
            Debug.Log("Lonely...");
        } else
        {
            Debug.Log("Guest : " + GameManager.instance.guest.characterName);
            string guestName = GameManager.instance.guest.characterName;
            guestPrefab = (GameObject)Resources.Load("prefab/Characters/"+guestName, typeof(GameObject));
            guestPrefab.transform.localScale = Vector3.one;
            Instantiate(guestPrefab, new Vector3(-20.2f, 0.5f, -6.2f), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
