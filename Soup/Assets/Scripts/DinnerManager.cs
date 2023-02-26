using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class DinnerManager : MonoBehaviour
{

    public GameObject guestPrefab;
    public Soup soup;
    public GameObject loadMorning;
    public GameObject soupSurface;
    public Object emotion;

    [Tooltip("Position the guest will spawn at")]
    public Vector3 guestPos;
    [Tooltip("Rotation of the guest")]
    public Vector3 guestRotation;
    bool drinkSoop = false;

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
            guestPrefab = (GameObject)Resources.Load("prefab/Characters/" + guestName, typeof(GameObject));
            guestPrefab.transform.localScale = Vector3.one * 0.09f;
            Instantiate(guestPrefab, guestPos, Quaternion.Euler(guestRotation));
            soup = GameManager.instance.GetComponent<Soup>();
            soupSurface.GetComponent<Renderer>().material.SetColor("_Color", soup.computeColor());
            loadMorning.SetActive(false);
            loadMorning.GetComponent<CanvasGroup>().alpha = 0;


        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonUp(0) && Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.name == "bowl") //If you click on the bowl
            {
                if (drinkSoop) //If you have already drunk the soup
                {
                    hit.collider.gameObject.transform.position = new Vector3(-19.463f, 0.882f, -6.259f);
                    hit.collider.gameObject.transform.rotation = Quaternion.Euler(-90, 90, 0);
                    soupSurface.SetActive(false);
                    loadMorning.SetActive(true);
                    loadMorning.GetComponent<CanvasGroup>().alpha = 1;

                }
                else //If you have not yet drunk the soup
                {
                    emotion.GetComponent<SpriteRenderer>().sprite = GameManager.instance.ChangeFaceWhenEatingSoup();
                    hit.collider.gameObject.transform.position += new Vector3(0, 0.2f, 0);
                    hit.collider.gameObject.transform.rotation = Quaternion.Euler(-120, 90, 0);
                    drinkSoop = true;
                }
            }
        }
    }
    public void LoadMorningScene(){
        GameManager.instance.guest = null;
        GameManager.instance.loadMorningScene();
    }
}
