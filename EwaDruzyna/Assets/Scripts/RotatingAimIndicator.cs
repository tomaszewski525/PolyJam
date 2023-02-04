using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingAimIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouse = Input.mousePosition; //zbieranie pozycji kursora
        Vector3 offset = Camera.main.ScreenToWorldPoint(mouse) - transform.position; //znajduje pozycję obiektu, o tagu MainCamera?
        //Vector2 offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);

        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        print(angle);
    }
}
