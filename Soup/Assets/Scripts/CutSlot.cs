using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CutSlot : MonoBehaviour, IDropHandler 
{
    public void OnDrop(PointerEventData eventData)
    {
        
        if(eventData.pointerDrag != null)
        {
            Debug.Log("Je coupe " + eventData.pointerDrag.GetComponent<Legume>().nom);
            Legume incomingLegume = eventData.pointerDrag.GetComponent<Legume>();
            SoupUIController soupUI = FindObjectOfType<SoupUIController>();
            soupUI.AddMixedBitsToSoup(incomingLegume);
            Inventaire inventaire = Inventaire.instance;

            //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            foreach (KeyValuePair<Legume, int> kvp in inventaire.inventaireLegumes)
            {
                //Debug.Log(kvp.Key.nom);
                if (kvp.Key.nom == incomingLegume.nom)
                {
                    soupUI.RemoveVegFromInv(kvp.Key);
                    DragDrop.updateLayer("UILegumeObject", 0);
                }
            }
        }
    }
}
