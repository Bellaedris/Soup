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
            SoupUIController soupUI = FindObjectOfType<SoupUIController>();
            soupUI.AddLegToSoup(eventData.pointerDrag.GetComponent<Legume>());
            soupUI.RemoveVegFromInv(eventData.pointerDrag.GetComponent<Legume>());
        }
    }
}
