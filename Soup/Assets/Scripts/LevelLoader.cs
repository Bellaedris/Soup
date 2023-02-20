using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    public Animator Transition;

    public IEnumerator loadlevel(){
        if(Transition.gameObject.activeSelf){
            Transition.SetTrigger("Start");
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(6);        
        }
        else{
            Debug.Log("error");
            yield return new WaitForSeconds(1f);

        }


    }
    public void trans(){
        StartCoroutine(loadlevel());

    }

}
