using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    private void Start() {
        //Inventaire_2.Instance.loadFile();
        GameManager.instance.InitGame();
    }


    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hit;
        if(Input.GetMouseButtonUp(0) && Physics.Raycast(ray, out hit, Mathf.Infinity)){
            if(hit.collider.gameObject.GetComponent<Legume>() != null){
                
                Inventaire_2.instance.AddLegume(hit.collider.gameObject.GetComponent<Legume>());
                Destroy(hit.collider.gameObject);
            }

        }
    }
}
