using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingAimIndicator : MonoBehaviour
{
    private GameObject mySprite, myRoot;
    public GameObject myPlantSprite;
    // Start is called before the first frame update
    void Start()
    {
        mySprite = GameObject.Find("aimIndicator");
        myRoot = GameObject.Find("root");
        myPlantSprite = GameObject.Find("PlantSprite");

        mySprite.GetComponent<Renderer>().enabled = false; //na start ukrywa indicator
        myRoot.GetComponent<Renderer>().enabled = false;

        ignoreMask = ~(1 << myPlantSprite.layer | 1 << myRoot.layer);
    }

    bool isThereAnEnemyWithinReach = false;
    public GameObject enemyInBounds;


    IEnumerator waiter()
    {
        yield return new WaitForSeconds(0.4F);
        myRoot.GetComponent<Renderer>().enabled = false;
    }

    void SwitchToEnemy()
    {
        //print(enemyInBounds);
        if (enemyInBounds != null)
        {
            switch (enemyInBounds.name)
            {
                case "EnemyLadybug":
                    SwitchToLadybug();
                    break;
                case "EnemySpider":
                    SwitchToSpider();
                    break;
                case "EnemySlug":
                    SwitchToSlug();
                    break;
            }
        }
    }

    void SwitchToLadybug()
    {
        print("Ladybug");
        //to do
    }

    void SwitchToSpider()
    {
        print("Spider");
        //to do
    }

    void SwitchToSlug()
    {
        print("Slug");
        //to do
    }

    void FireTheRoots()
    {
        mySprite.GetComponent<Renderer>().enabled = false;
        myRoot.GetComponent<Renderer>().enabled = true;
        myRoot.GetComponent<Animator>().Play("RootAnimation", 0, 0f);
        if (!isThereAnEnemyWithinReach)
        {
            StartCoroutine(waiter()); //TO TWORZY DELAY PRZED ZNIKNIECIEM
        }
        SwitchToEnemy();
    }

    // Update is called once per frame
    public float radius = 2f;
    public LayerMask ignoreMask;

    void Update()
    {
        Physics2D.queriesStartInColliders = false;

        if (Input.GetMouseButtonDown(0))
        {
            mySprite.GetComponent<Renderer>().enabled = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            FireTheRoots();
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 mouse = Input.mousePosition;
            Vector3 offset = Camera.main.ScreenToWorldPoint(mouse) - transform.position;

            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            //print(direction);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, radius, ignoreMask);
            Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, 10), new Vector3(transform.position.x, transform.position.y, 10) + new Vector3((direction * radius).x, (direction * radius).y, -10), Color.red, 2.0f);

            if (hit.collider != null && hit.collider.gameObject != myRoot)
            {
                enemyInBounds = hit.collider.gameObject;
            }
            else
            {
                enemyInBounds = null;
            }
        }
    }

}