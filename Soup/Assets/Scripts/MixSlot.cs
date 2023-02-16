using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MixSlot : MonoBehaviour, IDropHandler 
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name);
        if(eventData.pointerDrag != null)
        {
            Debug.Log("Mix " + eventData.pointerDrag.GetComponent<Legume>().nom);
            Legume incomingLegume = eventData.pointerDrag.GetComponent<Legume>();

            SoupUIController soupUI = FindObjectOfType<SoupUIController>();
            soupUI.AddLegToSoup(incomingLegume);
            Inventaire inventaire = Inventaire.instance;

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
