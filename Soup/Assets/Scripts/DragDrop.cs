using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] public Canvas canvas;
    [SerializeField] private GameObject dragableObject;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private GameObject dragable;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        dragable = Instantiate(dragableObject, transform);
        dragable.transform.GetChild(1).GetComponent<MeshFilter>().mesh = eventData.pointerDrag.GetComponent<Legume>().objet;
        if (eventData.pointerDrag.GetComponent<Legume>() != null)
        {
            updateLayer("UILegumeObject", 3);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Feur");
        updateLayer("UILegumeObject", 0);

        GameObject.Destroy(dragable);
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        dragable.GetComponent<RectTransform>().anchoredPosition += eventData.delta / canvas.scaleFactor;
     }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public static void updateLayer(string tag, int layer)
    {
        foreach (var item in GameObject.FindGameObjectsWithTag(tag))
        {
            item.layer = layer;
        }
    }
}
