using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinnerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.guest.characterName.Equals(""))
        {
            Debug.Log("Lonely...");
        } else
        {
            Debug.Log("Guest : " + GameManager.instance.guest.characterName);
        }   
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
