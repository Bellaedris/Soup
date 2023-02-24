using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;


public class LongClickRightMarket : MonoBehaviour,  IPointerDownHandler, IPointerUpHandler
{

    bool pressing = false;  
    private float pointerDownTimer;
    public GameObject leftArrow;

    private void Start() {
        if(Camera.main.transform.position.z > 9.5){
            this.gameObject.SetActive(false);
        }
    }
    private void Update() {

        if(pressing && Camera.main.transform.position.z < 10.5){
            Camera.main.transform.Translate(Vector3.right * (Time.deltaTime*2));
        }
        if(Camera.main.transform.position.z > 0){
            leftArrow.SetActive(true);
        }
    }


    //public PlayerController.ButtonsDirection dir;
 
 
    public void OnPointerDown (PointerEventData eventData)
    {
        if(Camera.main.transform.position.z > 10){
            this.gameObject.SetActive(false);
        }
        if(Camera.main.transform.position.z > 0){
            leftArrow.SetActive(true);
        }

        pressing = true;
    }
   
   
    public void OnPointerUp (PointerEventData eventData)
    {
        if(Camera.main.transform.position.z > 10){
            this.gameObject.SetActive(false);
        }
        if(Camera.main.transform.position.z > 0){
            leftArrow.SetActive(true);
        }
        pressing = false;
    }


}
