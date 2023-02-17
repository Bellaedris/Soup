using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest : MonoBehaviour
{
    public static Guest instance;

    public string characterName;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeGuest(GameObject newGuest)
    {
        characterName = newGuest.name;
    }

    public void resetGuest()
    {
        characterName = null;
    }
}
