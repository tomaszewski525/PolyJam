using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingAimIndicator : MonoBehaviour
{
    private GameObject mySprite;
    
    // Start is called before the first frame update
    void Start()
    {
         mySprite = GameObject.Find("aimIndicator");
         
         mySprite.GetComponent<Renderer>().enabled = false; //na start ukrywa indicator
    }

    void FireTheRoots() {
         mySprite.GetComponent<Renderer>().enabled = false;
        //to do
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
               mySprite.GetComponent<Renderer>().enabled = true; //na ramce wcisniecia myszy sie pojawia indicator
        }
        else if(Input.GetMouseButtonUp(0)){
            FireTheRoots(); //na ramce zwolnienia myszy robi sie cos jeszcze niezaimplementowanego
        }
        else if(Input.GetMouseButton(0)){
            Vector3 mouse = Input.mousePosition;
            Vector3 offset = Camera.main.ScreenToWorldPoint(mouse) - transform.position;
        

            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
            


    }
}
